using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPartList_New_Detail : _Forms
    {
        //  public int projectID;
        //  public int versiongrvData;
        //  public int versiongrvDataPO;


        //public  bool isFocusVersionGP = false;
        //  public bool isFocusVersionPO = false;


        public DataTable dt = new DataTable();
        public ProjectModel project = new ProjectModel();
        public int partlistVersionID = 0;
        public int partListTypeID = 0;
        public string projectTypeName = "";
        public frmProjectPartList_New_Detail()
        {
            InitializeComponent();
        }

        private void stackPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmProjectPartList_New_Detail_Load(object sender, EventArgs e)
        {
            this.Text += $" - {project.ProjectCode} - {projectTypeName}";
            //cboIsDeleted.SelectedIndex = 1;
            //cboStatusTBP.SelectedIndex = 0;
            //cboStatusPur.SelectedIndex = 0;
            loadData();
        }



        public void loadData()
        {
            //int version = 0;

            //if (isFocusVersionGP) version = versiongrvData;
            //else if (isFocusVersionPO) version = versiongrvDataPO;

            ////version = 19;


            //int isDelete = cboIsDeleted.SelectedIndex - 1;
            //int isApprovedTBP = cboStatusTBP.SelectedIndex - 1;
            //int isApprovedPur = cboStatusPur.SelectedIndex - 1;
            //int type = TextUtils.ToInt(cboPartListType.EditValue);

            dt = TextUtils.LoadDataFromSP("spGetProjectPartList_Khanh", "A",
                                    new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                    new object[] { project.ID, partListTypeID, 0, "", -1, -1, partlistVersionID });
            if (dt.Rows.Count > 0)
            {
                CalculateAllWork(dt);
                GetParent(dt);
            }

            grdData.DataSource = dt;

            bandProjectCode.Caption = project.ProjectCode;
            bandProjectName.Caption = project.ProjectName;
            bandNumberCode.Caption = "BM03-RTC.TE-QT01\nBan hành lần: 02\nNgày: ";

            //var summarys = grvData.Columns["AmountExport"].Summary;
            //if (summarys.Count > 0)
            //{
            //    grvData.Columns["AmountExport"].Summary.Clear();
            //}


            //var dataRowChild = dt.Select("CountChild <= 0 AND ParentID <> 0");
            //decimal totalAmout = dataRowChild.Sum(x => x.Field<decimal>("AmountExport"));
            //grvData.Columns["AmountExport"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "AmountExport", $"{totalAmout.ToString("n2")}"));

        }
        void CalculateWork(DataTable dataTable, int rowIndex = 0)
        {
            int qtyFull = TextUtils.ToInt(dataTable.Rows[rowIndex]["QtyFull"]);
            double price = TextUtils.ToDouble(dataTable.Rows[rowIndex]["Price"]);

            double totalPriceFromChildren = 0;
            double totalAmountFromChildren = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int childRowIndex = dataTable.Rows.IndexOf(row);

                int parentID = TextUtils.ToInt(row["ParentID"]);
                int id = TextUtils.ToInt(dataTable.Rows[rowIndex]["ID"]);
                if (id <= 0) continue;
                string tt = TextUtils.ToString(dataTable.Rows[rowIndex]["TT"]);

                if (row["ParentID"] != DBNull.Value && parentID == id)
                {
                    //rowIndex = childRowIndex;
                    CalculateWork(dataTable, childRowIndex);

                    double laborFromChild = TextUtils.ToDouble(row["Price"]);
                    double costFromChild = TextUtils.ToDouble(row["Amount"]);

                    totalPriceFromChildren += laborFromChild;
                    totalAmountFromChildren += costFromChild;
                }
            }


            double totalLabor = price + totalPriceFromChildren;
            double totalCost = qtyFull * price + totalAmountFromChildren;

            dataTable.Rows[rowIndex]["Price"] = totalLabor;
            dataTable.Rows[rowIndex]["Amount"] = totalCost;
        }

        void CalculateAllWork(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (TextUtils.ToInt(row["ParentID"]) == 0)
                {
                    int index = dataTable.Rows.IndexOf(row);

                    CalculateWork(dataTable, index);
                }
            }
        }

        void GetParent(DataTable dataTable)
        {
            //int countChild = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                if (id <= 0) continue;

                int parentID = TextUtils.ToInt(row["ParentID"]);
                var listChild = SQLHelper<ProjectPartListModel>.FindByAttribute("ParentID", id);

                if (/*parentID <= 0 || */listChild.Count > 0)
                {
                    row["Price"] = 0;
                    row["Amount"] = 0;
                }
                if (listChild.Count <= 0) continue;
                row["CountChild"] = listChild.Count;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                string filepath = Path.Combine(f.SelectedPath, $"DanhMucVatTuDuAn_{project.ProjectCode}_{projectTypeName}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    //compositeLink.Links.Add(printableComponentLink2);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            bool isDeleted = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, "IsDeleted"));
            bool isProblem = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, "IsProblem"));

            int countChild = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "CountChild"));
            int parentId = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "ParentID"));

            int id = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "ID"));
            if (id <= 0) return;

            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            else if (isProblem)
            {
                e.Appearance.BackColor = Color.Orange;
            }


            //if (parentId == 0)
            //{
            //    e.Appearance.BackColor = Color.Gold;
            //    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, System.Drawing.FontStyle.Bold);
            //}
            else if (countChild > 0)
            {
                if (parentId == 0)
                {
                    e.Appearance.BackColor = Color.Gold;
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, System.Drawing.FontStyle.Bold);
                }
            }

            decimal quantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, "QuantityReturn"));
            if (quantityReturn > 0)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "QtyMin"
                || e.Column.FieldName == "QtyFull"
                || e.Column.FieldName == "Unit"
                || e.Column.FieldName == "Price"
                || e.Column.FieldName == "UnitPriceQuote"
                || e.Column.FieldName == "UnitPricePurchase")
            {
                int parentID = TextUtils.ToInt(grvData.GetRowCellValue(e.ListSourceRowIndex, "ParentID"));
                int countChild = TextUtils.ToInt(grvData.GetRowCellValue(e.ListSourceRowIndex, "CountChild"));
                if (/*parentID == 0 || */countChild > 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        public int Compare(object x, object y)
        {
            // Kiểm tra nếu là giá trị null
            if (x == null || y == null)
                return 0;

            //string strX = x.ToString();
            //string strY = y.ToString();

            string strX = TextUtils.ToString(x);// x.ToString();
            string strY = TextUtils.ToString(y); // y.ToString();

            string pattern = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            Regex regex = new Regex(pattern);
            Regex regexStt = new Regex(@"^-?[\d\.]+$");

            if (string.IsNullOrWhiteSpace(strX) || string.IsNullOrWhiteSpace(strY))
            {
                return 0;
            }
            else
            {
                if (!regexStt.IsMatch(strX) || !regexStt.IsMatch(strY)) return 0;
            }

            strX = strX.Replace(",", ".");
            strY = strY.Replace(",", ".");

            // Tách các phần số sau dấu "."
            var xParts = strX.Split('.').Select(int.Parse).ToArray();
            var yParts = strY.Split('.').Select(int.Parse).ToArray();

            // So sánh từng phần của phiên bản
            int length = Math.Min(xParts.Length, yParts.Length);
            for (int i = 0; i < length; i++)
            {
                int comparison = xParts[i].CompareTo(yParts[i]);
                if (comparison != 0)
                    return comparison;
            }

            // So sánh theo độ dài của các phần
            return xParts.Length.CompareTo(yParts.Length);
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            try
            {
                // Kiểm tra nếu cột đang được sắp xếp là cột "Version"
                if (e.Column.FieldName == "TT")
                {
                    // Sử dụng VersionComparer để so sánh các giá trị trong cột "Version"
                    //VersionComparer comparer = new VersionComparer();

                    // So sánh hai giá trị x và y của cột
                    //e.Result = comparer.Compare(e.Value1, e.Value2);
                    e.Result = Compare(e.Value1, e.Value2);
                    e.Handled = true; // Đánh dấu rằng bạn đã xử lý sự kiện này
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
    }
}
