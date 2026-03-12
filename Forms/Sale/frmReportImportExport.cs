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
using DevExpress.XtraPrinting;
namespace BMS
{
    public partial class frmReportImportExport : _Forms
    {
        public ProductGroupModel name = new ProductGroupModel();
        public delegate void Signal(string signal);
        public Signal GetSignal;
        int Show;
        public frmReportImportExport()
        {
            InitializeComponent();
        }
        string _warehouseCode = "";
        public frmReportImportExport(string warehouseCode)
        {
            InitializeComponent();
            _warehouseCode = warehouseCode;
        }

        private void frmProductSale_Load(object sender, EventArgs e)
        {
            this.Text += " - " + _warehouseCode;
            dtpFromDate.Value.AddMonths(-1);
            grdData.Visible = false;
            tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
            loadProductGroup();
        }

        private void loadProductGroup()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM dbo.ProductGroup where IsVisible = 1");
            treeData.DataSource = dt;
        }

        DataTable dttt = new DataTable();
        void loadGrdData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);


            int IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            DataTable dt  = TextUtils.LoadDataFromSP("spGetDataReportImportExport_New", "A", 
                new string[] { "@StartDate", "@EndDate","@Group","@Find", "@WarehouseCode" }, 
                new object[] { dateTimeS, dateTimeE, IDTree,txtFilterText.Text.Trim(),_warehouseCode });
            grdMaster.DataSource = dt;
            LoadDataDetail();
        }

        void LoadDataDetail()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("LoadProductSaleDetail", "A", new string[] { "@ID" }, new object[] { id });
            grdData.DataSource = dt;
        }

        /// <summary>
        /// load dữ liệu vào bảng grdData khi thay đổi tại bảng treeData
        /// </summary>
        //public string GroupName;
        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string Id = TextUtils.ToString(treeData.FocusedNode.GetValue(colID));
           
                loadGrdData();
            
        }
        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", _warehouseCode).FirstOrDefault();
            warehouse = warehouse ?? new WarehouseModel();
            frmProductGroupDetail frm = new frmProductGroupDetail(warehouse.ID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProductGroup();
            }
        }
        /// <summary>
        /// hàm sửa lại dữ liệu Group
        /// </summary>
        public static int EditClick = 0;
        private void editDataGroup()
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", _warehouseCode).FirstOrDefault();
            warehouse = warehouse ?? new WarehouseModel();

            EditClick = 1;
            int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            if (ID == 0) return;
            ProductGroupModel model = (ProductGroupModel)ProductGroupBO.Instance.FindByPK(ID);
            frmProductGroupDetail frm = new frmProductGroupDetail(warehouse.ID);
            frm.productGroup = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProductGroup();
            }
        }
        /// <summary>
        /// dùng button Edit Data Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            editDataGroup();
        }
        /// <summary>
        /// click double to edit data group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            editDataGroup();
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            int IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            string groupName = TextUtils.ToString(treeData.FocusedNode.GetValue(colGroupName));
            if (IDTree == 0) return;

            if (ProductSaleBO.Instance.CheckExist("ProductGroupID", IDTree))
            {
                MessageBox.Show(string.Format("Nhóm thiết bị đang chứa thiết bị, không thể xóa nhóm thiết bị [{0}] khỏi danh sách!!", groupName), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhóm [{0}] hay không ?", groupName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductGroupBO.Instance.Delete(IDTree);
                treeData.DeleteNode(treeData.FocusedNode);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsVisible = TextUtils.ToBoolean(treeData.FocusedNode.GetValue(colIsVisible));
            if (MessageBox.Show("Bạn có muốn chuyển nhóm này thành không sử dụng không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
                if (IsVisible)
                {
                    TextUtils.ExcuteSQL($"Update ProductGroup set IsVisible = 0 where ID = {id}");
                }
                loadProductGroup();
            }
        }

        //--------------- code phần sản phẩm, thiết bị ------------------------------------
        /// <summary>
        /// creat tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            string groupName = TextUtils.ToString(treeData.FocusedNode.GetValue(colGroupName));
            if (IDTree == 0) return;
            ProductSaleModel model = new ProductSaleModel();
            model.ProductGroupID = IDTree;
            frmProductDetailSale frm = new frmProductDetailSale();
            frm.oProductSaleModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrdData();
                this.GetSignal?.Invoke("A");
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
            this.GetSignal?.Invoke("A");
        }
        /// <summary>
        /// void edit data in product
        /// </summary>
        public static int editGrv = 0;
        private int prevRow;

        private void editDataProduct()
        {
            // giữ con trỏ chuột đúng vị trí khi DoubleClick
            //if (grvMaster.GetSelectedRows().Length == 0) return;
            //prevRow = grvMaster.GetSelectedRows()[0];

            // giữ nguyên vị trí cột và hàng được lấy tiêu điểm không bị nhảy lên đầu 
            var topRowIndex = grvMaster.TopRowIndex;
            var focusedRowHandle = grvMaster.FocusedRowHandle;

            editGrv = 1;
            //int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colProductSaleID));
            if (ID == 0) return;
            ProductSaleModel model = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(ID);
            frmProductDetailSale frm = new frmProductDetailSale();
            frm.oProductSaleModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrdData();

                // giữ con trỏ chuột đúng vị trí khi DoubleClick
                //if (grvMaster.GetSelectedRows().Length != 0)
                //    grvMaster.FocusedRowHandle = prevRow;
                //grvMaster.SelectRow(prevRow);


                // giữ nguyên vị trí cột và hàng được lấy tiêu điểm không bị nhảy lên đầu 
                grvMaster.TopRowIndex = topRowIndex;
                grvMaster.FocusedRowHandle = focusedRowHandle;

            }
        }
        /// <summary>
        /// delete sản phẩm khỏi danh sách
        /// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductCode));
            string productName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductName));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] : [{1}] không?", productCode, productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductSaleBO.Instance.Delete(ID);
                grvMaster.DeleteSelectedRows();
            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{productCode}') ");
        }
        /// <summary>
        /// find data in DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadGrdData();
        }
        /// <summary>
        /// sử dụng nút enter để tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadGrdData();
            }
        }
        /// <summary>
        /// hàm tìm kiếm sản phẩm
        /// </summary>


        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvMaster.OptionsPrint.AutoWidth = false;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                XlsExportOptions options = new XlsExportOptions();

                try
                {
                    grvMaster.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmProductExcelSale frmExcel = new frmProductExcelSale();
            frmExcel.ShowDialog();
            loadProductGroup();
        }

        private void chkAllProduct_CheckedChanged(object sender, EventArgs e)
        {
            loadGrdData();
        }

        //TOÁN Update 09112022
        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editGrv = 1;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            int productSaleID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colProductSaleID));
            string ProductName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductName));
            string NumberDauKy = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNumberInStoreDauKy));
            string NumberCuoiKy = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNumberInStoreCuoiKy));
            string Import = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colImport));
            string Export = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colExport));
            if (ID == 0) return;
            ProductSaleModel model = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(ID);
            frmChiTietSanPhamSale frm = new frmChiTietSanPhamSale();
            frm.productSaleID = productSaleID;
            frm.oProductSaleModel = model;
            frm.ProductName = ProductName;
            frm.NumberDauKy = NumberDauKy;
            frm.NumberCuoiKy = NumberCuoiKy;
            frm.Import = Import;
            frm.Export = Export;
            frm.WarehouseCode = _warehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrdData();
            }
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            editDataProduct();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataDetail();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int IDGroup = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            TextUtils.LoadDataFromSP("spSetProductNewCode", "A", new string[] { "@Group" }, new object[] { IDGroup });
            loadGrdData();
        }
       
        private void showNCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string arrSv = File.ReadAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"));
            if (TextUtils.ToInt(arrSv) == 1)
            {
                grdData.Visible = false;
                tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
                Show = 0;
            }
            else
            {
                tableLayoutPanel2.SetColumnSpan(grdMaster, 1);
                grdData.Visible = true;
                Show = 1;
            }
            
        }

        private void frmProductSale_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Show==0)
                File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "0");
            else
                File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "1");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvMaster);
        }
    }

}


