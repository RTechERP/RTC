using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInventoryDemoProtectiveGear : _Forms
    {
        public int warehouseID = 5;
        private int _hoverRow = -1;
        private GridCellInfo _oldCellInfo = null;
        public frmInventoryDemoProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmInventoryDemoProtectiveGear_Load(object sender, EventArgs e)
        {
            //this.Text += $" - {this.Tag}";
            LoadProductGroup();
            LoadData();
        }

        void LoadProductGroup()
        {
            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH");
            var list = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2)).ToList();
            grdDataGroup.DataSource = list;
        }

        void LoadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                int allProduct = 1;
                int productGroupID = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue("ID"));

                /* string valueSelect = TextUtils.ToString(cboCheckAll.EditValue);

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
                 }*/

                //if (!string.IsNullOrEmpty(txtFilterText.Text.Trim()))
                //{
                //    productGroupID = 0;
                //    allProduct = 1;
                //}

                DataTable dtProduct = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
                                    new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                    new object[] { productGroupID, txtKeyword.Text.Trim(), allProduct, warehouseID });
                grdData.DataSource = dtProduct;
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    int billType = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillType));
            //    int numberInStore = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colInventoryLate));

            //    if (e.Column == colProductCode || e.Column == colProductName)
            //    {
            //        //if (listBillType.Contains(billType) && numberInStore == 0)
            //        //{
            //        //    e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
            //        //    e.Appearance.ForeColor = Color.Black;
            //        //}
            //        //else 
            //        if (billType == 7 && numberInStore == 0)
            //        {
            //            e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffd3dd");
            //            e.Appearance.ForeColor = Color.Black;
            //        }
            //    }
            //}
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvData_MouseMove(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    GridViewInfo viewInfo = (grvData.GetViewInfo() as GridViewInfo);
            //    if (viewInfo == null)
            //    {
            //        pictureBox1.Visible = false;
            //        return;
            //    }

            //    GridHitInfo gridHitInfo = grvData.CalcHitInfo(e.Location);
            //    GridCellInfo cellInfo = viewInfo.GetGridCellInfo(gridHitInfo);
            //    if (gridHitInfo != null && gridHitInfo.RowHandle >= 0 && _hoverRow != gridHitInfo.RowHandle && gridHitInfo.Column == colLocationImg)
            //    {
            //        //string A = TextUtils.ToString(sender);
            //        //grvQuestion.SelectRow(gridHitInfo.RowHandle);
            //        //grvQuestion.FocusedRowHandle = gridHitInfo.RowHandle;
            //        //if (gridHitInfo.Column != colContentTest)
            //        //    return;

            //        //if (_oldCellInfo != null)
            //        //    _oldCellInfo.Appearance.BackColor = Color.White;
            //        //_oldCellInfo = null;

            //        string locationImage = TextUtils.ToString(grvData.GetRowCellValue(gridHitInfo.RowHandle, colLocationImg));

            //        if (!string.IsNullOrEmpty(locationImage))
            //        {
            //            if (_oldCellInfo != null)
            //                _oldCellInfo.Appearance.BackColor = Color.White;
            //            _oldCellInfo = cellInfo;
            //            _hoverRow = gridHitInfo.RowHandle;
            //            pictureBox1.Location = new Point(cellInfo.Bounds.Width + 520, grdData.Location.Y + grvData.ColumnPanelRowHeight + 50);
            //            pictureBox1.Visible = true;
            //            cellInfo.Appearance.BackColor = Color.Yellow;

            //            loadImage(locationImage);
            //            return;
            //        }
            //    }
            //    else if (gridHitInfo != null && _hoverRow == gridHitInfo.RowHandle)
            //        return;

            //    if (_oldCellInfo != null)
            //        _oldCellInfo.Appearance.BackColor = Color.White;
            //    _oldCellInfo = null;
            //    pictureBox1.Visible = false;
            //}
            //finally
            //{
            //    if (!pictureBox1.Visible)
            //    {
            //        if (_oldCellInfo != null)
            //            _oldCellInfo.Appearance.BackColor = Color.White;
            //        _oldCellInfo = null;
            //    }
            //}
        }

        //private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    txtFilterText.Clear();
        //    //cboCheckAll.SetEditValue("");
        //    grvData.ActiveFilter.Clear();

        //    LoadData();
        //    ShowSpec();
        //}

        //void ShowSpec()
        //{
        //    //var id = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));
        //    int groupId = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

        //    colResolution.Visible = false;
        //    colMonoColor.Visible = false;
        //    colSensorSize.Visible = false;
        //    colDataInterface.Visible = false;
        //    colLensMount.Visible = false;
        //    colShutterMode.Visible = false;
        //    colPixelSize.Visible = false;
        //    colSensorSizeMax.Visible = false;
        //    colMOD.Visible = false;
        //    colFNo.Visible = false;
        //    colWD.Visible = false;

        //    colLampType.Visible = false;
        //    colLampColor.Visible = false;
        //    colLampPower.Visible = false;
        //    colLampWattage.Visible = false;

        //    colMagnification.Visible = false;
        //    colFocalLength.Visible = false;

        //    //TODO : HuyNT update 13/09/2024
        //    colInputValue.Visible = false;
        //    colOutputValue.Visible = false;
        //    colCurrentIntensityMax.Visible = false;

        //    string capRes = "Resolution";
        //    string capSensorSize = "Sensor Size";
        //    string capSensorSizeMax = "Sensor Size Max";

        //    if (groupId == 74)//AREA SCAN CAMERA
        //    {
        //        colResolution.Visible = true;
        //        colResolution.VisibleIndex = 2;
        //        colResolution.Caption = capRes + " (pixel)";

        //        colMonoColor.Visible = true;
        //        colMonoColor.VisibleIndex = 3;
        //        colSensorSize.Caption = capSensorSize + " (\")";

        //        colSensorSize.Visible = true;
        //        colSensorSize.VisibleIndex = 4;

        //        colDataInterface.Visible = true;
        //        colDataInterface.VisibleIndex = 5;

        //        colLensMount.Visible = true;
        //        colLensMount.VisibleIndex = 6;

        //        colShutterMode.Visible = true;
        //        colShutterMode.VisibleIndex = 7;
        //    }
        //    else if (groupId == 75)//VISION LIGHT
        //    {
        //        colLampType.Visible = !colLampType.Visible;
        //        colLampType.VisibleIndex = 2;

        //        colLampColor.Visible = true;
        //        colLampColor.VisibleIndex = 3;

        //        colLampPower.Visible = false;
        //        colLampPower.VisibleIndex = 4;

        //        colLampWattage.Visible = false;
        //        colLampWattage.VisibleIndex = 5;
        //    }
        //    else if (groupId == 78) //LENS TELECENTRIC
        //    {
        //        colResolution.Visible = true;
        //        colResolution.VisibleIndex = 2;
        //        colResolution.Caption = capRes + " (µm)";

        //        colSensorSizeMax.Visible = true;
        //        colSensorSizeMax.VisibleIndex = 3;
        //        colSensorSizeMax.Caption = capSensorSizeMax + " (\")";

        //        colWD.Visible = true;
        //        colWD.VisibleIndex = 4;

        //        colLensMount.Visible = true;
        //        colLensMount.VisibleIndex = 5;

        //        colFNo.Visible = true;
        //        colFNo.VisibleIndex = 6;

        //        colMagnification.VisibleIndex = 1;
        //    }
        //    else if (groupId == 79) //LINE SCAN CAMERA
        //    {
        //        colResolution.Visible = true;
        //        colResolution.VisibleIndex = 1;

        //        colMonoColor.Visible = true;
        //        colMonoColor.VisibleIndex = 2;

        //        colPixelSize.Visible = true;
        //        colPixelSize.VisibleIndex = 3;

        //        colDataInterface.Visible = true;
        //        colDataInterface.VisibleIndex = 4;

        //        colLensMount.Visible = true;
        //        colLensMount.VisibleIndex = 5;
        //    }
        //    else if (groupId == 81)//LENS FIX FOCAL LENGTH 
        //    {
        //        //colResolution.Visible = true;
        //        colResolution.VisibleIndex = 2;
        //        colResolution.Caption = capRes + " (µm)";

        //        //colSensorSizeMax.Visible = true;
        //        colSensorSizeMax.VisibleIndex = 3;
        //        colSensorSizeMax.Caption = capSensorSizeMax + " (\")";

        //        //colMOD.Visible = true;
        //        colMOD.VisibleIndex = 4;

        //        //colLensMount.Visible = true;
        //        colLensMount.VisibleIndex = 5;

        //        //colFNo.Visible = true;
        //        colFNo.VisibleIndex = 6;

        //        colFocalLength.VisibleIndex = 1;
        //    }
        //    else if (groupId == 139) //LENS SPECIAL
        //    {
        //        //colResolution.Visible = true;
        //        colResolution.VisibleIndex = 2;
        //        //colResolution.Caption = colResolution.Caption + " (µm)";

        //        //colSensorSizeMax.Visible = true;
        //        colSensorSizeMax.VisibleIndex = 3;
        //        //colSensorSizeMax.Caption = colSensorSizeMax.Caption + " (\")";

        //        //colMOD.Visible = true;
        //        colMOD.VisibleIndex = 4;

        //        //colLensMount.Visible = true;
        //        colLensMount.VisibleIndex = 5;

        //        //colFNo.Visible = true;
        //        colFNo.VisibleIndex = 6;

        //        colFocalLength.VisibleIndex = 1;
        //    }
        //    //TODO : HuyNT update 13/09/2024
        //    else if (groupId == 92) //NGUON DC
        //    {
        //        //colInputValue.Visible = true;
        //        colInputValue.VisibleIndex = 2;
        //        //colOutputValue.Visible = true;
        //        colOutputValue.VisibleIndex = 3;
        //        //colCurrentIntensityMax.Visible = true;
        //        colCurrentIntensityMax.VisibleIndex = 4;
        //    }
        //}

        //void loadImage(string imageName)
        //{
        //    try
        //    {
        //        var request = WebRequest.Create("http://113.190.234.64:8083/api/Upload/Images/ProductDemo/" + imageName);
        //        var response = request.GetResponse();
        //        var stream = response.GetResponseStream();

        //        pictureBox1.Image = Image.FromStream(stream);
        //        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        //        pictureBox1.Width = 400;
        //        pictureBox1.Height = 400;

        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //}

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    grvData.OptionsPrint.PrintSelectedRowsOnly = false;
            //    try
            //    {
            //        string filepath = $"{f.SelectedPath}/DanhSachTonKhoDoBaoHo_{DateTime.Now.ToString("ddMMyy")}.xlsx";
            //        grvData.ExportToXls(filepath, optionsEx);

            //        Process.Start(filepath);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //        //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
            //    }
            //    grvData.ClearSelection();
            //}


            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachTonKhoDoBaoHo_{DateTime.Now.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachTonKhoDoBaoHo_{DateTime.Now.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

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

        private void btnAddQR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
            if (id <= 0) return;
            frmAddQRCodeDetail frm = new frmAddQRCodeDetail(warehouseID);
            frm.Edit = true;
            frm.id = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
            grvData.FocusedRowHandle = rowHandle;
        }

        private void btnBorrowNCCReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInventoryBorrowNCCDemo frm = new frmInventoryBorrowNCCDemo();
            frm.warehouseID = 1;
            frm.ShowDialog();
        }


        void UpdateStatus(int status)
        {
            string statusText = status == 0 ? "đưa vào sử dụng" : "đang giặt";
            int productRTCID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));

            if (productRTCID <= 0) return;

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn cập nhật thành {statusText} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    { ProductRTCModel_Enum.Status.ToString(),status},
                };

                SQLHelper<ProductRTCModel>.UpdateFieldsByID(myDict, productRTCID);

                LoadData();
            }


        }

        private void btnWashing_Click(object sender, EventArgs e)
        {

            UpdateStatus(1);

        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "Status"));
            if (status == 1)
            {

                e.Appearance.BackColor = Color.LightYellow;
            }

        }

        private void btnBackToUse_Click(object sender, EventArgs e)
        {
            UpdateStatus(0);
        }
    }
}
