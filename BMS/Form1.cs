using BMS;
using BMS.Model;
using DevExpress.Data;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace BMS
{
    public partial class Form1 : _Forms
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 2)
            //{
            //    //var row = dataGridView1.SelectedRows;
            //    dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = false;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();

            //    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG; //Config xuất ảnh ra excel
            //    gridView1.OptionsPrint.PrintSelectedRowsOnly = false;
            //    try
            //    {
            //        string filepath = $"{f.SelectedPath}/DanhSachCongTac.xls";
            //        gridView1.ExportToXls(filepath, optionsEx);

            //        Process.Start(filepath);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //}

            //string filepath = txtBxSaveTo.Text.ToString();

            //DirectoryInfo info = new DirectoryInfo(@"\\192.168.1.2\ftp\UpdateVersion\RTC");
            //FileInfo[] file = info.GetFiles();
            //string newFile = file.OrderByDescending(x => Convert.ToInt32(Path.GetFileNameWithoutExtension(x.FullName))).FirstOrDefault().Name;
            //WebClient webClient = new WebClient();
            //webClient.DownloadFile("http://192.168.1.2:8083/api/Upload/Images/Test/657.zip", Path.Combine(@"C:\Users\Admin\Desktop", newFile));

            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFile("http://192.168.1.2:8083/api/Upload/Images/Test/657.zip", Path.Combine(f.SelectedPath, "657.zip"));
            //}
            sendEmailOutlook();
        }

        public class IMG
        {
            public string ImagePath { get; set; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BindingDataSpreadsheet();
            checkUpdateVersion();
            //AddUnboundColumn(gridView1);
            //gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

            List<IMG> listIMG = new List<IMG>()
            {
                new IMG(){ImagePath = @"D:\Image\12.jpg"},
                new IMG(){ImagePath = @"D:\Image\asqweqwewewfrgt4hgththghbgfthgthgthg.jpg"},
            };

            //gridControl1.DataSource = listIMG;
            //AssignPictureEdittoImageColumn(colImage);
        }

        void AddUnboundColumn(GridView view)
        {
            // Create an unbound column.
            GridColumn colImage = new GridColumn();
            colImage.FieldName = "Image";
            colImage.Caption = "Image";
            colImage.UnboundType = UnboundColumnType.Object;
            colImage.OptionsColumn.AllowEdit = false;
            colImage.Visible = true;

            // Add the Image column to the grid's Columns collection.
            view.Columns.Add(colImage);

            //AssignPictureEdittoImageColumn(colImage);
        }

        void AssignPictureEdittoImageColumn(GridColumn column)
        {
            // Create and customize the PictureEdit repository item.
            RepositoryItemPictureEdit riPictureEdit = new RepositoryItemPictureEdit();
            riPictureEdit.SizeMode = PictureSizeMode.Zoom;

            // Add the PictureEdit to the grid's RepositoryItems collection.
            //gridControl1.RepositoryItems.Add(riPictureEdit);

            // Assign the PictureEdit to the 'Image' column.
            column.ColumnEdit = riPictureEdit;
        }

        Dictionary<string, Image> imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileName = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "ImagePath") as string ?? string.Empty;
                if (!imageCache.ContainsKey(fileName))
                {
                    Image img = GetImage(fileName);
                    imageCache.Add(fileName, img);
                }
                e.Value = imageCache[fileName];
            }
        }

        Image GetImage(string path)
        {
            // Load an image by its local path, URL, etc.
            // The following code loads the image from te specified file.
            Image img = null;
            if (File.Exists(path))
                img = Image.FromFile(path);
            else
                img = Image.FromFile(@"D:\Image\12.jpg");

            return img;
        }

        void checkUpdateVersion()
        {
            //Get version hiện tại
            string filePathVersion = Path.Combine(Application.StartupPath, "Version.txt");
            var currentVersion = File.ReadAllText(Path.Combine(Application.StartupPath, "Version.txt"));

            //Get version mới nhất trên API
            DirectoryInfo info = new DirectoryInfo(@"\\192.168.1.2\ftp\UpdateVersion\RTC");
            FileInfo[] file = info.GetFiles();
            string newFile = file.OrderByDescending(x => Convert.ToInt32(Path.GetFileNameWithoutExtension(x.FullName))).FirstOrDefault().FullName;

            var newVersion = Path.GetFileNameWithoutExtension(newFile);
            if (currentVersion != newVersion)
            {
                MessageBox.Show($"Có phiên bản mới!,Old:{currentVersion} - New:{newVersion}");
            }
        }


        void BindingDataSpreadsheet()
        {

            //var list = SQLHelper<EmployeeModel>.FindAll();
            //IWorkbook workbook = spreadsheetControl1.Document;
            //Worksheet sheet = workbook.Worksheets[0];
            //sheet.DataBindings.BindToDataSource(list);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string[] customerCodes = File.ReadAllLines(@"C:\Users\Admin\Desktop\dskh.txt");

                progressBar1.Maximum = customerCodes.Length;
                for (int i = 0; i < customerCodes.Length; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    textBox1.Invoke((Action)(() => { textBox1.Text = $"{i + 1}/{customerCodes.Length}"; }));

                    //TextUtils.ExcuteProcedure("spUpdateCustomerDuplicate", new string[] { "@CustomerCode" }, new object[] { customerCodes[i] });
                    SQLHelper<object>.ExcuteProcedure("spUpdateCustomerDuplicate", new string[] { "@CustomerCode" }, new object[] { customerCodes[i] });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1, "Show message", "hiiaiaiaia", ToolTipIcon.Info);
        }



        private void sendEmailOutlook()
        {
            Outlook.MailItem oMsg;
            Outlook.Application oApp;
            oApp = new Outlook.Application();
            try
            {
                //oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                oMsg.Subject = "test";
                oMsg.To = TextUtils.ToString("letheanh040499@gmail.com").Trim();
                string cc = TextUtils.ToString("huynt2410@gmail.com;").Trim();
                List<string> lstCC = new List<string>();
                if (cc.Length > 0)
                {
                    string[] arrCC = cc.Split(';');
                    for (int j = 0; j < arrCC.Length; j++)
                    {
                        string mail = arrCC[j];
                        if (!mail.Contains("@")) continue;
                        lstCC.Add(arrCC[j]);
                    }
                }
                oMsg.CC = string.Join(";", lstCC);
                //oMsg.Display();
                oMsg.HTMLBody = "<HTML><H2>NT.Huy Test</H2><BODY>huydz123</BODY></HTML>";
                oMsg.Send();
                //oMsg.Close(Outlook.OlInspectorClose.olSave);
                //
                //ExcuteSQL($"UPDATE EmployeeSendEmail SET StatusSend = 2, DateSend = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID = {dt.Rows[i]["ID"]}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //string message = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}:\nMessage: {ex.Message}\n{ex.ToString()}\n-------------------------------\n";
                //File.AppendAllText($"logException-{DateTime.Now.ToString("yyyy-MM-dd")}.txt", message);

                //if (ex.Message.Contains("The RPC server is unavailable"))
                //{
                //    IsRun = false;
                //    break;
                //}
            }
            finally
            {
                oMsg = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000, "A", "adadasdasd", ToolTipIcon.Info);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Login();

            //// Đường dẫn tới file HTML hoặc URL có form
            string htmlPath = @"C:/Users/Admin/Desktop/indexlogin.html";

            // Đảm bảo WebBrowser không có lỗi JavaScript
            webBrowser1.ScriptErrorsSuppressed = true;

            // Tải form HTML vào WebBrowser
            //webBrowser1.Url = new Uri(htmlPath);

            //string html = "<form action=\"http://localhost:26179/Home/LoginToCourse\" method=\"post\" id=\"frmSubmitLink\">" +
            //                    "<input type=\"text\" name=\"userName\" value=\"ltanh\"/>" +
            //                    "<input type=\"text\" name=\"passwordHash\" value=\"MQA=\" />" +
            //                    "<input type=\"number\" name=\"registerIdeaTypeID\" value=\"1\"/>" +
            //                    "<button type=\"submit\">Login</button>" +
            //                "</form>";

            string userName = "ltanh";
            string passwordHash = "MQA=";
            int registerIdeaTypeID = 10;
            string htmlContent = $@"<!DOCTYPE html>
                                    <html lang=""en"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                        <title>Form Example</title>
                                    </head>
                                    <body style=""height: 100vh; width: 100%; display: flex; justify-content:  center; align-items: center; margin: 0;padding: 0;"">

                                        <img src=""./loading.gif"" alt="""">
                                       <form action=""http://localhost:26179/Home/LoginToCourse"" method=""post"" id=""frmSubmitLink"" style=""display: none;"">
                                            <input type=""text"" name=""userName"" value=""{userName}""/>
                                            <input type=""text"" name=""passwordHash"" value=""{passwordHash}"" />
                                            <input type=""number"" name=""registerIdeaTypeID"" value=""{registerIdeaTypeID}""/>
                                            <button type=""submit"">Login</button>
                                        </form>
                                        <script>
                                           document.getElementById(""frmSubmitLink"").submit();
                                        </script>
                                    </body>
                                    </html>";

            // Define the file path where you want to save the HTML file
            //string filePath = "C:\\path\\to\\your\\file.html";

            // Write the HTML content to the file
            File.WriteAllText(htmlPath, htmlContent);

            Process.Start(htmlPath);

            //webBrowser1.DocumentText = html;

            //webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            //webBrowser1.Navigating += WebBrowser1_Navigating;

            // Khi tài liệu được tải xong
            //webBrowser1.DocumentCompleted += (s, ev) =>
            //{
            //    var document = webBrowser1.Document;

            //    var form = document.GetElementById("frmSubmitLink");
            //    document.GetElementById("username").SetAttribute("value", "NV0079");
            //    document.GetElementById("password").SetAttribute("value", "MQA=");


            //    //Tự động gửi form khi tài liệu đã được tải xong
            //    if (form != null)
            //    {
            //        form.InvokeMember("submit");
            //    }

            //    //var element = webBrowser1.Document.All["hrefId"];
            //    //if (element != null)
            //    //{
            //    //    webBrowser1.Select();
            //    //    element.Focus();
            //    //    SendKeys.Send("{ENTER}");
            //    //}
            //};

            //Bắt sự kiện khi có điều hướng(Navigating)
            //webBrowser1.Navigating += (s, en) =>
            //{
            //    string targetUrl = $@"http://localhost:26179/Home/Index?departmentID={2}&courseCatalogID={10}&catalogType={2}";

            //    // Mở URL trong trình duyệt ngoài
            //    Process.Start(targetUrl);

            //    // Ngừng điều hướng trong WebBrowser
            //    en.Cancel = true;

            //};


            //string text = File.ReadAllText(htmlPath);
            //text = text.Replace("@UserName", "NV0079");
            //text = text.Replace("@Password", "MQA=");
            //text = text.Replace("@RegisterIdeaTypeID", "1");
            //File.WriteAllText(htmlPath, text);

            //Process.Start(text);

        }

        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string targetUrl = $@"http://localhost:26179/Home/Index?departmentID={2}&courseCatalogID={10}&catalogType={2}";
            //string targetUrl = e.Url;
            Process.Start(targetUrl);

            e.Cancel = true;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var form = webBrowser1.Document.GetElementById("frmSubmitLink");
            if (form != null)
            {
                form.InvokeMember("submit");
            }
        }

        async Task Login()
        {
            try
            {
                var url = "http://localhost:26179/Home/LoginToCourse";  // The URL where the form data is being submitted

                // Tạo CookieContainer để lưu trữ cookies
                var cookieContainer = new CookieContainer();

                // Tạo HttpClientHandler và thiết lập CookieContainer
                var handler = new HttpClientHandler()
                {
                    CookieContainer = cookieContainer
                };

                using (var client = new HttpClient())
                {
                    // Gửi yêu cầu GET (hoặc trang đăng nhập, nếu cần thiết)
                    var response = await client.GetAsync("http://localhost:26179/Home/Index");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Failed to retrieve session page.");
                        return;
                    }

                    // Define form parameters
                    var formData = new Dictionary<string, string>
                    {
                        { "username", "ltanh" },
                        { "passwordHash", "MQA=" },
                        { "registerIdeaTypeID", "1" },
                    };

                    // Create the content for the POST request
                    var content = new FormUrlEncodedContent(formData);

                    // Send the POST request asynchronously
                    var postResponse = await client.PostAsync(url, content);

                    // Check if the request was successful
                    if (postResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Form submitted successfully");
                        string urlto = "http://localhost:26179/Home/Index?departmentID=2&courseCatalogID=9&catalogType=2";
                        Process.Start(urlto);
                    }
                    else
                    {
                        MessageBox.Show($"Form submission failed with status code: {postResponse.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
