using BMS;
using BMS.Model;
using DevExpress.Utils;
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


using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;

namespace BMS
{
    public partial class frmTest : _Forms
    {
        public frmTest()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        public Label myLable;
        private void frmTest_Load(object sender, EventArgs e)
        {
            //loadData();

            //LoadData();
        }
        void loadData()
        {/// spGetRequestBuyRTC

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {

            }
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetRequestBuyRTC", new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@TTDH" }
           , new object[] { 1, 200, "2022 - 09 - 01 00:00:00", "2022-09-30 00:00:00", "", -1 });



            treeList1.DataSource = dts.Tables[0];
            grdMaster.DataSource = dts.Tables[0];

        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //if (treeList1.FocusedNode == null) return;
            //var id = treeList1.GetFocusedRowCellValue(colIDs);
            //TreeListNode listChildNode = treeList1.FindNodeByKeyID(id);
            //TreeListNode parent = treeList1.FocusedNode.ParentNode;
            //bool select = TextUtils.ToBoolean(treeList1.FocusedNode.GetValue(colYeuCaus));
            //treeList1.SetRowCellValue(treeList1.FocusedNode, colYeuCaus, select ? 0 : 1);
            //if (parent == null)
            //{
            //    foreach (TreeListNode node in treeList1.FocusedNode.Nodes)
            //        treeList1.SetRowCellValue(node, colYeuCaus, select ? 0 : 1);
            //}
            //else
            //{
            //    select = !select;
            //    if (select)
            //    {
            //        if (parent != null)
            //            foreach (TreeListNode item in parent.Nodes)
            //            {
            //                select = TextUtils.ToBoolean(item.GetValue(colYeuCaus));
            //                if (!select)
            //                {
            //                    treeList1.SetRowCellValue(parent, colYeuCaus, 0);
            //                    return;
            //                }
            //            }

            //        treeList1.SetRowCellValue(parent, colYeuCaus, 1);
            //    }
            //    //else
            //    //{
            //    //    if (parent != null)
            //    //        treeList1.SetRowCellValue(parent, colYeuCaus, 0);
            //    //}
            //}
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        void LoadData()
        {

            List<EmployeeModel> list = new List<EmployeeModel>()
            {
                new EmployeeModel(){Code = "NV01",FullName = "Nguyễn Minh Khôi", ImagePath = "https://cdn-icons-png.flaticon.com/512/219/219969.png"},
                //new EmployeeModel(){Code = "NV01",FullName = "Nguyễn Minh Khôi", ImagePath = @"D:\LeTheAnh\Image\UploadPNK230526010_070623_101809.jpg"}
            };

            gridControl1.DataSource = list;
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            //Get dường dãn file
            string imagePath = TextUtils.ToString(view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "ImagePath"));
            if (e.Column == colShowImage && e.IsGetData)//Kiểm tra có phải cột để hiển thị ảnh ko và có dữ liệu tại cell đó không.
            {
                if (!string.IsNullOrEmpty(imagePath) && !imagePath.Contains("NoImageUpdate"))
                {
                    try
                    {
                        //nếu imagePath là dường link api
                        var request = WebRequest.Create(imagePath);
                        var response = request.GetResponse();
                        var stream = response.GetResponseStream();
                        e.Value = Image.FromStream(stream);


                        //nếu imagePath là đường dẫn local
                        //e.Value = Image.FromFile(imagePath);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"export_image.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG; //Setting kiểu export
                gridView1.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls";
                    string filepath = f.FileName;
                    gridView1.ExportToXlsx(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Screen[] screenList = Screen.AllScreens;

            foreach (Screen screen in screenList)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(screen.DeviceName, -1, ref dm);

                decimal scalingFactor = Math.Round(Decimal.Divide(dm.dmPelsWidth, screen.Bounds.Width), 2);

                if (scalingFactor > 1)
                {

                    MessageBox.Show((scalingFactor * 100).ToString());
                }
                else
                {
                    MessageBox.Show((scalingFactor).ToString());
                }
            }
        }

        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);
    }
}
