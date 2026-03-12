using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInventoryDemo : _Forms
    {
        int warehouseID = 0;

        private int _hoverRow = -1;
        private GridCellInfo _oldCellInfo = null;

        public frmInventoryDemo(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmInventoryDemo_Load(object sender, EventArgs e)
        {
            this.Text += $" - {this.Tag}";
            LoadProductGroup();
            LoadLocation();
            LoadData();
        }

        void LoadProductGroup()
        {
            //DataTable dt = TextUtils.Select($"SELECT * FROM dbo.ProductGroupRTC WHERE WarehouseID = 1 and ProductGroupNo NOT IN ('DBH','CCDC') ORDER BY NumberOrder");

            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH","NOT LIKE");
            var exp3 = new Expression("ProductGroupNo", "CCDC","<>");

            var list = SQLHelper< ProductGroupRTCModel>.FindByExpression(exp1.And(exp2).And(exp3));
            treeData.DataSource = list;
        }

        void LoadLocation()
        {
            DataTable dt = TextUtils.Select($"SELECT * FROM ProductLocation WHERE WarehouseID = {warehouseID}");
            cbLocation.Properties.DataSource = dt;
            cbLocation.Properties.DisplayMember = "LocationName";
            cbLocation.Properties.ValueMember = "ID";
        }

        void LoadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
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

                DataTable dtProduct = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
                                    new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                    new object[] { productGroupID, txtFilterText.Text.Trim(), allProduct, warehouseID });
                grdData.DataSource = dtProduct;
            }
            //    try
            //{
            //    MyLib.ShowWaitForm("Load Data.........");


            //    //string filterString = $"Contains([ProductName],'VL')";
            //    //grvData.Columns["ProductName"].FilterInfo = new ColumnFilterInfo(filterString);

            //}
            //finally
            //{
            //    MyLib.CloseWaitForm();
            //}
        }

        void ShowSpec()
        {
            //var id = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));
            int groupId = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

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

            if (groupId == 74)//AREA SCAN CAMERA
            {
                colResolution.Visible = true;
                colResolution.VisibleIndex = 2;
                colResolution.Caption = capRes + " (pixel)";

                colMonoColor.Visible = true;
                colMonoColor.VisibleIndex = 3;
                colSensorSize.Caption = capSensorSize + " (\")";

                colSensorSize.Visible = true;
                colSensorSize.VisibleIndex = 4;

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
            else if (groupId == 78) //LENS TELECENTRIC
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
            else if (groupId == 81)//LENS FIX FOCAL LENGTH 
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

        void loadImage(string imageName)
        {
            try
            {
                var request = WebRequest.Create("http://113.190.234.64:8083/api/Upload/Images/ProductDemo/" + imageName);
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

        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            txtFilterText.Clear();
            cboCheckAll.SetEditValue("");
            grvData.ActiveFilter.Clear();

            LoadData();
            ShowSpec();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Files|*.xls";
            dialog.FileName = $"DanhSachThietBi_{DateTime.Now.ToString("ddMMyy")}.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/DanhSachThietBi_{DateTime.Now.ToString("ddMMyy")}.xls";
                    string filepath = dialog.FileName;
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboCheckAll_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
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
                int productID = TextUtils.ToInt(grvData.GetRowCellValue(item, colProductRTCID));
                LstProductLocationID.Add(productID);
            }

            if (MessageBox.Show("Bạn có chắc muốn set vị trí này cho tất cả các thiết \nbị được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string lstID = string.Join(";", LstProductLocationID);
                TextUtils.ExcuteProcedure("spUpdateLocationProduct", new string[] { "@ListProductID", "@ProductLocationID" }, new object[] { lstID, cbLocation.EditValue });
            }

            LoadData();
            LstProductLocationID.Clear();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string NumberDauKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumber));
            string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colInventoryLate));
            string Import = TextUtils.ToString(grvData.GetFocusedRowCellValue(colImport));
            string Export = TextUtils.ToString(grvData.GetFocusedRowCellValue(colExport));
            string Borrowing = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBorrowing));
            string NumberReal = TextUtils.ToString(grvData.GetFocusedRowCellValue(colInventoryReal));
            if (ID == 0) return;
            //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            ProductRTCModel model = SQLHelper< ProductRTCModel >.FindByID(ID);
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

        int[] listBillType = new int[] { 0, 2, 3, 5 };
        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int billType = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillType));
                int numberInStore = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colInventoryLate));

                if (e.Column == colProductCode || e.Column == colProductName)
                {
                    if (listBillType.Contains(billType) && numberInStore == 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (billType == 7 && numberInStore == 0)
                    {
                        e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffd3dd");
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnAddQRCode_Click(object sender, EventArgs e)
        {
            //frmAddQRCode frm = new frmAddQRCode(warehouseID);
            //frm.Tag = this.Tag;
            //frm.Show();

            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
            if (id <= 0) return;
            frmAddQRCodeDetail frm = new frmAddQRCodeDetail(warehouseID);
            frm.Edit = true;
            frm.id = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
            grvData.FocusedRowHandle = rowHandle;
        }

        private void btnBorrowNCCReport_Click(object sender, EventArgs e)
        {
            frmInventoryBorrowNCCDemo frm = new frmInventoryBorrowNCCDemo();
            frm.warehouseID = 1;
            frm.ShowDialog();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
            if (ID == 0) return;
            ProductRTCModel model = SQLHelper<ProductRTCModel>.FindByID(ID);
            frmProductDetailRTC frm = new frmProductDetailRTC(warehouseID);
            frm.oProductRTCModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }
    }
}
