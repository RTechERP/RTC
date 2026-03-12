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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraEditors.Controls;

namespace BMS
{
    public partial class frmCustomer : _Forms
    {
        int warehouseID = 0;
        DataSet oDataSet = new DataSet();
        public frmCustomer(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            if (warehouseID == 3) this.Text += $" - {this.Tag}";

            txtPageNumber.Text = "1";
            loadCustomer();
            LoadEmployee();
            LoadTeam();

        }

        private void LoadTeam()
        {
            // DataSet ds = SQLHelper<T>.GetDataSetFromSP("spGetEmployeeManager", new string[] { }, new object[] { });
            DataTable dt = TextUtils.Select("Select * From GroupSales");
            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "GroupSalesName";
            cboTeam.Properties.DataSource = dt;

        }
        #region Methods
        private void loadCustomer()
        {

            int groupID = TextUtils.ToInt(cboTeam.EditValue);

            int ID = TextUtils.ToInt(grdData.FocusedView);
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            oDataSet = TextUtils.LoadDataSetFromSP("spGetCustomer"
                                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@EmployeeID", "@GroupID" }
                                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), employeeId, groupID });
            grdData.DataSource = oDataSet.Tables[0];


            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }

        void LoadEmployeeSale()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            List<CustomerEmployeeModel> list = SQLHelper<CustomerEmployeeModel>.FindByAttribute("CustomerID", id);
            grdSale.DataSource = list;
        }

        private void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployeeSale.ValueMember = "ID";
            cboEmployeeSale.DisplayMember = "FullName";
            cboEmployeeSale.DataSource = list;

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;

            bool isAdmin = (!Global.IsAdmin && !Global.IsAdminSale && Global.UserID != 1177 && Global.UserID != 1313 && Global.UserID != 23 && Global.UserID != 1380 && Global.LoginName == "PU11");
            if (isAdmin)
            {
                cboEmployee.Enabled = false;
                cboEmployee.EditValue = Global.EmployeeID;
            }
        }


        #endregion

        #region Buttons Events
        /// <summary>
        /// thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //CustomerModel model = new CustomerModel();
            //frmCustomerDetail frm = new frmCustomerDetail();
            //frm.customer = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadCustomer();
            //}


            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            frm.Show();
        }

        /// <summary>
        /// button sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// click button delete khách hàng khỏi danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string customerName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCustomerName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa khách hàng [{0}] không?", customerName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //string sql = $"UPDATE dbo.Customer SET IsDeleted = 1 WHERE ID = {ID}";
                //TextUtils.ExcuteSQL(sql);

                CustomerModel customer = SQLHelper<CustomerModel>.FindByID(ID);
                if (customer == null) return;
                customer.IsDeleted = true;
                SQLHelper<CustomerModel>.Update(customer);

                grvData.DeleteSelectedRows();
                grvData_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button nhập excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmCustomerExcel frmExcel = new frmCustomerExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }
        #endregion

        /// <summary>
        /// void edit data 
        /// </summary>
        private void EditData()
        {
            //var focusedRowHandle = grvData.FocusedRowHandle;
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //CustomerModel model = (CustomerModel)CustomerBO.Instance.FindByPK(ID);
            //frmCustomerDetail frm = new frmCustomerDetail();
            //frm.customer = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadCustomer();

            //    grvData.FocusedRowHandle = focusedRowHandle;
            //    grvData_FocusedRowChanged(null, null);
            //}


            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            CustomerModel model = (CustomerModel)CustomerBO.Instance.FindByPK(ID);
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            frm.customer = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
                grvData.FocusedRowHandle = focusedRowHandle;
                grvData_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// event EditData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// link đến bảng contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            ArrayList arr = CustomerContactBO.Instance.FindByAttribute("CustomerID", id);
            grdContact.DataSource = arr;
            ArrayList address = AddressStockBO.Instance.FindByAttribute("CustomerID", id);
            grdAddress.DataSource = address;

            LoadEmployeeSale();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadCustomer();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadCustomer();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadCustomer();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadCustomer();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadCustomer();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadCustomer();
            LoadEmployeeSale();
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


            //GridControl gridData = new GridControl();
            //GridView gridView = new GridView();
            //GridColumn colStt = gridView.Columns.Add();

            //gridView.Columns.Add
            //gridData.MainView = gridView;

            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachKhachHang_{DateTime.Now.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    DataTable dtExport = oDataSet.Tables[2];
                    grdDataExport.DataSource = dtExport;

                    //string filepath = Path.Combine(f.SelectedPath, $"");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdDataExport;

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



        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void frmCustomer_SizeChanged(object sender, EventArgs e)
        {
            //int width = splitContainerControl1.Width;
            //int height = splitContainerControl2.Height;

            //splitContainerControl1.Panel2.MinSize = 350;
            //splitContainerControl1.Panel1.MinSize = width - 300;

            //splitContainerControl2.Panel2.MinSize = 200;
            //splitContainerControl2.Panel1.MinSize = height - 200;

            //splitContainerControl1.SplitterPosition = 2 * width / 4;
            //splitContainerControl2.SplitterPosition = 2 * height / 3;


        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            //var listEmp = SQLHelper<EmployeeModel>.LoadDataFromSP("spGetEmployeeManager", new string[] { "@group" }, 
            //                                       new object[] { TextUtils.ToInt(cboTeam.EditValue) });

            loadCustomer();
        }

        private void btnCustomerSpecialization_Click(object sender, EventArgs e)
        {
            frmCustomerSpecialization frm = new frmCustomerSpecialization();
            frm.Show();
        }
    }
}


