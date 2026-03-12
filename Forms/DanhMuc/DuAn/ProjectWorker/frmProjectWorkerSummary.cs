using BMS.Model;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectWorkerSummary : _Forms
    {
        public DataTable dt = new DataTable();
        public ProjectModel project = new ProjectModel();
        public int workerVersionID = 0;
        public string projectTypeName = "";
        public frmProjectWorkerSummary()
        {
            InitializeComponent();
        }

        private void frmProjectWorkerSummary_Load(object sender, EventArgs e)
        {
            this.Text += $" - {project.ProjectCode} - {projectTypeName}";
            LoadData();
        }

        public void LoadData()
        {

            //dt = TextUtils.LoadDataFromSP("spGetProjectPartList", "A",
            //                        new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
            //                        new object[] { 0, 0, -1, "", -1, -1, partlistVersionID });



            dt = TextUtils.LoadDataFromSP("spGetProjectWorker", "A",
                                    new string[] { "@ProjectID", "@ProjectWorkerTypeID", "@IsApprovedTBP", "@IsDeleted", "@KeyWord", "@ProjectWorkerVersion" },
                                    new object[] { project.ID, 0, -1, 0, "", workerVersionID });
            if (dt.Rows.Count > 0)
            {
                CalculateWork(dt);
                GetParent(dt);
            }

            grdData.DataSource = dt;

            //bandProjectCode.Caption = project.ProjectCode;
            //bandProjectName.Caption = project.ProjectName;
            //bandNumberCode.Caption = "BM03-RTC.TE-QT01\nBan hành lần: 02\nNgày: ";

            //var summarys = grvData.Columns["TotalPrice"].Summary;
            //if (summarys.Count > 0)
            //{
            //    grvData.Columns["TotalPrice"].Summary.Clear();
            //}

            //var dataRowChild = dt.Select("CountChild <= 0");
            //decimal totalAmout = dataRowChild.Sum(x => TextUtils.ToDecimal(x.Field<decimal>("TotalPrice")));
            //grvData.Columns["TotalPrice"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TotalPrice", $"{totalAmout.ToString("n2")}"));
            
        }

        void CalculateWork(DataTable dataTable, int rowIndex = 0)
        {
            // Lấy thông tin về công việc hiện tại
            int numberOfPeople = TextUtils.ToInt(dataTable.Rows[rowIndex]["AmountPeople"]);
            double numberOfDays = TextUtils.ToDouble(dataTable.Rows[rowIndex]["NumberOfDay"]);
            double laborCostPerDay = TextUtils.ToDouble(dataTable.Rows[rowIndex]["Price"]);

            // Xử lý các công việc con
            double totalamountPeopleFromChildren = 0;
            double totalnumberOfDaysFromChildren = 0;
            double totallaborCostPerDayFromChildren = 0;
            double totalLaborFromChildren = 0;
            double totalCostFromChildren = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int childRowIndex = dataTable.Rows.IndexOf(row);

                if (row["ParentID"] != DBNull.Value &&
                    TextUtils.ToInt(row["ParentID"]) == TextUtils.ToInt(dataTable.Rows[rowIndex]["ID"]))
                {
                    CalculateWork(dataTable, childRowIndex);

                    // Lấy giá trị từ công việc con
                    double amountPeopleFromChild = TextUtils.ToInt(row["AmountPeople"]);
                    double dayFromChild = TextUtils.ToDouble(row["NumberOfDay"]);
                    double priceFromChild = TextUtils.ToDouble(row["Price"]);
                    double laborFromChild = TextUtils.ToDouble(row["TotalWorkforce"]);
                    double costFromChild = TextUtils.ToDouble(row["TotalPrice"]);

                    // Tính tổng từ công việc con
                    totalamountPeopleFromChildren += amountPeopleFromChild;
                    totalnumberOfDaysFromChildren += dayFromChild;
                    totallaborCostPerDayFromChildren += priceFromChild;
                    totalLaborFromChildren += laborFromChild;
                    totalCostFromChildren += costFromChild;
                }
            }


            // Tính tổng từ công việc hiện tại và các công việc con
            double totalLabor = numberOfPeople * numberOfDays + totalLaborFromChildren;
            double totalCost = totalLabor * laborCostPerDay + totalCostFromChildren;

            // Cập nhật dữ liệu cho công việc hiện tại
            //dataTable.Rows[rowIndex]["AmountPeople"] = numberOfPeople + totalamountPeopleFromChildren;
            //dataTable.Rows[rowIndex]["NumberOfDay"] = numberOfDays + totalnumberOfDaysFromChildren;
            //dataTable.Rows[rowIndex]["Price"] = laborCostPerDay + totallaborCostPerDayFromChildren;
            //dataTable.Rows[rowIndex]["TotalWorkforce"] = totalLabor;
            dataTable.Rows[rowIndex]["TotalPrice"] = totalCost;
        }

        void GetParent(DataTable dataTable)
        {
            //int countChild = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                if (id <= 0) continue;
                int parentID = TextUtils.ToInt(row["ParentID"]);
                var listChild = SQLHelper<ProjectWorkerModel>.FindByAttribute("ParentID", id);

                if (parentID <= 0 || listChild.Count > 0)
                {
                    //row["AmountPeople"] = 0;
                    //row["NumberOfDay"] = 0;
                    //row["Price"] = 0;
                    //row["TotalWorkforce"] = 0;
                }

                if (listChild.Count <= 0) continue;
                row["CountChild"] = listChild.Count;
                if (listChild.Count > 0)
                {
                    row["TotalPriceChild"] = 0;
                }
                
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                string filepath = Path.Combine(f.SelectedPath, $"NhanCongDuAn_{project.ProjectCode}_{projectTypeName}.xlsx");

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
            //else
            if (countChild > 0)
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, System.Drawing.FontStyle.Bold);
            }
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AmountPeople"
                || e.Column.FieldName == "NumberOfDay"
                || e.Column.FieldName == "TotalWorkforce"
                || e.Column.FieldName == "Price" )
            {
                int parentID = TextUtils.ToInt(grvData.GetRowCellValue(e.ListSourceRowIndex, "ParentID"));
                int countChild = TextUtils.ToInt(grvData.GetRowCellValue(e.ListSourceRowIndex, "CountChild"));
                if (countChild > 0)
                {
                    e.DisplayText = "";
                }
            }
        }
    }
}
