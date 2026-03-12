using BMS.Business;
using DevExpress.Utils;
using DevExpress.XtraTab;
using Forms.TB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmHistoryProductRTCProtectiveGearNew : _Forms
    {
        public int warehouseID = 5;
        bool isDefault = false;

        string pathPattern = $@"DoPhongSach\Anh";
        string urlAPI = $@"http://192.168.1.2:8083/api/hcns";

        bool isMove = false;

        public frmHistoryProductRTCProtectiveGearNew()
        {
            InitializeComponent();
        }

        private void frmHistoryProductRTCProtectiveGearNew_Load(object sender, EventArgs e)
        {
            //panel1.AllowDrop = 
            LoadData();


            //btnFind_Click(null, null);
        }


        void LoadData()
        {
            DateTime date = new DateTime(2024, 09, 01);
            DateTime dateStart = date;
            DateTime dateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            string keyword = txtKeyword.Text.Trim();
            string status = "1,2,3,4,5,6,7,8";
            int employeeID = 0;
            int isDeleted = 0;
            int productGroupRTCID = 140;
            //int locationType = xtraTabControl1.SelectedTabPageIndex + 1;

            //var controls = xtraTabControl1.SelectedTabPage.Controls;
            //if (controls.Count <= 0) return;

            //Panel panel = (Panel)controls[0];
            //if (panel == null) return;

            //DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryProductRTCProtectiveGear", "A",
            //        new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@IsDeleted", "@WarehouseID", "@ProductGroupRTCID", "@Keyword", "@LocationType" },
            //        new object[] { dateStart, dateEnd, employeeID, status, isDeleted, warehouseID, productGroupRTCID, keyword, locationType });
            //grdData.DataSource = dtHistoryProduct;

            //panel.Controls.Clear();
            //flowLayoutPanel1.Controls.Clear();

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            {
                List<ProductRTCDTO> listData = SQLHelper<ProductRTCDTO>.ProcedureToList("spGetHistoryProductRTCProtectiveGear",
                    new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@IsDeleted", "@WarehouseID", "@ProductGroupRTCID", "@Keyword" },
                    new object[] { dateStart, dateEnd, employeeID, status, isDeleted, warehouseID, productGroupRTCID, keyword });

                listData = listData.Where(x=>x.ProductGroupRTCID == 140).ToList();

                var listType1 = listData.Where(x => x.LocationType == 1).ToList();
                var listType2 = listData.Where(x => x.LocationType == 2).ToList();
                var listType3 = listData.Where(x => x.LocationType == 3).ToList();

                LoadLayout(panel1, listType1);
                LoadLayout(panel2, listType2);
                LoadLayout(panel3, listType3);
            }



            //using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            //{
            //    int index = 0;
            //    int rowCount = dt.Rows.Count / 5;

            //    int widthOld = 260;
            //    int heightOld = 210;

            //    for (int i = 0; i < rowCount + 1; i++)
            //    {
            //        for (int j = 0; j < 5; j++)
            //        {
            //            if (dt.Rows.Count <= index) break;
            //            //ucProductRTC uc = new ucProductRTC();
            //            ////uc.Width = (this.Width / 5) - 10;
            //            //uc.Location = new Point(uc.Width * j + 5, uc.Height * i + 5);
            //            //uc.BackColor = Color.Pink;

            //            DataRow dataRow = dt.Rows[index];
            //            int productRTCID = TextUtils.ToInt(dataRow["ID"]);
            //            int histortyID = TextUtils.ToInt(dataRow["HistortyID"]);
            //            int productLocationID = TextUtils.ToInt(dataRow["ProductLocationID"]);

            //            string productCode = TextUtils.ToString(dataRow["ProductCode"]);
            //            string productName = TextUtils.ToString(dataRow["ProductName"]);
            //            string size = TextUtils.ToString(dataRow["Size"]);

            //            string code = TextUtils.ToString(dataRow["Code"]);
            //            string fullName = TextUtils.ToString(dataRow["FullName"]);

            //            DateTime? dateBorrow = TextUtils.ToDate4(dataRow["DateBorrow"]);
            //            DateTime? dateReturnExpected = TextUtils.ToDate4(dataRow["DateReturnExpected"]);

            //            string dateBorrowText = dateBorrow.HasValue ? dateBorrow.Value.ToString("dd/MM/yyyy") : ""; ;
            //            string dateReturnExpectedText = dateReturnExpected.HasValue ? dateReturnExpected.Value.ToString("dd/MM/yyyy") : "";

            //            int statusHistory = TextUtils.ToInt(dataRow["StatusNew"]);
            //            int statusProduct = TextUtils.ToInt(dataRow["Status"]);
            //            //string employeeBorrow = "";

            //            //if (statusHistory != 0)
            //            //{
            //            //    employeeBorrow = $"Người mượn: {code} - {fullName}";
            //            //    dateBorrowText = dateBorrow.HasValue ? $"Ngày mượn: {dateBorrow.Value.ToString("dd/MM/yyyy")}" : "";
            //            //    dateReturnExpectedText = dateReturnExpected.HasValue ? $"Ngày dự kiến trả: {dateReturnExpected.Value.ToString("dd/MM/yyyy")}" : "";
            //            //}

            //            int x = TextUtils.ToInt(dataRow["CoordinatesX"]);
            //            int y = TextUtils.ToInt(dataRow["CoordinatesY"]);
            //            //x = 0; y = 0;
            //            if (isDefault)
            //            {
            //                x = 0; y = 0;
            //            }

            //            //uc.Location = new Point(, );

            //            ucProductRTC uc = new ucProductRTC();
            //            string sizeText = $"\nSize: {size}";
            //            if (string.IsNullOrWhiteSpace(size)) sizeText = "";
            //            uc.lblProductName.Text = $"{productCode} - {productName} {sizeText}";
            //            uc.lblLocation.Text = $"Vị trí: {TextUtils.ToString(dataRow["LocationName"])}";
            //            uc.lblEmployee.Text = $"Người mượn: {code} - {fullName}";
            //            uc.lblDateBorrow.Text = $"Ngày mượn: {dateBorrowText}";
            //            uc.lblDateExpect.Text = $"Ngày trả: {dateReturnExpectedText}";

            //            string statusText = TextUtils.ToString(dataRow["StatusText"]);
            //            uc.lblStatusText.Text = $"{statusText}";
            //            //uc.lblStatusText.Text = $"Đăng ký mượn";
            //            uc.lblStatusText.Visible = !string.IsNullOrWhiteSpace(statusText);
            //            //uc.BackColor = Color.LimeGreen;
            //            uc.Tag = productLocationID;
            //            //uc.Width = (this.Width / 5) - 10;

            //            uc.productRTCID = productRTCID;
            //            uc.histortyID = histortyID;
            //            uc.statusProduct = statusProduct;

            //            x = x < 0 ? 0 : x;
            //            y = y < 0 ? 0 : y;
            //            if (x == 0 && y == 0)
            //            {
            //                x = (uc.Width * j + (j * 10)) + 10;
            //                y = (uc.Height * i + (i * 10)) + 10;
            //            }

            //            //int ratioTop = (uc.Height / heightOld);
            //            //int ratioLeft = (uc.Width / widthOld);

            //            uc.Location = new Point(x, y);

            //            uc.pnHeader.BackColor = Color.FromArgb(61, 134, 66);//Xanh
            //            uc.lblProductName.ForeColor = Color.White;
            //            if (statusHistory != 0)
            //            {
            //                uc.pnHeader.BackColor = Color.FromArgb(199, 65, 21);//Red

            //                //uc.lblEmployee.ForeColor = Color.White;
            //                //uc.lblDateBorrow.ForeColor = Color.White;
            //                //uc.lblDateExpect.ForeColor = Color.White;
            //                //uc.lblLocation.ForeColor = Color.White;
            //            }
            //            else if (statusProduct == 1)
            //            {
            //                //uc.BackColor = Color.LightYellow;
            //                uc.pnHeader.BackColor = Color.FromArgb(255, 202, 44);//Vàng
            //                uc.lblProductName.ForeColor = Color.Black;
            //            }

            //            string locationImg = TextUtils.ToString(dataRow["LocationImg"]);
            //            //locationImg = "cat.gif";
            //            //uc.panel2.Visible = !string.IsNullOrEmpty(locationImg);
            //            LoadImage(locationImg, productCode, uc.pbImage);

            //            panel.Controls.Add(uc);
            //            //flowLayoutPanel1.Controls.Add(uc);


            //            index++;
            //        }
            //    }
            //}
        }


        void LoadImage(string locationImg, string productCode, PictureBox pictureBox)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(locationImg))
                {
                    pictureBox.Visible = false;
                    return;
                };

                FileInfo file = new FileInfo(locationImg);
                string url = $"{urlAPI}/{pathPattern}/{productCode}_{file.Name}";
                //string url = $"{urlAPI}/{pathPattern}/MBH01_16-Thuy-Duong-N30-1-450x450.jpg";
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                pictureBox.Image = Image.FromStream(stream);
                pictureBox.ImageLocation = url;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Thông báo");
                return;
            }
        }
        void SaveLayout(int productLocationID, Control control)
        {
            //int productLocationID = TextUtils.ToInt(this.Tag);

            var myDict = new Dictionary<string, object>()
            {
                {ProductLocationModel_Enum.CoordinatesX.ToString(),control.Location.X },
                {ProductLocationModel_Enum.CoordinatesY.ToString(),control.Location.Y },
            };

            SQLHelper<ProductLocationModel>.UpdateFieldsByID(myDict, productLocationID);
        }

        void LoadLayout(Control control, List<ProductRTCDTO> list)
        {
            int index = 0;
            int rowCount = list.Count / 5;

            //int widthOld = 260;
            //int heightOld = 210;
            control.Controls.Clear();
            for (int i = 0; i < rowCount + 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (list.Count <= index) break;
                    ProductRTCDTO product = list[index];

                    int productRTCID = product.ID;
                    int histortyID = product.HistortyID;
                    int productLocationID = TextUtils.ToInt(product.ProductLocationID);

                    string productCode = product.ProductCode;
                    string productName = product.ProductName;
                    string size = product.Size;

                    string code = product.Code;
                    string fullName = product.FullName;

                    DateTime? dateBorrow = product.DateBorrow;
                    DateTime? dateReturnExpected = product.DateReturnExpected;

                    string dateBorrowText = dateBorrow.HasValue ? dateBorrow.Value.ToString("dd/MM/yyyy") : ""; ;
                    string dateReturnExpectedText = dateReturnExpected.HasValue ? dateReturnExpected.Value.ToString("dd/MM/yyyy") : "";

                    int statusHistory = product.StatusNew;
                    int statusProduct = TextUtils.ToInt(product.Status);

                    int x = TextUtils.ToInt(product.CoordinatesX);
                    int y = TextUtils.ToInt(product.CoordinatesY);
                    if (isDefault)
                    {
                        x = 0; y = 0;
                    }

                    ucProductRTC uc = new ucProductRTC();
                    string sizeText = $"\nSize: {size}";
                    if (string.IsNullOrWhiteSpace(size)) sizeText = "";
                    uc.lblProductName.Text = $"{productCode} - {productName} {sizeText}";
                    uc.lblLocation.Text = $"Vị trí: {product.LocationName}";
                    uc.lblEmployee.Text = $"Người mượn: {code} - {fullName}";
                    uc.lblDateBorrow.Text = $"Ngày mượn: {dateBorrowText}";
                    uc.lblDateExpect.Text = $"Ngày trả: {dateReturnExpectedText}";

                    string statusText = product.StatusText;
                    uc.lblStatusText.Text = $"{statusText}";
                    uc.lblStatusText.Visible = !string.IsNullOrWhiteSpace(statusText);
                    uc.Tag = productLocationID;

                    uc.productRTCID = productRTCID;
                    uc.histortyID = histortyID;
                    uc.statusProduct = statusProduct;

                    x = x < 0 ? 0 : x;
                    y = y < 0 ? 0 : y;
                    if (x == 0 && y == 0)
                    {
                        x = (uc.Width * j + (j * 10)) + 10;
                        y = (uc.Height * i + (i * 10)) + 10;
                    }

                    uc.Location = new Point(x, y);

                    uc.pnHeader.BackColor = Color.FromArgb(61, 134, 66);//Xanh
                    uc.lblProductName.ForeColor = Color.White;
                    if (statusHistory != 0)
                    {
                        uc.pnHeader.BackColor = Color.FromArgb(199, 65, 21);//Red
                    }
                    else if (statusProduct == 1)
                    {
                        //uc.BackColor = Color.LightYellow;
                        uc.pnHeader.BackColor = Color.FromArgb(255, 202, 44);//Vàng
                        uc.lblProductName.ForeColor = Color.Black;
                    }

                    string locationImg = product.LocationImg;
                    LoadImage(locationImg, productCode, uc.pbImage);

                    control.Controls.Add(uc);
                    index++;
                }
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            if (c != null)
            {
                c.Location = panel1.PointToClient(new Point(e.X, e.Y));
                panel1.Controls.Add(c);

                isMove = true;
            }
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            if (isMove)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn vừa thay đổi vị trí.\nBạn có muốn lưu lại vị trí trước không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    btnSaveLayout_Click(null, null);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

                isMove = false;
            }

            isDefault = false;
            LoadData();
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {

        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            bool isConfirm = false;
            if (sender != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn lưu lại layout không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                isConfirm = dialogResult == DialogResult.Yes;
            }

            if (isConfirm || sender == null)
            {
                foreach (XtraTabPage item in xtraTabControl1.TabPages)
                {
                    var controls = item.Controls;
                    if (controls.Count <= 0) continue;

                    Panel panel = (Panel)controls[0];
                    if (panel == null) continue;

                    //panel1.AutoScrollPosition = new Point(0, 0);
                    panel.AutoScrollPosition = new Point(0, 0);
                    foreach (Control control in panel.Controls)
                    {
                        //ucProductRTC uc = (ucProductRTC)item;
                        int productLocationID = TextUtils.ToInt(control.Tag);
                        SaveLayout(productLocationID, control);
                    }
                }

                isMove = false;
            }

            //frmProductLocationTech frm = new frmProductLocationTech(warehouseID);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}
        }

        private void btnBorrow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHistoryProductRTCDetailProtectiveGear frm = new frmHistoryProductRTCDetailProtectiveGear();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //LoadData();
            }
        }

        private void btnDuyenMuon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Int32[] selectedRowHandles = grvData.GetSelectedRows();
            //if (selectedRowHandles.Length <= 0) return;
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{
            //    int selectedRowHandle = selectedRowHandles[i];
            //    int productHistoryID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
            //    int Status = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colStatusNew));

            //    HistoryProductRTCModel historyProductRTCModel = SQLHelper<HistoryProductRTCModel>.FindByID(productHistoryID);
            //    if (historyProductRTCModel.ID <= 0) return;
            //    if (Status == 7)
            //    {
            //        historyProductRTCModel.Status = 1;
            //    }
            //    historyProductRTCModel.IsDelete = false;
            //    SQLHelper<HistoryProductRTCModel>.Update(historyProductRTCModel);
            //}
            //LoadData();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            //panel1.Scale(new SizeF(1.1f, 1.1f));

            //panel1.AutoScrollPosition = new Point(100,100);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            //panel1.Scale(new SizeF(0.9f, 0.9f));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn cài đặt lại vị trí mặc định không?\nLayout trước đó sẽ bị mất và không thể lấy lại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                isDefault = true;
                LoadData();

                isMove = true;
            }
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            var panel = (Panel)sender;
            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            if (c != null)
            {
                c.Location = panel.PointToClient(new Point(e.X, e.Y));
                panel.Controls.Add(c);
                isMove = true;
            }
        }

        private void panel2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //LoadData();
        }

        private void frmHistoryProductRTCProtectiveGearNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isMove)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn vừa thay đổi vị trí.\nBạn có muốn lưu lại vị trí trước không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    btnSaveLayout_Click(null, null);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                //isMove = false;
            }
        }
    }
}
