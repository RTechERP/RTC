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
using static Forms.Classes.DelegateEvents;
using System.Net;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Columns;
using BMS.Utils;
//using Forms.TB.Borrow;
using DevExpress.XtraRichEdit.Model;
using DevExpress.Utils.Extensions;

namespace BMS
{
    public partial class frmProductRTC : _Forms
    {
        public event ChildFormAcivated OnChildFormAcivated;
        public event ChildFormClosed OnChildFormClosed;

        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        DataTable _dtName = new DataTable();
        DataTable dttt = new DataTable();

        DataSet dataSet = new DataSet();
        public int warehouseID;
        //public DataTable dt = new DataTable();
        public ProductGroupRTCModel name = new ProductGroupRTCModel();
        string _pathFileBackUp = Path.Combine(Application.StartupPath, "BackUp.txt");

        public frmProductRTC()
        {
            InitializeComponent();
            if (!File.Exists(_pathFileBackUp))
            {
                File.WriteAllText(_pathFileBackUp, @"D:\SQL BackUp");
            }
        }
        public frmProductRTC(int WarehouseID)
        {
            InitializeComponent();
            if (!File.Exists(_pathFileBackUp))
            {
                File.WriteAllText(_pathFileBackUp, @"D:\SQL BackUp");
            }
            warehouseID = WarehouseID;
        }

        private void frmProductRTC_Load(object sender, EventArgs e)
        {
            loadLocation();
            loadData();
            //repositoryItemImageEdit1.Images = "https://khodohoa.vn/wp-content/uploads/2019/09/43087481675_3969d2e92c_o.jpg";
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");

            grdData.ContextMenuStrip = contextMenuStrip2;
        }

        private void loadData()
        {
            //dt = TextUtils.Select($"SELECT * FROM dbo.ProductGroupRTC WHERE WarehouseID = 1 AND ProductGroupNo NOT IN ('DBH','CCDC') ORDER BY NumberOrder");


            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH", "NOT LIKE");
            var exp3 = new Expression("ProductGroupNo", "CCDC", "<>");

            List<ProductGroupRTCModel> list = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2).And(exp3));

            if (Global.IsAdmin && Global.EmployeeID <= 0)
            {
                list = SQLHelper<ProductGroupRTCModel>.FindAll();
            }

            treeData.DataSource = list;
        }
        void loadLocation()
        {
            DataTable dt = TextUtils.Select($"SELECT * FROM ProductLocation WHERE WarehouseID = {warehouseID}");
            cbLocation.Properties.DataSource = dt;
            cbLocation.Properties.DisplayMember = "LocationName";
            cbLocation.Properties.ValueMember = "ID";
        }

        void showSpec(int groupId)
        {
            //var id = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

            colResolution.Visible = false;
            colMonoColor.Visible = false;
            colSensorSize.Visible = false;
            colDataInterface.Visible = false;
            colLensMount.Visible = false;
            colShutterMode.Visible = false;
            colPixelSize.Visible = false;
            colSensorSizeMax.Visible = false;
            colMOD.Visible = false;
            colFNo.Visible = false;
            colWD.Visible = false;

            colLampType.Visible = false;
            colLampColor.Visible = false;
            colLampPower.Visible = false;
            colLampWattage.Visible = false;

            colMagnification.Visible = false;
            colFocalLength.Visible = false;

            //TODO : HuyNT update 13/09/2024
            colInputValue.Visible = false;
            colOutputValue.Visible = false;
            colCurrentIntensityMax.Visible = false;

            string capRes = "Resolution";
            string capSensorSize = "Sensor Size";
            string capSensorSizeMax = "Sensor Size Max";

            if (groupId == 74) //AREA SCAN CAMERA
            {
                colResolution.Visible = true;
                colResolution.VisibleIndex = 2;
                colResolution.Caption = capRes + " (pixel)";

                colMonoColor.Visible = true;
                colMonoColor.VisibleIndex = 3;

                colSensorSize.Visible = true;
                colSensorSize.VisibleIndex = 4;
                colSensorSize.Caption = capSensorSize + " (\")";

                colDataInterface.Visible = true;
                colDataInterface.VisibleIndex = 5;

                colLensMount.Visible = true;
                colLensMount.VisibleIndex = 6;

                colShutterMode.Visible = true;
                colShutterMode.VisibleIndex = 7;
            }
            else if (groupId == 75)//VISION LIGHT
            {
                colLampType.Visible = !colLampType.Visible;
                colLampType.VisibleIndex = 2;

                colLampColor.Visible = true;
                colLampColor.VisibleIndex = 3;

                colLampPower.Visible = false;
                colLampPower.VisibleIndex = 4;

                colLampWattage.Visible = false;
                colLampWattage.VisibleIndex = 5;
            }
            else if (groupId == 78)//LENS TELECENTRIC
            {
                colResolution.Visible = true;
                colResolution.VisibleIndex = 2;
                colResolution.Caption = capRes + " (µm)";

                colSensorSizeMax.Visible = true;
                colSensorSizeMax.VisibleIndex = 3;
                colSensorSizeMax.Caption = capSensorSizeMax + " (\")";

                colWD.Visible = true;
                colWD.VisibleIndex = 4;

                colLensMount.Visible = true;
                colLensMount.VisibleIndex = 5;

                colFNo.Visible = true;
                colFNo.VisibleIndex = 6;

                colMagnification.VisibleIndex = 1;
            }
            else if (groupId == 79) //LINE SCAN CAMERA
            {
                colResolution.Visible = true;
                colResolution.VisibleIndex = 1;

                colMonoColor.Visible = true;
                colMonoColor.VisibleIndex = 2;

                colPixelSize.Visible = true;
                colPixelSize.VisibleIndex = 3;

                colDataInterface.Visible = true;
                colDataInterface.VisibleIndex = 4;

                colLensMount.Visible = true;
                colLensMount.VisibleIndex = 5;
            }
            else if (groupId == 81 /*|| groupId == 139*/) //LENS FIX FOCAL LENGTH
            {
                //colResolution.Visible = true;
                colResolution.VisibleIndex = 2;
                colResolution.Caption = capRes + " (µm)";

                //colSensorSizeMax.Visible = true;
                colSensorSizeMax.VisibleIndex = 3;
                colSensorSizeMax.Caption = capSensorSizeMax + " (\")";

                //colMOD.Visible = true;
                colMOD.VisibleIndex = 4;

                //colLensMount.Visible = true;
                colLensMount.VisibleIndex = 5;

                //colFNo.Visible = true;
                colFNo.VisibleIndex = 6;

                colFocalLength.VisibleIndex = 1;
            }
            else if (groupId == 139) //LENS SPECIAL
            {
                //colResolution.Visible = true;
                colResolution.VisibleIndex = 2;
                //colResolution.Caption = colResolution.Caption + " (µm)";

                //colSensorSizeMax.Visible = true;
                colSensorSizeMax.VisibleIndex = 3;
                //colSensorSizeMax.Caption = colSensorSizeMax.Caption + " (\")";

                //colMOD.Visible = true;
                colMOD.VisibleIndex = 4;

                //colLensMount.Visible = true;
                colLensMount.VisibleIndex = 5;

                //colFNo.Visible = true;
                colFNo.VisibleIndex = 6;

                colFocalLength.VisibleIndex = 1;
            }
            //TODO : HuyNT update 13/09/2024
            else if (groupId == 92) //NGUON DC
            {
                //colInputValue.Visible = true;
                colInputValue.VisibleIndex = 2;
                //colOutputValue.Visible = true;
                colOutputValue.VisibleIndex = 3;
                //colCurrentIntensityMax.Visible = true;
                colCurrentIntensityMax.VisibleIndex = 4;
            }
        }
        /// <summary>
        /// load dữ liệu vào bảng grdData khi thay đổi tại bảng treeData
        /// </summary>
        //public string GroupName;
        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            txtFilterText.Clear();
            cboCheckAll.SetEditValue("");
            grvData.ActiveFilter.Clear();

            int id = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));
            showSpec(id);
            findData();


        }
        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmProductGroupRTCDetail frm = new frmProductGroupRTCDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
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
            ProductGroupRTCModel model = (ProductGroupRTCModel)ProductGroupRTCBO.Instance.FindByPK(ID);
            frmProductGroupRTCDetail frm = new frmProductGroupRTCDetail(warehouseID);
            frm.Group = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
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
            int groupID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            string groupName = TextUtils.ToString(treeData.FocusedNode.GetValue(colNameTree));
            if (groupID == 0) return;

            if (ProductRTCBO.Instance.CheckExist("ProductGroupRTCID", groupID))
            {
                MessageBox.Show(string.Format("Nhóm thiết bị đang chứa thiết bị, không thể xóa nhóm thiết bị [{0}] khỏi danh sách!!", groupName), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhóm [{0}] hay không ?", groupName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductGroupRTCBO.Instance.Delete(groupID);
                treeData.DeleteNode(treeData.FocusedNode);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeleteGroup_Click(null, null);
        }

        //--------------- code phần sản phẩm, thiết bị ------------------------------------
        /// <summary>
        /// creat tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            string groupName = TextUtils.ToString(treeData.FocusedNode.GetValue(colNameTree));
            if (ID == 0) return;
            ProductRTCModel model = new ProductRTCModel();
            model.ProductGroupRTCID = ID;
            frmProductDetailRTC frm = new frmProductDetailRTC(warehouseID);
            frm.oProductRTCModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
                findData();
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
        public static int editGrv = 0;
        private void editDataProduct()
        {
            editGrv = 1;
            var rowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            ProductRTCModel model = SQLHelper<ProductRTCModel>.FindByID(ID);
            frmProductDetailRTC frm = new frmProductDetailRTC(1);
            frm.oProductRTCModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                //loadGrdData();
                findData();
                grvData.FocusedRowHandle = rowHandle;
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
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (ID == 0) return;

            var exp1 = new Expression("ProductRTCID", ID);
            var exp2 = new Expression("IsDelete", 1, "<>");
            var checkHistory = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2));
            if (/*HistoryProductRTCBO.Instance.CheckExist("ProductRTCID", ID)*/checkHistory.Count > 0)
            {
                MessageBox.Show("Sản phẩm này đã tồn tại trong lịch sử mượn, không thể xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] : [{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //ProductRTCBO.Instance.Delete(ID);
                ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByID(ID);
                if (product == null) return;
                product.IsDelete = true;
                SQLHelper<ProductRTCModel>.Update(product);

                grvData.DeleteSelectedRows();
            }
        }
        /// <summary>
        /// find data in DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            findData();
            //grvData.RowStyle += grvData_RowStyle;
            //grvData.FocusedRowChanged += grvData_FocusedRowChanged;
            grvData_FocusedRowChanged(null, null);
        }


        /// <summary>
        /// event editData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            //if (Global.IsAdmin)
            editDataProduct();
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
                findData();
            }
        }
        /// <summary>
        /// hàm tìm kiếm sản phẩm
        /// </summary>
        private void findData()
        {
            try
            {
                MyLib.ShowWaitForm("Load Data.........");
                int allProduct = 0;
                int productGroupID = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

                string valueSelect = TextUtils.ToString(cboCheckAll.EditValue);

                if (!string.IsNullOrEmpty(valueSelect))
                {
                    string[] selected = valueSelect.Split(',');

                    foreach (string item in selected)
                    {
                        if (TextUtils.ToInt(item) == 0)
                        {
                            productGroupID = 0;
                        }

                        if (TextUtils.ToInt(item) == 1)
                        {
                            allProduct = TextUtils.ToInt(item);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtFilterText.Text.Trim()))
                {
                    productGroupID = 0;
                    allProduct = 1;
                }

                DataTable dtProduct = TextUtils.LoadDataFromSP("spGetProductRTC", "A",
                                    new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                    new object[] { productGroupID, txtFilterText.Text.Trim(), allProduct, warehouseID });

                //if (dtProduct.Rows[0]["ProductName"].ToString() != "AREA LIGHT")
                //    dtProduct.Rows[0]["ProductName"] = "AREA LIGHT";

                grdData.DataSource = dtProduct;

                //string _data = TextUtils.ToString(txtFilterText.Text.Trim());
                //if (_data == "")
                //{
                //    loadGrdData();
                //}
                //else
                //{
                //    DataTable dt = new DataTable();
                //    dt = TextUtils.LoadDataFromSP("spFindProductRTC_New", "A", new string[] { "@Find" }, new object[] { _data });//spFindProductRTC

                //    DataColumn data = new DataColumn("Location", typeof(Byte[]));
                //    dt.Columns.Add(data);

                //    grdData.DataSource = dt;
                //    //LoadDataImg(dt);

                //}
            }
            finally
            {
                MyLib.CloseWaitForm();
            }

        }
        async void LoadDataImg(DataTable dt)
        {
            Task task = Task.Factory.StartNew(() =>
              {
                  try
                  {
                      this.Invoke((MethodInvoker)delegate
                      {
                          for (int i = 0; i < dt.Rows.Count; i++)
                          {
                              string location1 = TextUtils.ToString(dt.Rows[i]["LocationImg"]);
                              if (location1 == "")
                                  continue;
                              dt.Rows[i]["Location"] = Lib.FileImgToByte(location1);
                              // grvData.SetRowCellValue(i, "Location", (byte[])Lib.FileImgToByte(location1));
                          }
                      });
                  }
                  catch (Exception)
                  {

                  }

              });
            grdData.DataSource = dt;
            await task;

        }

        private void btnQRcode_Click(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            //frm.oProductRTCModel = model;
            frmProductQRCode frm = new frmProductQRCode();
            List<string> lst = new List<string>();
            int[] lstIndex = grvData.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                lst.Add(grvData.GetRowCellValue(lstIndex[i], colProductCode).ToString() + ";" + grvData.GetRowCellValue(lstIndex[i], colAddress).ToString() + ";" + grvData.GetRowCellValue(lstIndex[i], colNote).ToString());
            }
            frm.LST = lst;
            frm.ShowDialog();
        }

        private void btnCheckProduct_Click(object sender, EventArgs e)
        {
            frmProductCheck frm = new frmProductCheck();
            frm.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmProductExcel frmExcel = new frmProductExcel();
            frmExcel.ShowDialog();
            loadData();
        }

        private void chkAllProduct_CheckedChanged(object sender, EventArgs e)
        {
            findData();



        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả nhà cung cấp", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int Number = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumber));
                    int NumberInStore = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberInStore));
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) continue;
                    if (Number == 0) continue;
                    if (NumberInStore != 0) continue;
                    ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(id);
                    model.Number = 0;
                    ProductRTCBO.Instance.Update(model);
                }
            }
            //loadGrdData();

            findData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //frmImportBox frm = new frmImportBox();
            //frm.Show();

            frmImportChangeProductCamORLenInfo frm = new frmImportChangeProductCamORLenInfo();
            //frmImportExcelProduct frm = new frmImportExcelProduct();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                findData();
            }
        }




        private void Img_Click(object sender, EventArgs e)
        {
            string a = "";

            for (int i = 0; i < dttt.Rows.Count; i++)
            {
                try
                {
                    int ID = TextUtils.ToInt(dttt.Rows[i]["ID"]);
                    if (ID == 0) return;
                    ProductRTCModel p = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);

                    if (p.LocationImg == null || p.LocationImg == "")
                    {
                        //Image image = Lib.ByteToImg(p.ImagePath);
                        //if (image == null)
                        //{
                        //	continue;
                        //}
                        a = p.ProductCode;
                        if (p.ProductCode == "155219080200006AA")
                        {

                        }
                        //giảm hình ảnh 
                        //Image ifirst = Image.FromFile(Lib.ImgtoByte(pictureBox1.Image));
                        //Image iresize = TextUtils.Resize(image, 0.5F);
                        //iresize.Save($"{p.ProductCode}.jpg");
                        p.LocationImg = @"\\192.168.1.168\ftp\Anh\IMG\" + $"{p.ProductCode}.jpg";
                        if (p.ID > 0)
                        {
                            ProductRTCBO.Instance.Update(p);
                            DocUtils.UploadFile(Path.Combine(Application.StartupPath, $"{p.ProductCode}.jpg"), @"Anh\IMG");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"{a}", "", MessageBoxButtons.OK);
                }

            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            string Text = $@"{File.ReadAllText(_pathFileBackUp)}\RTC-{DateTime.Now.ToString("dd-MM-yyyy")}.bak";
            TextUtils.ExcuteProcedure("spBackUp", new string[] { "@text" }, new object[] { Text });
        }




        private void btnUpdateProductGroup_Click(object sender, EventArgs e)
        {
            frmImportChangeProductGroup frm = new frmImportChangeProductGroup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadData();
                //loadGrdData();
            }
        }

        int _RowIndex;
        private void btnAddQRCode_Click(object sender, EventArgs e)
        {
            _RowIndex = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            frmAddQRCodeDetail frm = new frmAddQRCodeDetail(warehouseID);
            frm.Edit = true;
            frm.id = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
            grvData.FocusedRowHandle = _RowIndex;
        }


        //Update
        //TOÁN: 09112022
        //Chi tiết phiếu mượn nhập xuất
        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string NumberDauKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumber));
            string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colInventoryLate));
            string Import = TextUtils.ToString(grvData.GetFocusedRowCellValue(colImport));
            string Export = TextUtils.ToString(grvData.GetFocusedRowCellValue(colExport));
            string Borrowing = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBorrowing));
            string NumberReal = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberReal));
            if (ID == 0) return;
            //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            ProductRTCModel model = SQLHelper<ProductRTCModel>.FindByID(ID);
            frmMaterialDetailOfProductRTC frm = new frmMaterialDetailOfProductRTC(warehouseID);
            frm.ProductRTCID = model.ID;
            frm.ProductName = ProductName;
            frm.ProductCode = ProductCode;
            frm.NumberDauKy = NumberDauKy;
            frm.NumberCuoiKy = NumberCuoiKy;
            frm.NumberReal = NumberReal;
            frm.Borrowing = Borrowing;
            frm.Import = Import;
            frm.Export = Export;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void frmProductRTC_Activated(object sender, EventArgs e)
        {
            if (OnChildFormAcivated != null)
                OnChildFormAcivated(this);
        }

        private void frmProductRTC_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnChildFormClosed != null)
                OnChildFormClosed(this);
        }




        private void grdData_Click(object sender, EventArgs e)
        {

        }
        void loadImage(string imageName)
        {
            try
            {
                var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/Images/ProductDemo/" + imageName);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Width = 400;
                pictureBox1.Height = 400;

            }
            catch (Exception)
            {
                return;
            }
        }
        private int _hoverRow = -1;
        private GridCellInfo _oldCellInfo = null;
        private void grvData_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridViewInfo viewInfo = (grvData.GetViewInfo() as GridViewInfo);
                if (viewInfo == null)
                {
                    pictureBox1.Visible = false;
                    return;
                }

                GridHitInfo gridHitInfo = grvData.CalcHitInfo(e.Location);
                GridCellInfo cellInfo = viewInfo.GetGridCellInfo(gridHitInfo);
                if (gridHitInfo != null && gridHitInfo.RowHandle >= 0 && _hoverRow != gridHitInfo.RowHandle && gridHitInfo.Column == colLocationImg)
                {
                    //string A = TextUtils.ToString(sender);
                    //grvQuestion.SelectRow(gridHitInfo.RowHandle);
                    //grvQuestion.FocusedRowHandle = gridHitInfo.RowHandle;
                    //if (gridHitInfo.Column != colContentTest)
                    //    return;

                    //if (_oldCellInfo != null)
                    //    _oldCellInfo.Appearance.BackColor = Color.White;
                    //_oldCellInfo = null;

                    string locationImage = TextUtils.ToString(grvData.GetRowCellValue(gridHitInfo.RowHandle, colLocationImg));

                    if (!string.IsNullOrEmpty(locationImage))
                    {
                        if (_oldCellInfo != null)
                            _oldCellInfo.Appearance.BackColor = Color.White;
                        _oldCellInfo = cellInfo;
                        _hoverRow = gridHitInfo.RowHandle;
                        pictureBox1.Location = new Point(cellInfo.Bounds.Width + 520, grdData.Location.Y + grvData.ColumnPanelRowHeight + 50);
                        pictureBox1.Visible = true;
                        cellInfo.Appearance.BackColor = Color.Yellow;

                        loadImage(locationImage);
                        return;
                    }
                }
                else if (gridHitInfo != null && _hoverRow == gridHitInfo.RowHandle)
                    return;

                if (_oldCellInfo != null)
                    _oldCellInfo.Appearance.BackColor = Color.White;
                _oldCellInfo = null;
                pictureBox1.Visible = false;
            }
            finally
            {
                if (!pictureBox1.Visible)
                {
                    if (_oldCellInfo != null)
                        _oldCellInfo.Appearance.BackColor = Color.White;
                    _oldCellInfo = null;
                }
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            var rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Bạn chưa chọn thiết bị. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            List<int> LstProductLocationID = new List<int>();
            foreach (var item in rowSelected)
            {
                int productID = TextUtils.ToInt(grvData.GetRowCellValue(item, colID));
                LstProductLocationID.Add(productID);
            }

            if (MessageBox.Show("Bạn có chắc muốn set vị trí này cho tất cả các thiết \nbị được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string lstID = string.Join(";", LstProductLocationID);
                TextUtils.ExcuteProcedure("spUpdateLocationProduct", new string[] { "@ListProductID", "@ProductLocationID" }, new object[] { lstID, cbLocation.EditValue });
            }

            findData();
            LstProductLocationID.Clear();
        }

        private void cboCheckAll_EditValueChanged(object sender, EventArgs e)
        {
            findData();
        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xls";
            f.FileName = $"DanhSachThietBi_{DateTime.Now.ToString("ddMMyy")}.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/DanhSachThietBi_{DateTime.Now.ToString("ddMMyy")}.xls";
                    string filepath = f.FileName;
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }

        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.RowHandle >= 0)
            {
                int billType = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillType));
                int numberInStore = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colInventoryLate));

                if (e.Column == colProductCode || e.Column == colProductName)
                {
                    if ((billType == 0 || billType == 2) && numberInStore == 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            bool eCancel = true;
            if (grvData.SelectedRowsCount > 0)
            {
                List<ToolStripMenuItem> removeItems = new List<ToolStripMenuItem>();
                foreach (ToolStripMenuItem item in contextMenuStrip2.Items)
                {
                    
                    if (item.Tag != null && item.Tag.GetType() == typeof(GridPopupMenuItemTag))
                    {
                        GridPopupMenuItemTag itemTag = (GridPopupMenuItemTag)item.Tag;
                        if (itemTag.ItemType == "LocDong")
                            removeItems.Add(item);
                    }
                }

                for (int i = 0; i < removeItems.Count; i++)
                {
                    contextMenuStrip2.Items.Remove(removeItems[i]);
                }


                if (grvData.FocusedColumn == colMaker)
                {
                    string cellValue = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colMaker)).Trim();
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        GridPopupMenuItemTag itemTag = new GridPopupMenuItemTag
                        {
                            ItemType = "LocDong",
                            ColumnIndex = grvData.FocusedColumn.VisibleIndex,
                            RowIndex = grvData.FocusedRowHandle,
                            ColumnFieldName = grvData.FocusedColumn.FieldName
                        };

                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = cellValue;
                        item.Tag = itemTag;
                        item.Click -= GridControlPopupMenuItemClickEvents;
                        item.Click += GridControlPopupMenuItemClickEvents;
                        contextMenuStrip2.Items.Add(item);
                        eCancel = false;
                    }
                }
                else
                {
                    string cellValue = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, grvData.FocusedColumn)).Trim();

                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        string[] cellValues = cellValue.Split(' ');
                        cellValues = cellValues.Distinct().ToArray();
                        foreach (string s in cellValues)
                        {
                            GridPopupMenuItemTag itemTag = new GridPopupMenuItemTag
                            {
                                ItemType = "LocDong",
                                ColumnIndex = grvData.FocusedColumn.VisibleIndex,
                                RowIndex = grvData.FocusedRowHandle,
                                ColumnFieldName = grvData.FocusedColumn.FieldName
                            };

                            ToolStripMenuItem item = new ToolStripMenuItem();
                            item.Text = s.Trim();
                            item.Tag = itemTag;
                            item.Click -= GridControlPopupMenuItemClickEvents;
                            item.Click += GridControlPopupMenuItemClickEvents;
                            contextMenuStrip2.Items.Add(item);
                            eCancel = false;
                        }
                    }
                }
            }

            e.Cancel = eCancel;
        }

        public void GridControlPopupMenuItemClickEvents(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            GridPopupMenuItemTag itemTag = (GridPopupMenuItemTag)item.Tag;
            switch (itemTag.ItemType)
            {
                case "LocDong":
                    {
                        string filterString = $"Contains([{itemTag.ColumnFieldName}],'{item.Text}')";
                        grvData.Columns[itemTag.ColumnIndex].FilterInfo = new ColumnFilterInfo(filterString);
                        break;
                    }
            }
        }

        public class GridPopupMenuItemTag
        {
            public string ItemType { get; set; }
            public int ColumnIndex { get; set; }
            public int RowIndex { get; set; }
            public string ColumnFieldName { get; set; }

        }

        private void clearFillterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grvData.ClearColumnsFilter();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //int groupId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductGroupRTCID));
            //showSpec(groupId);
        }

        private void treeData_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.C)
            //{
            //    string value = TextUtils.ToString(treeData.GetFocusedRowCellValue(treeData.FocusedColumn));
            //    Clipboard.SetText(value);
            //    e.Handled = true;
            //}
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            frmProductRTCPurchaseRequest frm = new frmProductRTCPurchaseRequest(id);
            frm.Show();
        }

        private void btnPuchaseRequestDemo_Click(object sender, EventArgs e)
        {
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            if (string.IsNullOrWhiteSpace(productCode)) return;

            frmProjectPartlistPurchaseRequest frm = new frmProjectPartlistPurchaseRequest();
            frm.isPurchaseRequestDemo = true;
            frm.productCode = productCode;
            frm.Show();
        }

        private void btnShowRequestBuy_Click(object sender, EventArgs e)
        {
            frmPurchaseRequestDemo frm = new frmPurchaseRequestDemo();
            frm.Show();
        }
    }
}


