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
    public partial class frmProductSale : _Forms
    {
        public ProductGroupModel name = new ProductGroupModel();
        public delegate void Signal(string signal);
        public Signal GetSignal;
        int Show;
        public frmProductSale()
        {
            InitializeComponent();
        }

        private void frmProductSale_Load(object sender, EventArgs e)
        {
            Show = TextUtils.ToInt(File.ReadAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt")));
            //if (Show == 0)
            //{
            //    grdData.Visible = false;
            //    tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
            //}
            //else
            //{
            //    tableLayoutPanel2.SetColumnSpan(grdMaster, 1);
            //    grdData.Visible = true;
            //}

            loadProductGroup();
        }

        private void loadProductGroup()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM dbo.ProductGroup");
            treeData.DataSource = dt;
        }

        DataTable dttt = new DataTable();

        void loadGrdData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải..."))
            {
                int IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));

                if (IDTree == 4) colDetail.Visible = false; //nếu là kho vision
                else grvMaster.Columns[colDetail.FieldName].VisibleIndex = grvMaster.VisibleColumns.Count - 1;

                if (chkAllProduct.Checked) IDTree = 0;

                DataTable dt = TextUtils.LoadDataFromSP("usp_LoadProductsale", "A", new string[] { "@id", "@Find" }, new object[] { IDTree, txtFilterText.Text.Trim() });
                grdMaster.DataSource = dt;


                LoadDataDetail();
                LoadProductGroupWarehouse();
            }

        }

        void LoadDataDetail()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("LoadProductSaleDetail", "A", new string[] { "@ID" }, new object[] { id });
            grdData.DataSource = dt;
        }

        void LoadProductGroupWarehouse()
        {
            int productGroupID = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A", new string[] { "@WarehouseID", "@ProductGroupID" }, new object[] { 0, productGroupID });
            grdProductGroupWarehouse.DataSource = dt;

        }

        /// <summary>
        /// load dữ liệu vào bảng grdData khi thay đổi tại bảng treeData
        /// </summary>
        //public string GroupName;
        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string Id = TextUtils.ToString(treeData.FocusedNode.GetValue(colID));
            if (!chkAllProduct.Checked)
            {
                loadGrdData();
            }
        }
        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmProductGroupDetail frm = new frmProductGroupDetail(0);
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
            EditClick = 1;
            int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            if (ID == 0) return;
            ProductGroupModel model = (ProductGroupModel)ProductGroupBO.Instance.FindByPK(ID);
            frmProductGroupDetail frm = new frmProductGroupDetail(0);
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

            if (ProductSaleBO.Instance.CheckExist("ProductGroupID", IDTree) && !Global.IsAdmin)
            {
                MessageBox.Show(string.Format("Nhóm thiết bị đang chứa thiết bị, không thể xóa nhóm thiết bị [{0}] khỏi danh sách!!", groupName), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhóm [{0}] hay không ?", groupName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {"IsVisible",false }
                };

                SQLHelper<ProductGroupModel>.UpdateFieldsByID(myDict, IDTree);
                //ProductGroupBO.Instance.Delete(IDTree);
                //treeData.DeleteNode(treeData.FocusedNode);
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
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ProductSaleModel model = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(ID);
            frmProductDetailSale frm = new frmProductDetailSale();
            frm.oProductSaleModel = model;
            frm.maker = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMaker));
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
            bool isFix = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsFix));
            if (isFix && !Global.IsAdmin)
            {
                MessageBox.Show(string.Format("Vật tư [{0}] đã chuẩn hóa không được phép sửa, xóa", productName), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] : [{1}] không?", productCode, productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //ProductSaleBO.Instance.Delete(ID);

                var myDict = new Dictionary<string, object>()
                {
                    { "IsDeleted",true },
                    { "UpdatedDate",DateTime.Now },
                    { "UpdatedBy",Global.AppUserName },
                };

                SQLHelper<ProductSaleModel>.UpdateFieldsByID(myDict, ID);
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


        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //editGrv = 1;
            //int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //ProductSaleModel model = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(ID);
            //frmChiTietSanPhamSale frm = new frmChiTietSanPhamSale();
            //frm.oProductSaleModel = model;
            //frm.WarehouseCode = "HN";
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadGrdData();
            //}
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
            //if (TextUtils.ToInt(arrSv) == 1)
            //{
            //    grdData.Visible = false;
            //    tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
            //    Show = 0;
            //}
            //else
            //{
            //    tableLayoutPanel2.SetColumnSpan(grdMaster, 1);
            //    grdData.Visible = true;
            //    Show = 1;
            //}

        }

        private void frmProductSale_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Show == 0)
                File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "0");
            else
                File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "1");

        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void treeData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {

        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            bool isVisible = TextUtils.ToBoolean(treeData.GetRowCellValue(e.Node, colIsVisible));
            if (!isVisible)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void btnUpdateProductNewCode_Click(object sender, EventArgs e)
        {
            string productNewCodeLatest = txtLatestCode.Text.Trim();
            string productGroupCode = TextUtils.ToString(treeData.GetFocusedRowCellValue(colProductGroupID));
            int productGroupID = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

            //if (string.IsNullOrWhiteSpace(productNewCodeLatest))
            //{
            //    MessageBox.Show("Vui lòng nhập Code mới nhất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(productGroupCode))
            //{
            //    MessageBox.Show("Không tìm thấy mã nhóm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (!productNewCodeLatest.StartsWith(productGroupCode))
            //{
            //    MessageBox.Show("Code mới nhất phải bắt đầu bằng mã nhóm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            int[] rowIndexs = grvMaster.GetSelectedRows();
            if (rowIndexs.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn cập nhật Mã nội bộ cho các sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            using (WaitDialogForm fWait = new WaitDialogForm("Updating ........ =)) ........", ""))
            {

                foreach (int row in rowIndexs)
                {
                    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(row, colID));
                    string newCode = GetProductNewCode(productGroupCode + "0", productGroupID);

                    var myDict = new Dictionary<string, object>()
                    {
                        { "ProductNewCode",newCode},
                        { "UpdatedDate",DateTime.Now},
                        { "UpdatedBy",Global.AppCodeName}
                    };

                    SQLHelper<ProductSaleModel>.UpdateFieldsByID(myDict, id);
                }

                //List<ProductSaleModel> lstModel = new List<ProductSaleModel>();
                //foreach (int rowIndex in rowIndexs)
                //{
                //    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(rowIndex, colID));
                //    ProductSaleModel model = SQLHelper<ProductSaleModel>.FindByID(id);
                //    if (model.ID > 0 && !lstModel.Contains(model)) lstModel.Add(model);
                //}

                //lstModel = lstModel.OrderBy(p => p.ID).ToList();
                //int numberLatest = TextUtils.ToInt(productNewCodeLatest.Substring(productGroupCode.Length)) + 1;

                //foreach (ProductSaleModel item in lstModel)
                //{
                //    string numberCodeText = numberLatest.ToString();
                //    while ((numberCodeText.Length + productGroupCode.Length) < 9)
                //    {
                //        numberCodeText = "0" + numberCodeText;
                //    }


                //    item.ProductNewCode = $"{productGroupCode}{numberCodeText}";
                //    SQLHelper<ProductSaleModel>.Update(item);

                //    numberLatest++;

                //}
            }

            MessageBox.Show("Update thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadGrdData();
        }

        string GetProductNewCode(string productGroupCode, int productGroupID)
        {
            string productNewCode = "";

            //GET DANH SÁCH SP THEO LOẠI KHO
            //DataRowView productGroup = (DataRowView)cboGroup.GetSelectedDataRow();
            //if (productGroup == null) return productNewCode;
            //int productGroupID = TextUtils.ToInt(productGroup["ID"]);
            //string productGroupCode = TextUtils.ToString(productGroup["ProductGroupID"]).Trim();

            List<ProductSaleModel> listProducts = SQLHelper<ProductSaleModel>.FindByAttribute("ProductGroupID", productGroupID);
            var listNewCodes = listProducts.Where(x => x.ProductNewCode.StartsWith(productGroupCode))
                                           .Select(x => new
                                           {
                                               ID = x.ID,
                                               ProductNewCode = x.ProductNewCode,
                                               STT = string.IsNullOrWhiteSpace(x.ProductNewCode) ? 0 : TextUtils.ToInt(x.ProductNewCode.Substring(productGroupCode.Length)),
                                           }).ToList();

            int numberCode = listNewCodes.Count <= 0 ? 0 : listNewCodes.Max(x => x.STT);
            string numberCodeText = (++numberCode).ToString();

            while ((numberCodeText.Length + productGroupCode.Length) < 9)
            {
                numberCodeText = "0" + numberCodeText;
            }
            productNewCode = $"{productGroupCode}{numberCodeText}";
            return productNewCode;
        }

        private void btnRequestQuote_Click(object sender, EventArgs e)
        {
            frmProjectPartlistPriceRequestNew frm = new frmProjectPartlistPriceRequestNew(4);
            frm.Show();
        }

        private void btnPurchaseRequest_Click(object sender, EventArgs e)
        {
            //if (treeData.GetFocusedRowCellValue(colProductGroupID).ToString().ToUpper() != "MK")
            //{
            //    MessageBox.Show(string.Format("Vui lòng chọn sản phầm thuộc nhóm Marketing!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New_Khanh", "A",
                                               new string[] { "@DateStart", "@DateEnd" },
                                               new object[] { new DateTime(2000, 1, 1), new DateTime(2000, 1, 1) });
            DataTable dtClone = dt.Clone();
            dtClone.Columns.Add("STT");
            int countSTT = 0;


            var unitCounts = SQLHelper<UnitCountModel>.FindAll();
            foreach (int i in grvMaster.GetSelectedRows())
            {
                countSTT++;
                DataRow dr = dtClone.NewRow();

                string unitName = TextUtils.ToString(grvMaster.GetRowCellValue(i, colUnit));
                UnitCountModel unitCount = unitCounts.Where(x => x.UnitName.ToLower().Trim() == unitName.ToLower().Trim()).FirstOrDefault() ?? new UnitCountModel();
                dr["STT"] = countSTT;
                dr["ProductCode"] = grvMaster.GetRowCellValue(i, colProductCode).ToString();
                dr["ProductNewCode"] = grvMaster.GetRowCellValue(i, colProductNewCode).ToString();
                dr["ProductName"] = grvMaster.GetRowCellValue(i, colProductName).ToString();
                dr["UnitName"] = unitCount.ID;
                dr["Manufacturer"] = grvMaster.GetRowCellValue(i, colMaker).ToString();
                dr["ProductGroupID"] = treeData.Selection[0].GetValue(treeData.Columns["ID"]);
                dtClone.Rows.Add(dr);
            }
            frmProjectPartlistPurchaseRequestDetaiNew frm = new frmProjectPartlistPurchaseRequestDetaiNew(7);
            frm.dt = dtClone;
            //frm.requestTypeID = 7;
            frm.ShowDialog();
        }
    }

}


