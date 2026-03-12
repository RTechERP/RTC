using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Forms.DanhMuc.DuAn;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectWorker_New2 : _Forms
    {
        //public int ProjectID;
        //public string projectCode;
        //public string projectName;

        DataTable dt = new DataTable();

        bool isFocusVersionGP = false;
        bool isFocusVersionPO = false;

        public ProjectModel project = new ProjectModel();
        public frmProjectWorker_New2()
        {
            InitializeComponent();
        }

        private void frmProjectWorker_New_Load(object sender, EventArgs e)
        {
            this.Text = $"NHÂN CÔNG DỰ ÁN {project.ProjectCode} - {project.ProjectName}";
            cboDeleted.SelectedIndex = 1;
            cboStatusTBP.SelectedIndex = 0;

            LoadProjectSolution();
            LoadVersion();
            //loadData();
            loadDataWorker();

        }

        void LoadProjectSolution()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectSolution", "A", new string[] { "@ProjectID" }, new object[] { project.ID });
            grdDataSolution.DataSource = dt;

            grvDataSolution.FocusedRowHandle = 0;
        }

        void LoadVersion()
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectWorkerVersion_New", "A", new string[] { "@ProjectSolutionID", "@StatusVersion" }, new object[] { projectSolutionId, 1 });
            grdData.DataSource = dt;

            grvData.FocusedRowHandle = 0;
            isFocusVersionGP = true;
            isFocusVersionPO = !isFocusVersionGP;
            loadDataWorker();
        }

        void LoadVersionPO()
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectWorkerVersion_New", "A", new string[] { "@ProjectSolutionID", "@StatusVersion" }, new object[] { projectSolutionId, 2 });
            grdDataPO.DataSource = dt;

            isFocusVersionGP = false;
            isFocusVersionPO = !isFocusVersionGP;
            loadDataWorker();
        }

        void loadData()
        {
            int version = 0;
            //var isFocusGP = grvData.IsFocusedView;
            //var isFocusPO = grvDataPO.IsFocusedView;

            //int versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //int versionId = 0;
            if (isFocusVersionGP) version = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            else if (isFocusVersionPO) version = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));

            //version = 19;

            int isDelete = cboDeleted.SelectedIndex - 1;
            int isApprovedTBP = cboStatusTBP.SelectedIndex - 1;
            //int type = TextUtils.ToInt(cboPartListType.EditValue);

            //DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartList", "A",
            //                        new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
            //                        new object[] { project.ID, type, isDelete, txtKeyword.Text.Trim(), isApprovedTBP, isApprovedPur, version });
            //if (dt.Rows.Count > 0)
            //{
            //    CalculateWork(dt);
            //}

            //treeListData.DataSource = dt;

            //treeListData.ExpandAll();
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

                if (row["ParentID"] != DBNull.Value && TextUtils.ToInt(row["ParentID"]) == TextUtils.ToInt(dataTable.Rows[rowIndex]["ID"]))
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
            //dataTable.Rows[rowIndex][colAmountPeople.FieldName] = totalamountPeopleFromChildren;
            //dataTable.Rows[rowIndex][colNumberOfDay.FieldName] = totalnumberOfDaysFromChildren;
            //dataTable.Rows[rowIndex][colTotalWorkforce.FieldName] = totalLaborFromChildren;
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


        void ApprovedSolution(bool isApproved, int status)
        {
            int rowHandle = grvDataSolution.FocusedRowHandle;

            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            string statusText = status == 1 ? "báo giá" : "PO";

            int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} {statusText} của giải pháp [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.FindByID(id);
                if (solution == null) return;

                if (status == 1)
                {
                    solution.IsApprovedPrice = isApproved;
                    solution.EmployeeApprovedPriceID = Global.EmployeeID;
                }
                else if (status == 2)
                {
                    solution.IsApprovedPO = isApproved;
                    solution.EmployeeApprovedPOID = Global.EmployeeID;
                }

                SQLHelper<ProjectSolutionModel>.Update(solution);
                LoadProjectSolution();
                grvDataSolution.FocusedRowHandle = rowHandle;
            }
        }


        void ApprovedVersion(bool isApproved)
        {
            //int rowHandle = grvData.FocusedRowHandle;
            //string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //if (id <= 0) return;
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} của phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    ProjectWorkerVersionModel version = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
            //    if (version == null) return;
            //    version.IsApproved = isApproved;
            //    version.ApprovedID = Global.EmployeeID;

            //    SQLHelper<ProjectPartListVersionModel>.Update(version);
            //    LoadVersion();
            //    grvData.FocusedRowHandle = rowHandle;
            //}
        }
        void loadDataWorker()
        {
            //loadType();
            int type = 0; //TextUtils.ToInt(cboProjectWorkerType.EditValue);
            int isApprovedTBP = cboStatusTBP.SelectedIndex - 1;
            int isDeleted = cboDeleted.SelectedIndex - 1;
            int version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            int versionBO = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersionPO));
            int versionID = 0;
            if(isFocusVersionGP) versionID  = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            else versionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            dt = TextUtils.LoadDataFromSP("spGetProjectWorker", "A",
               new string[] { "@ProjectID", "@ProjectWorkerTypeID", "@IsApprovedTBP", "@IsDeleted", "@KeyWord", "@ProjectWorkerVersion" },
               new object[] { project.ID, type, isApprovedTBP, isDeleted, txtFind.Text.Trim(), versionID });
            if (dt.Rows.Count > 0)
            {
                CalculateAllWork(dt);
            }

            foreach (var node in treeListData.GetNodeList())
            {
                if (node.HasChildren)
                {
                    node.SetValue(colTotalWorkforce.FieldName, 0);
                }
            }

            treeListData.DataSource = dt;
            treeListData.ParentFieldName = "ParentID";
            treeListData.KeyFieldName = "ID";
            treeListData.ExpandAll();
        }
        private void treeListData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            //bool value = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsDeleted));
            //if (value)
            //{
            //    e.Appearance.BackColor = Color.Red;
            //    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
            //    e.Appearance.ForeColor = Color.White;
            //}
        }

        //private void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog f = new FolderBrowserDialog();
        //    if (f.ShowDialog() == DialogResult.OK)
        //    {
        //        XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
        //        optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
        //        optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.True;
        //        optionsEx.AllowFixedColumns = DevExpress.Utils.DefaultBoolean.True;
        //        optionsEx.SheetName = cboPartListType.Text;

        //        try
        //        {
        //            string filepath = $"{f.SelectedPath}/TienDoVatTuDuAn_{project.ProjectCode}.xls";

        //            // Export the TreeList data including appearance settings to Excel
        //            treeListData.ExportToXls(filepath, optionsEx);

        //            // Open the exported Excel file
        //            Process.Start(filepath);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message.ToString());
        //        }
        //        treeListData.ClearSelection();
        //    }
        //}

        private void cboCategoryPartList_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnHaveNotOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list = treeListData.GetAllCheckedNodes();

            if (list.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set trạng thái này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
                    if (id == 1) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    model.Status = -1;
                    ProjectPartListBO.Instance.Update(model);
                }
                loadData();
                list.Clear();
            }

        }

        private void btnReturned_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list = treeListData.GetAllCheckedNodes();

            if (list.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set trạng thái này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
                    if (id == 1) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    model.Status = 2;
                    ProjectPartListBO.Instance.Update(model);
                }
                loadData();
                list.Clear();
            }

        }

        private void btnHaveOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list = treeListData.GetAllCheckedNodes();

            if (list.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set trạng thái này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
                    if (id == 1) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    model.Status = 1;
                    ProjectPartListBO.Instance.Update(model);
                }
                loadData();
                list.Clear();
            }

        }

        private void btnNoOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var list = treeListData.GetAllCheckedNodes();

            if (list.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set trạng thái này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
                    if (id == 1) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    model.Status = 0;
                    ProjectPartListBO.Instance.Update(model);
                }
                loadData();
                list.Clear();
            }

        }

        private void comboEdit_EditValueChanged(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //if (TextUtils.ToInt(comboEdit.EditValue) <= 0)
            //{
            //    return;
            //}
            //if (list.Count <= 0)
            //{
            //    MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            //if (MessageBox.Show("Bạn có chắc muốn set người phụ trách này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
            //        if (id == 0) continue;
            //        ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //        ProjectPartListJoiner.ProjectManagerID = model.EmployeeID = TextUtils.ToInt(comboEdit.EditValue);
            //        ProjectPartListBO.Instance.Update(model);
            //    }
            //    loadData();
            //    list.Clear();
            //}
            //clear();
        }


        void clear()
        {
            //dtpRequestDate.EditValue = null;
            //dtpExpectedReturnDate.EditValue = null;
            //dtpReturnDate.EditValue = null;
            //dtpOrderDate.EditValue = null;
            //comboEdit.EditValue = 0;
        }


        private void barEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //var date = TextUtils.ToDate4(dtpRequestDate.EditValue);
            //if (date == null)
            //{
            //    return;
            //}
            //if (list.Count <= 0)
            //{
            //    MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (MessageBox.Show($"Bạn có chắc muốn set Ngày yêu cầu [{date.Value.ToString("dd-MM-yyyy")}] cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
            //        if (id == 0) continue;
            //        ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //        model.RequestDate = date;
            //        ProjectPartListBO.Instance.Update(model);
            //    }
            //    loadData();
            //    list.Clear();
            //}
            //clear();
        }
        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //var date = TextUtils.ToDate4(dtpExpectedReturnDate.EditValue);
            //if (date == null)
            //{
            //    return;
            //}
            //if (list.Count <= 0)
            //{
            //    MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (MessageBox.Show($"Bạn có chắc muốn set Ngày dự kiến hàng về [{date.Value.ToString("dd-MM-yyyy")}] cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
            //        if (id == 0) continue;
            //        ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //        model.ExpectedReturnDate = date;
            //        ProjectPartListBO.Instance.Update(model);
            //    }
            //    loadData();
            //    list.Clear();
            //}
            //clear();
        }

        private void barEditItem4_EditValueChanged(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //var date = TextUtils.ToDate4(dtpOrderDate.EditValue);
            //if (date == null)
            //{
            //    return;
            //}
            //if (list.Count <= 0)
            //{
            //    MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (MessageBox.Show($"Bạn có chắc muốn set Ngày bắt đầu đặt hàng [{date.Value.ToString("dd-MM-yyyy")}] cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
            //        if (id == 0) continue;
            //        ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //        model.OrderDate = date;
            //        ProjectPartListBO.Instance.Update(model);
            //    }
            //    loadData();
            //    list.Clear();
            //}
            //clear();
        }

        private void barEditItem5_EditValueChanged(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //var date = TextUtils.ToDate4(dtpReturnDate.EditValue);
            //if (date == null)
            //{
            //    return;
            //}
            //if (list.Count <= 0)
            //{
            //    MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (MessageBox.Show($"Bạn có chắc muốn set Ngày nhận hàng [{date.Value.ToString("dd-MM-yyyy")}] cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int id = TextUtils.ToInt(treeListData.GetRowCellValue(list[i], colID));
            //        if (id == 0) continue;
            //        ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //        model.ReturnDate = date;
            //        ProjectPartListBO.Instance.Update(model);
            //    }
            //    loadData();
            //    list.Clear();
            //}
            //clear();
        }

        private void cboIsDeleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        void approvedTBP(bool isApproved)
        {
            string approvedText = isApproved ? "duyệt" : "hủy duyệt";
            bool isAciveVersion = false;
            string version = "";

            if (isFocusVersionGP)
            {
                isAciveVersion = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue("IsActive"));
                version = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
            }
            else if (isFocusVersionPO)
            {
                isAciveVersion = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsActive"));
                version = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("Code"));
            }


            //string version = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
            var listNodes = treeListData.GetAllCheckedNodes();
            if (!isAciveVersion)
            {
                MessageBox.Show($"Vui lòng chọn sử dụng phiên bản [{version}] trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning); //TN.Binh update 13/11/25
                return;
            }
            //TN.Binh update 13/11/25
            foreach (var node in listNodes)
            {
                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                // bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));

                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                if (isDelete)
                {
                    MessageBox.Show($"Không thể {approvedText} vì nhân công thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn {approvedText} các nhân công đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (var node in listNodes)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    ProjectWorkerModel model = SQLHelper<ProjectWorkerModel>.FindByID(id);
                    model.IsApprovedTBP = isApproved;
                    ProjectWorkerBO.Instance.Update(model);
                }

                loadDataWorker();
                //end
            }
        }

        //Update yêu cầu mua hàng
        void UpdatePurchaseRequest(List<ProjectPartListModel> listPartlists)
        {
            foreach (ProjectPartListModel item in listPartlists)
            {
                if (item.ID <= 0) continue;
                ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByAttribute("ProjectPartListID", item.ID).FirstOrDefault();
                request = request == null ? new ProjectPartlistPurchaseRequestModel() : request;
                request.ProjectPartListID = item.ID;
                request.EmployeeID = Global.EmployeeID;
                request.ProductCode = item.ProductCode;
                request.ProductName = item.GroupMaterial;
                request.StatusRequest = item.Status;
                request.DateRequest = item.RequestDate;
                request.DateReturnExpected = item.ExpectedReturnDate;
                request.Quantity = item.QtyFull;
                request.SupplierSaleID = item.SupplierSaleID;
                request.UnitMoney = item.UnitMoney;

                request.Quantity = item.QtyFull;
                request.UnitPrice = item.PriceOrder;
                request.TotalPrice = item.TotalPriceOrder;

                UnitCountModel unit = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", item.Unit.Trim()).FirstOrDefault();
                if (unit != null)
                {
                    request.UnitCountID = unit.ID;
                }

                if (request.ID <= 0)
                {
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(request);
                }
                else
                {
                    if (request.StatusRequest > 2) continue;
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                }

            }
        }


        ///Xoá phiên bản nhân công
        private void DeleteVersion()
        {
            int id = 0;

            if (isFocusVersionGP) id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            else id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));

            //int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;


            bool isActive = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue(colIsActive));
            string code = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colCode));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }


            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectWorkerVersionModel worker = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
                worker.IsDeleted = true;
                SQLHelper<ProjectWorkerVersionModel>.Update(worker);
                grvDataPO.DeleteSelectedRows();
                loadData();

                string sql = $"UPDATE dbo.ProjectWorker SET IsDeleted = 1," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = '{Global.LoginName}' " +
                            $"WHERE ProjectWorkerVersionID = {id}";
                TextUtils.ExcuteSQL(sql);

            }
        }

        private void barManager1_Load(object sender, EventArgs e)
        {
            //var list = treeListData.GetAllCheckedNodes();
            //foreach (var node in list)
            //{
            //    var isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
            //    if (!isApprovedTBP)
            //    {
            //        btnIsUnApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        return;
            //    }
            //    else
            //    {
            //        btnIsUnApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }

            //}
        }



        private void barManager1_QueryShowPopupMenu(object sender, DevExpress.XtraBars.QueryShowPopupMenuEventArgs e)
        {

            //var list = treeListData.GetAllCheckedNodes();
            //foreach (var node in list)
            //{
            //    var isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
            //    var isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
            //    btnIsUnApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    btnIsUnApprovedPurchase.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    btnIsApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    btnIsApprovedPurchase.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    if (!isApprovedTBP)
            //    {
            //        btnIsUnApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        btnIsApprovedTBP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    if (!isApprovedPurchase)
            //    {
            //        btnIsUnApprovedPurchase.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //        btnIsApprovedPurchase.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //}

        }

        private void groupVersion_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (TextUtils.ToString(e.Button.Properties.Tag) == "AddVersion")
            {
                btnNewVersion_Click(null, null);
            }
            else if (TextUtils.ToString(e.Button.Properties.Tag) == "EditVersion")
            {
                btnEditVersion_Click(null, null);
            }
            else
            {
                btnDeleteVersion_Click(null, null);
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ////grvDataPO.FocusedRowHandle = -1;
            isFocusVersionGP = true;
            isFocusVersionPO = false;
            ////Stopwatch stopwatch = new Stopwatch();
            ////stopwatch.Start();


            //btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = false;
            //loadData();
            loadDataWorker();

        }
        private void grvDataPO_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ////grvData.FocusedRowHandle = -1;
            isFocusVersionGP = false;
            isFocusVersionPO = true;
            ////Stopwatch stopwatch = new Stopwatch();
            ////stopwatch.Start();
            //btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = true;
            //loadData();
            ////stopwatch.Stop();
            loadDataWorker();
        }

        private void btnNewVersion_Click(object sender, EventArgs e)
        {
            btnAddVersion_Click(null, null);
        }

        private void btnEditVersion_Click(object sender, EventArgs e)
        {
            btnUpdateVersion_Click(null, null);
        }

        private void btnDeleteVersion_Click(object sender, EventArgs e)
        {
            btnRemoveVersion_Click(null, null);
        }

        //private void btnImportExcel_Click(object sender, EventArgs e)
        //{

        //    frmPartListImportExcel frm = new frmPartListImportExcel();
        //    frm.projectID = project.ID;
        //    frm.projectCode = project.ProjectCode;
        //    frm.version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
        //    frm.Text = frm.Text + " - " + TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        //loadPartListType();
        //        loadData();
        //    }
        //}

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEditVersion_Click(null, null);
        }

        private void mnuImportExcel_Click(object sender, EventArgs e)
        {
            btnImportExcelPartlist_Click(null, null);
        }

        private void btnImportExcelPartlist_Click(object sender, EventArgs e)
        {
            //string data = TextUtils.ToString(grvData.GetFocusedValue());
            //string data2 = TextUtils.ToString(grvRoomTwo.GetFocusedValue());
            //GridView view = sender as GridView;
            //var isFocusGP = grvData.IsFocusedView;
            //var isFocusPO = grvDataPO.IsFocusedView;

            //int versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            int versionId = 0;
            if (isFocusVersionGP) versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            else if (isFocusVersionPO) versionId = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));

            //if (versionId <= 0)return;
            frmPartListImportExcel frm = new frmPartListImportExcel();
            //frm.varImport = new
            //{
            //    versionId = versionId,
            //    versionCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode)),
            //    versionTypeId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectTypeID)),
            //    versionType = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectTypeName)),
            //    projectId = project.ID,
            //    projectCode = project.ProjectCode,
            //    projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID")),
            //};

            frm.var = new frmPartListImportExcel.Variable()
            {
                VersionID = versionId,
                VersionCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode)),
                ProjectTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectTypeID)),
                ProjectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectTypeName)),
                ProjectID = project.ID,
                ProjectCode = project.ProjectCode,
                ProjectSolutionID = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID")),
            };

            //frm.projectID = project.ID;
            //frm.projectCode = project.ProjectCode;
            //frm.version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            //frm.Text = frm.Text + " - " + TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));


            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadPartListType();
                loadData();
            }
        }

        private void btnAddVersion_Click(object sender, EventArgs e)
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (projectSolutionId <= 0) return;

            frmProjectPartListVersion frm = new frmProjectPartListVersion(1);
            frm.statusVersion = 1;
            frm.projectId = project.ID;
            frm.projectSolutionId = projectSolutionId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVersion();
            }
        }

        private void btnUpdateVersion_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }
            ProjectWorkerVersionModel versionModel = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
            frmProjectPartListVersion frm = new frmProjectPartListVersion(1);
            frm.projectId = project.ID;
            frm.projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            frm.workerVersion = versionModel;
            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVersion();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnRemoveVersion_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
            bool isActive = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue("IsActive"));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản nhân công mã [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }


            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectWorkerVersionModel version = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
                version.IsDeleted = true;
                SQLHelper<ProjectWorkerVersionModel>.Update(version);
                grvData.DeleteSelectedRows();


                string sql = $"UPDATE dbo.ProjectWorker SET IsDeleted = 1," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = '{Global.LoginName}' " +
                            $"WHERE ProjectWorkerVersionID = {id}";
                TextUtils.ExcuteSQL(sql);

                LoadVersion();
            }


            //DeleteVersion();
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            //if (id <= 0)
            //{
            //    return;
            //}

            //bool isActive = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsActive));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //if (isActive)
            //{
            //    MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
            //    return;
            //}

            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    ProjectWorkerVersionBO.Instance.Delete(id);
            //    grvData.DeleteSelectedRows();
            //    loadData();

            //    ProjectWorkerBO.Instance.DeleteByAttribute("ProjectWorkerVersionID", id);
            //}
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            grvData.OptionsPrint.AutoWidth = false;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"PhienBanTienDoVatTuDuAn_{project.ProjectCode}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                    optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.True;

                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = treeListData;

                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.ExportToXlsx(saveFileDialog.FileName, optionsEx);
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvDataSolution_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadVersion();
            LoadVersionPO();
        }

        private void btnAddVersionContext_Click(object sender, EventArgs e)
        {
            btnAddVersion_Click(null, null);
        }

        private void btnPriceRequest_Click(object sender, EventArgs e)
        {
            int statusVersion = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colStatusVersion));
            string statusVersionText = TextUtils.ToString(grvData.GetFocusedRowCellValue(colStatusVersionText));
            if (statusVersion == 2)
            {
                MessageBox.Show($"Danh mục vật tư phiên bản [{statusVersionText}] đã PO.\n Bạn không thể Yêu cầu báo giá!", "Thông báo");
                return;
            }

            int partListId = 0;
            var selectedNodes = treeListData.GetAllCheckedNodes();
            if (selectedNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu báo giá!", "Thông báo");
                return;
            }

            if (!CheckValidate()) return;
            foreach (TreeListNode node in selectedNodes)
            {
                partListId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                int status = TextUtils.ToInt(treeListData.GetRowCellValue(node, colStatusPriceRequest));
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                if (partListId <= 0) continue;
                if (node.HasChildren) continue;
                if (status > 0)
                {
                    MessageBox.Show($"Vật tư Stt [{stt}] đã được Y/c báo giá.\nVui lòng kiểm tra lại!", "Thông báo");
                    return;
                }
            }




            //DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu báo giá những sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.No) return;

            frmPriceRequestDetail frm = new frmPriceRequestDetail();
            frm.lblMessage.Text = "Bạn có chắc muốn yêu cầu báo giá những sản phẩm đã chọn không?";
            if (frm.ShowDialog() != DialogResult.OK) return;
            foreach (TreeListNode node in selectedNodes)
            {
                partListId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                int status = TextUtils.ToInt(treeListData.GetRowCellValue(node, colStatusPriceRequest));
                if (partListId <= 0) continue;
                if (status > 0) continue;
                ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByID(partListId);
                if (partList == null || partList.ID <= 0) continue;
                partList.StatusPriceRequest = 1;
                partList.DeadlinePriceRequest = frm.dtpDeadlinePriceRequest.Value;
                partList.DatePriceRequest = DateTime.Now;
                SQLHelper<ProjectPartListModel>.Update(partList);

                if (node.HasChildren) continue;
                ProjectPartlistPriceRequestModel priceRequest = new ProjectPartlistPriceRequestModel();
                priceRequest.ProjectPartListID = partListId;
                priceRequest.EmployeeID = Global.EmployeeID;
                priceRequest.ProductCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                priceRequest.ProductName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));
                priceRequest.StatusRequest = status + 1;
                priceRequest.DateRequest = DateTime.Now;
                priceRequest.Deadline = frm.dtpDeadlinePriceRequest.Value;
                priceRequest.Deadline = TextUtils.ToDate4(grvDataSolution.GetFocusedRowCellValue(colPriceReportDeadline));
                priceRequest.Quantity = TextUtils.ToInt(treeListData.GetRowCellValue(node, colQtyFull));

                SQLHelper<ProjectPartlistPriceRequestModel>.Insert(priceRequest);
            }

            loadData();
        }

        private void frmProjectWorker_New_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnAddSolution_Click(object sender, EventArgs e)
        {
            frmProjectSolutionDetail frm = new frmProjectSolutionDetail();
            frm.projectId = TextUtils.ToInt(project.ID);
            //frm.projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            frm.projectRequestId = 0;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProjectSolution();
            }
        }

        private void btnEditSolution_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataSolution.FocusedRowHandle;
            int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (id <= 0)
            {
                return;
            }

            frmProjectSolutionDetail frm = new frmProjectSolutionDetail();
            frm.projectSolution = SQLHelper<ProjectSolutionModel>.FindByID(id);
            frm.projectId = TextUtils.ToInt(project.ID);
            //frm.projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            frm.projectRequestId = 0;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProjectSolution();
                grvDataSolution.FocusedRowHandle = rowHandle;
            }
        }

        private void btnApprovedSolution_Click(object sender, EventArgs e)
        {
            ApprovedSolution(true, 1);
        }

        private void btnUnApprovedSolution_Click(object sender, EventArgs e)
        {
            ApprovedSolution(false, 1);
        }

        private void btnApprovedVersion_Click(object sender, EventArgs e)
        {
            ApprovedVersion(true);
        }

        private void btnUnApprovedVersion_Click(object sender, EventArgs e)
        {
            ApprovedVersion(false);
        }

        private void btnApprovedPO_Click(object sender, EventArgs e)
        {
            ApprovedSolution(true, 2);
        }

        private void btnUnApprovedPO_Click(object sender, EventArgs e)
        {
            ApprovedSolution(false, 2);
        }

        private void btnRefreshSolution_Click(object sender, EventArgs e)
        {

            int rowHandle = grvDataSolution.FocusedRowHandle;
            LoadProjectSolution();
            grvDataSolution.FocusedRowHandle = rowHandle;
        }

        private void btnRefreshVersion_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadVersion();
            grvData.FocusedRowHandle = rowHandle;
        }

        private void btnAddSolutionContext_Click(object sender, EventArgs e)
        {
            btnAddSolution_Click(null, null);
        }

        private void btnEditSolutionContext_Click(object sender, EventArgs e)
        {
            btnEditSolution_Click(null, null);
        }

        private void btnConvertVersionPO_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }

            int projectTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectTypeID));
            string projectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectTypeName));
            int solutionProjectID = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue(gridColumn9));
            var exp1 = new Expression("ProjectTypeID", projectTypeID);
            var exp2 = new Expression("StatusVersion", 2);
            var exp3 = new Expression("ProjectSolutionID", solutionProjectID);
            var versions = SQLHelper<ProjectWorkerVersionModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (versions.Count > 0)
            {
                MessageBox.Show($"Danh mục [{projectTypeName}] đã có phiên bản PO!", "Thông báo");
            }
            else
            {
                ProjectWorkerVersionModel versionModel = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);

                ProjectWorkerVersionModel newVersion = new ProjectWorkerVersionModel();

                newVersion.ProjectID = versionModel.ProjectID;
                newVersion.STT = versionModel.STT;
                newVersion.Code = versionModel.Code;
                newVersion.DescriptionVersion = versionModel.DescriptionVersion;
                newVersion.IsActive = false;
                newVersion.ProjectSolutionID = versionModel.ProjectSolutionID;
                newVersion.StatusVersion = 2;
                newVersion.ProjectTypeID = versionModel.ProjectTypeID;

                newVersion.ID = (int)ProjectWorkerVersionBO.Instance.Insert(newVersion);
                UpdateWorker(newVersion.ID, versionModel.ID);
                LoadVersion();
                LoadVersionPO();

                grvDataPO.FocusedRowHandle = grvDataPO.RowCount - 1;

                //ProjectWorkerVersionBO.Instance.Delete(id);
                ////UpdatePartlist(newVersion.ID, versionModel.ID);

                ////TextUtils.ExcuteProcedure("spUpdateProjectPartList", new string[] { "@VersionID", "@NewVersionID" }, new object[] { versionModel.ID, newVersion.ID });

                //UpdateWorker(newVersion.ID, id);
                //grvDataPO.FocusedRowHandle = grvDataPO.RowCount - 1;

                ////LoadVersion();
                //LoadVersionPO();
                //btnSearch_Click(null, null);
            }

        }

        void UpdateWorker(int newVersionId, int oldVersionId)
        {
            try
            {

                List<ProjectWorkerModel> projectWorkers = SQLHelper<ProjectWorkerModel>.FindByAttribute("ProjectWorkerVersionID", oldVersionId);
                Regex regex = new Regex(@"^-?[\d\.]+$");
                foreach (ProjectWorkerModel item in projectWorkers)
                {
                    string stt = item.TT;

                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;

                    ProjectWorkerModel worker = new ProjectWorkerModel();
                    worker.TT = stt;
                    worker.WorkContent = item.WorkContent;
                    worker.AmountPeople = item.AmountPeople;
                    worker.NumberOfDay = item.NumberOfDay;
                    worker.TotalWorkforce = item.AmountPeople * item.NumberOfDay;
                    worker.Price = item.Price;
                    worker.TotalPrice = item.Price * worker.TotalWorkforce;
                    worker.ParentID = GetParentId(stt, newVersionId);
                    worker.ProjectID = item.ProjectID;
                    worker.ProjectWorkerVersionID = newVersionId;
                    worker.ProjectTypeID = item.ProjectTypeID;

                    if (worker.ID > 0)
                    {
                        SQLHelper<ProjectWorkerModel>.Update(worker);
                    }
                    else
                    {
                        SQLHelper<ProjectWorkerModel>.Insert(worker);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int GetParentId(string tt, int versionId)
        {
            int parentId = 0;
            if (!tt.Contains(".")) return parentId;

            string parentTt = tt.Substring(0, tt.LastIndexOf(".")).Trim();

            var exp1 = new Expression("TT", parentTt);
            var exp2 = new Expression("ProjectWorkerVersionID", versionId);
            var exp3 = new Expression("IsDeleted", 1, "<>");
            ProjectWorkerModel parent = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
            if (parent != null && parent.ID > 0)
            {
                parentId = parent.ID;
            }
            return parentId;
        }

        private void btnAddVersionPO_Click(object sender, EventArgs e)
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (projectSolutionId <= 0) return;

            frmProjectPartListVersion frm = new frmProjectPartListVersion(1);
            frm.statusVersion = 2;
            frm.projectId = project.ID;
            frm.projectSolutionId = projectSolutionId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVersionPO();
            }
        }

        private void btnEditVersionPO_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataPO.FocusedRowHandle;
            int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }
            ProjectWorkerVersionModel versionModel = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
            frmProjectPartListVersion frm = new frmProjectPartListVersion(1);
            frm.projectId = project.ID;
            frm.projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            frm.workerVersion = versionModel;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVersionPO();
                grvDataPO.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDeleteVersionPO_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("Code"));
            bool isActive = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsActive"));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản nhân công mã [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }


            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectWorkerVersionModel version = SQLHelper<ProjectWorkerVersionModel>.FindByID(id);
                version.IsDeleted = true;
                SQLHelper<ProjectWorkerVersionModel>.Update(version);
                grvDataPO.DeleteSelectedRows();


                string sql = $"UPDATE dbo.ProjectWorker SET IsDeleted = 1," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = '{Global.LoginName}' " +
                            $"WHERE ProjectWorkerVersionID = {id}";
                TextUtils.ExcuteSQL(sql);

                LoadVersionPO();
            }

            //DeleteVersion();
        }



        private void btnRefreshVersionPO_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataPO.FocusedRowHandle;
            LoadVersionPO();
            grvDataPO.FocusedRowHandle = rowHandle;
        }

        void ApprovedVersionPO(bool isApproved)
        {
            //int rowHandle = grvDataPO.FocusedRowHandle;
            //string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            //int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            //if (id <= 0) return;
            //string code = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colCode));
            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} của phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    ProjectPartListVersionModel version = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
            //    if (version == null) return;
            //    version.IsApproved = isApproved;
            //    version.ApprovedID = Global.EmployeeID;

            //    SQLHelper<ProjectPartListVersionModel>.Update(version);
            //    LoadVersionPO();
            //    grvDataPO.FocusedRowHandle = rowHandle;
            //}
        }
        private void btnApprovedVersionPO_Click(object sender, EventArgs e)
        {
            ApprovedVersionPO(true);
        }

        private void btnUnApprovedVersionPO_Click(object sender, EventArgs e)
        {
            ApprovedVersionPO(false);
        }


        private void btnNewVersionPO_Click(object sender, EventArgs e)
        {
            btnAddVersionPO_Click(null, null);
        }

        private void btnUpdateVersionPO_Click(object sender, EventArgs e)
        {
            btnEditVersionPO_Click(null, null);
        }

        private void btnRemoveVersionPO_Click(object sender, EventArgs e)
        {
            btnDeleteVersionPO_Click(null, null);
        }

        private void mnuImportExcelPO_Click(object sender, EventArgs e)
        {
            btnImportExcelPartlist_Click(null, null);
        }

        private void grdDataPO_DoubleClick(object sender, EventArgs e)
        {
            btnEditVersionPO_Click(null, null);
        }

        private void btnRequestExport_Click(object sender, EventArgs e)
        {
            var selectedNodes = treeListData.GetAllCheckedNodes();
            if (selectedNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu xuất kho!", "Thông báo");
                return;
            }
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu xuất kho danh sách vật tư đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;

            List<int> ids = new List<int>();
            foreach (TreeListNode node in selectedNodes)
            {
                int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                if (id <= 0) continue;
                ids.Add(id);
            }
            if (ids.Count <= 0) return;

            string idText = string.Join(",", ids);

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartListByID", new string[] { "@ID" }, new object[] { idText });
            DataTable dt = dataSet.Tables[1];

            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, new string[] { "ProductGroupID" });
            foreach (DataRow row in distinctValues.Rows)
            {

                int productGroupID = TextUtils.ToInt(row["ProductGroupID"]);
                DataRow[] dtDetails = dt.Select($"ProductGroupID = {productGroupID}");
                if (dtDetails.Length <= 0) continue;
                var dataRow = dtDetails[0];

                BillExportModel bill = new BillExportModel();
                bill.Code = GetBillExportCode();
                //bill.WarehouseID = 1;
                //bill.RequestDate = DateTime.Now;
                bill.Status = 6;
                bill.SenderID = 0;
                bill.UserID = Global.UserID;
                bill.WarehouseType = SQLHelper<ProductGroupModel>.FindByID(productGroupID).ProductGroupName;
                bill.KhoTypeID = productGroupID;
                bill.GroupID = TextUtils.ToString(productGroupID);
                bill.WarehouseID = 1;
                bill.RequestDate = DateTime.Now;
                bill.CustomerID = TextUtils.ToInt(dataRow["CustomerID"]);
                bill.Address = TextUtils.ToString(dataRow["Address"]);

                int billExportID = SQLHelper<BillExportModel>.Insert(bill).ID;

                for (int i = 0; i < dtDetails.Length; i++)
                {
                    var dataRowDetail = dtDetails[i];
                    TreeListNode node = treeListData.FindNodeByFieldValue("ID", dataRowDetail["ID"]);
                    decimal remainQuantity = TextUtils.ToDecimal(node["RemainQuantity"]);
                    decimal qty = TextUtils.ToDecimal(node["Qty"]);
                    if (qty <= 0) continue;

                    BillExportDetailModel detail = new BillExportDetailModel();
                    detail.BillID = billExportID;
                    detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                    detail.Qty = qty;
                    detail.ProductFullName = TextUtils.ToString(dataRowDetail["GroupMaterial"]);
                    detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
                    detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
                    detail.Note = TextUtils.ToString(dataRowDetail["Note"]);
                    detail.TotalQty = TextUtils.ToInt(dataRowDetail["QtyFull"]);
                    detail.SerialNumber = "";
                    detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ID"]);
                    detail.STT = i + 1;

                    SQLHelper<BillExportDetailModel>.Insert(detail);

                }
            }

            loadData();

            //var selectedNodes = treeListData.GetAllCheckedNodes();
            //if (selectedNodes.Count <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu xuất kho!", "Thông báo");
            //    return;
            //}
            //DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu xuất kho danh sách vật tư đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog != DialogResult.Yes) return;

            //int count = 1;
            //// Group partlist by ProductGroupID
            //Dictionary<int, List<ProjectPartListModel>> ProductGroup = new Dictionary<int, List<ProjectPartListModel>>();
            //foreach (TreeListNode node in selectedNodes)
            //{
            //    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
            //    if (id <= 0) continue;
            //    ProjectPartListModel item = SQLHelper<ProjectPartListModel>.FindByID(id);
            //    if (item == null)
            //        continue;
            //    var exp1 = new Expression("ProjectPartListID", item.ID);
            //    var exp2 = new Expression("ProductCode", item.ProductCode);
            //    var requestPrice = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            //    if (requestPrice == null) continue;
            //    int productGroupID = requestPrice.ProductGroupID;
            //    if (!ProductGroup.ContainsKey(productGroupID))
            //    {
            //        ProductGroup[productGroupID] = new List<ProjectPartListModel>();
            //    }
            //    ProductGroup[productGroupID].Add(item);
            //}
            //// Insert BillExport
            //foreach (var pg in ProductGroup)
            //{
            //    int productGroupID = pg.Key;
            //    List<ProjectPartListModel> itemsInProductGroup = pg.Value;
            //    BillExportModel billExport = new BillExportModel();
            //    billExport.Code = GetBillExportCode();
            //    //billExport.CreatDate = DateTime.Now;
            //    billExport.UserID = Global.UserID;

            //    ProjectPartListModel firstItem = itemsInProductGroup.FirstOrDefault();
            //    if (firstItem != null)
            //    {
            //        var project = SQLHelper<ProjectModel>.FindByID(firstItem.ProjectID);
            //        var customer = SQLHelper<CustomerModel>.FindByID(project.CustomerID);
            //        if (customer != null)
            //        {
            //            billExport.CustomerID = customer.ID;
            //            billExport.Address = customer.Address;
            //        }
            //    }
            //    billExport.WarehouseType = SQLHelper<ProductGroupModel>.FindByID(productGroupID).ProductGroupName;
            //    billExport.Status = 6;
            //    billExport.SenderID = 0;
            //    billExport.KhoTypeID = productGroupID;
            //    billExport.GroupID = TextUtils.ToString(productGroupID);
            //    billExport.WarehouseID = 1;
            //    billExport.RequestDate = DateTime.Now;
            //    int billExportID = SQLHelper<BillExportModel>.Insert(billExport).ID;

            //    // Insert BillExportDetail
            //    foreach (var item in itemsInProductGroup)
            //    {
            //        var requestPrice = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByAttribute("ProjectPartListID", item.ID).FirstOrDefault();
            //        if (requestPrice == null)continue;

            //        var project = SQLHelper<ProjectModel>.FindByID(item.ProjectID);
            //        if (project == null) continue;

            //        BillExportDetailModel detail = new BillExportDetailModel();
            //        detail.BillID = billExportID;
            //        detail.ProductID = requestPrice.ProductSaleID;
            //        detail.Qty = item.QuantityReturn;
            //        detail.ProductFullName = item.GroupMaterial;
            //        detail.ProjectName = project.ProjectName;
            //        detail.ProjectID = item.ProjectID;
            //        detail.Note = item.Note;
            //        //detail.TotalQty = item.QtyFull;
            //        detail.SerialNumber = "";
            //        detail.ProjectPartListID = item.ID;
            //        detail.STT = count;
            //        count++;

            //        SQLHelper<BillExportDetailModel>.Insert(detail);
            //    }
            //}
        }

        private void btnRequestImport_Click(object sender, EventArgs e)
        {
            var nodeSelecteds = treeListData.GetAllCheckedNodes();
            if (nodeSelecteds.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn PO muốn yêu cầu nhập kho!", "Thông báo");
                return;
            }
            foreach (TreeListNode node in nodeSelecteds)
            {
                if (node.HasChildren == true)
                {
                    MessageBox.Show("Không chọn các hàng có các mục con!", "Thông báo");
                    return;
                }
            }


            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu nhập kho danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> ids = new List<int>();
                foreach (TreeListNode node in nodeSelecteds)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    if (id <= 0) continue;
                    ids.Add(id);
                }
                if (ids.Count <= 0) return;
                string idText = string.Join(",", ids);
                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListByID", "A", new string[] { "@ID" }, new object[] { idText });
                DataView view = new DataView(dt);

                DataTable distinctValues = view.ToTable(true, new string[] { "SuplierSaleFinalID", "ProductGroupID" });

                foreach (DataRow row in distinctValues.Rows)
                {
                    int supplierSaleFinalID = TextUtils.ToInt(row["SuplierSaleFinalID"]);
                    string productGroupID = TextUtils.ToString(row["ProductGroupID"]);
                    DataRow[] dtDetails = dt.Select($"SuplierSaleFinalID = {supplierSaleFinalID} AND ProductGroupID = {productGroupID}");
                    if (dtDetails.Length <= 0) continue;
                    var dataRow = dtDetails[0];

                    //insert bill
                    BillImportModel bill = new BillImportModel();
                    bill.BillImportCode = GetBillImportCode();
                    bill.CreatDate = DateTime.Now;
                    bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
                    bill.Reciver = "Admin kho";//
                    bill.Status = false;
                    bill.Suplier = TextUtils.ToString(dataRow["NameNCCFinal"]);
                    bill.BillType = false;
                    bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
                    bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
                    bill.SupplierID = TextUtils.ToInt(dataRow["SuplierSaleFinalID"]);
                    bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
                    bill.ReciverID = 0;//Get Admin kho
                    bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
                    bill.WarehouseID = 1;
                    bill.BillTypeNew = 4;//Trạng thái yêu cầu nhập kho
                    bill.DateRequestImport = DateTime.Now;

                    bill.ID = SQLHelper<BillImportModel>.Insert(bill).ID;
                    TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

                    //Insert BillImportDetail
                    decimal totalQty = 0;
                    for (int i = 0; i < dtDetails.Length; i++)
                    {
                        var dataRowDetail = dtDetails[i];

                        //decimal quantityFull = TextUtils.ToDecimal(dataRowDetail["QtyFull"]);
                        // if (quantityFull < 0) continue;
                        BillImportDetailModel detail = new BillImportDetailModel();
                        detail.BillImportID = bill.ID;
                        detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                        //detail.Qty = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
                        //detail.Qty = TextUtils.ToDecimal(dataRowDetail["QtyFull"]);
                        detail.Price = TextUtils.ToDecimal(dataRowDetail["Price"]);

                        detail.TotalPrice = detail.Qty * detail.Price;
                        detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
                        //detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
                        detail.SomeBill = "";
                        //detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
                        detail.STT = i + 1;
                        detail.TotalQty = totalQty;
                        detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]); ;
                        //detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
                        detail.SerialNumber = "";
                        detail.CodeMaPhieuMuon = "";
                        detail.BillExportDetailID = 0;

                        SQLHelper<BillImportDetailModel>.Insert(detail);
                    }

                }
            }
        }
        string GetBillExportCode()
        {
            DateTime dateNow = DateTime.Now;
            string year = dateNow.ToString("yy");
            string month = dateNow.ToString("MM");
            string day = dateNow.ToString("dd");

            string code = $"PXK{year}{month}{day}";
            //Get bill code mới nhất
            var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            BillExportModel bill = SQLHelper<BillExportModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();

            string currentCode = bill == null ? "" : bill.Code.Trim();
            int stt = string.IsNullOrEmpty(currentCode) ? 1 : TextUtils.ToInt(currentCode.Substring(currentCode.Length - 3)) + 1;
            string sttText = stt.ToString();
            while (sttText.Length < 3)
            {
                sttText = "0" + sttText;
            }
            code = code + sttText;
            return code;
        }

        string GetBillImportCode()
        {
            DateTime dateNow = DateTime.Now;
            string code = $"PNK{dateNow.ToString("yyyyMMdd")}";
            //Get bill code mới nhất
            var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            BillImportModel bill = SQLHelper<BillImportModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();

            string currentCode = bill == null ? "" : bill.BillImportCode.Trim();
            int stt = string.IsNullOrEmpty(currentCode) ? 1 : TextUtils.ToInt(currentCode.Substring(currentCode.Length - 3)) + 1;
            string sttText = stt.ToString();
            while (sttText.Length < 3)
            {
                sttText = "0" + sttText;
            }
            string billImportCode = code + sttText;
            return billImportCode;
        }

        private void btnCancelPriceRequest_Click(object sender, EventArgs e)
        {
            var selectedNodes = treeListData.GetAllCheckedNodes();
            if (selectedNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn huỷ yêu cầu báo giá!", "Thông báo");
                return;
            }

            foreach (TreeListNode node in selectedNodes)
            {
                int partListId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                if (partListId <= 0) continue;
                if (node.HasChildren) continue;

                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                bool isCheckPrice = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsCheckPrice));
                if (isCheckPrice)
                {
                    MessageBox.Show($"Phòng mua đang check giá sản phẩm Stt [{stt}].\nBạn không thể Huỷ Y/c báo giá!", "Thông báo");
                    return;
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu báo giá những sản phẩm đã chọn không?\n" +
                                                    "(Lưu ý: Bạn không thể huỷ Y/c của người khác.)", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            foreach (TreeListNode node in selectedNodes)
            {
                int partListId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                int employeeId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colEmployeeIDRequestPrice));
                if (partListId <= 0) continue;
                if (Global.EmployeeID != employeeId) continue;
                if (node.HasChildren) continue;

                var exp1 = new Expression("ProjectPartListID", partListId);
                var exp2 = new Expression("EmployeeID", employeeId);
                ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                if (priceRequest == null) continue;
                priceRequest.IsDeleted = true;
                SQLHelper<ProjectPartlistPriceRequestModel>.Update(priceRequest);

                ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByID(partListId);
                if (partList == null || partList.ID <= 0) continue;
                partList.StatusPriceRequest = 0;
                partList.DatePriceRequest = null;
                SQLHelper<ProjectPartListModel>.Update(partList);
            }

            loadData();
        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void grvDataPO_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ////grvData.FocusedRowHandle = -1;
            //isFocusVersionGP = false;
            //isFocusVersionPO = true;
            ////Stopwatch stopwatch = new Stopwatch();
            ////stopwatch.Start();
            //btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = true;
            //loadData();
        }


        private void grvData_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            isFocusVersionGP = true;
            isFocusVersionPO = false;

            btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = toolStripSeparator6.Visible = toolStripSeparator7.Visible = false;
            treeListData.Tag = 1;
            treeListData.Columns["IsApprovedTBPText"].VisibleIndex = -1;
            loadDataWorker();
        }

        private void grvDataPO_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            isFocusVersionGP = false;
            isFocusVersionPO = true;
            btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = toolStripSeparator6.Visible = toolStripSeparator7.Visible = true;
            treeListData.Tag = 2;
            treeListData.Columns["IsApprovedTBPText"].VisibleIndex = 1;
            loadDataWorker();
        }

        private bool CheckValidate()
        {
            //bool result = true;
            var selectedNodes = treeListData.GetAllCheckedNodes();
            foreach (TreeListNode node in selectedNodes)
            {
                int partListId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                if (partListId <= 0) continue;
                if (node.HasChildren) continue;

                int status = TextUtils.ToInt(treeListData.GetRowCellValue(node, colStatusPriceRequest));
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));

                string Manufacturer = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer));
                string ProductCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                string GroupMaterial = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));

                int QtyFull = TextUtils.ToInt(treeListData.GetRowCellValue(node, colQtyFull));
                int QtyMin = TextUtils.ToInt(treeListData.GetRowCellValue(node, colQtyMin));
                //if (status > 0)
                //{
                //    MessageBox.Show($"Vật tư Stt [{stt}] đã được Y/c báo giá.\nVui lòng kiểm tra lại!", "Thông báo");
                //    return false;
                //}

                if (string.IsNullOrWhiteSpace(GroupMaterial))
                {
                    MessageBox.Show($"[Tên vật tư] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(ProductCode))
                {
                    MessageBox.Show($"[Mã vật tư] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(Manufacturer))
                {
                    MessageBox.Show($"[Hãng SX] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }

                if (QtyFull <= 0)
                {
                    MessageBox.Show($"[Số lượng tổng] có số thứ tự [{stt}] phải > 0!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }

                if (QtyMin <= 0)
                {
                    MessageBox.Show($"[Số lượng / 1 máy] có số thứ tự [{stt}] phải > 0!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
            }


            return true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            treeListData.CheckAll();
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            treeListData.UncheckAll();
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            int projectId = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colProjectID));

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
            if (project == null) return;
            if (!project.CreatedDate.HasValue) return;

            string solutionCode = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution)).Trim();

            string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solutionCode}/2D/GC/DH";


            List<TreeListNode> rowSelecteds = treeListData.GetAllCheckedNodes();
            if (rowSelecteds.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn tải file!", "Thông báo");
                return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string productCode = "";
                try
                {
                    foreach (TreeListNode node in rowSelecteds)
                    {
                        productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                        if (string.IsNullOrEmpty(productCode)) continue;
                        string pathDowload = Path.Combine(fbd.SelectedPath, $"{productCode}.pdf");
                        string url = $"http://113.190.234.64:8083/api/project/{pathPattern}/{productCode}.pdf";

                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(url, pathDowload);
                        Process.Start(pathDowload);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File [{productCode}.pdf] không tồn tại!\n{ex.Message}", "Thông báo");
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            int type = TextUtils.ToInt(treeListData.Tag);
            loadDataWorker();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            int version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            int versionBO = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersionPO));
            int versionID = version == 0 ? versionBO : version;
            frmProjectWorkerDetail frm = new frmProjectWorkerDetail();
            frm.projectID = project.ID;
            frm.ProjectWorkerVersionID = versionID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int type = TextUtils.ToInt(treeListData.Tag);
                loadDataWorker();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            int versionBO = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersionPO));
            int versionID = version == 0 ? versionBO : version;
            int workerID = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
            bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetFocusedRowCellValue(colIsApprovedTBP));
            string stt = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colTT));
            if (treeListData.FocusedNode.HasChildren)
            {
                return;
            }
            if (workerID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isApprovedTBP)
            {
                MessageBox.Show($"Nhân công TT[{stt}] đã được TBP duyệt! Vui lòng chọn lại.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmProjectWorkerDetail frm = new frmProjectWorkerDetail();
            frm.projectID = project.ID;
            frm.ID = workerID;
            frm.ProjectWorkerVersionID = versionID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int type = TextUtils.ToInt(treeListData.Tag);
                loadDataWorker();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var listNode = treeListData.GetAllCheckedNodes();
            if (listNode.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (var node in listNode)
            {
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                if (isApprovedTBP)
                {
                    MessageBox.Show($"Nhân công TT[{stt}] đã được TBP duyệt! Vui lòng chọn lại.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in listNode)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(item, colID));
                    ProjectWorkerModel model = (ProjectWorkerModel)ProjectWorkerBO.Instance.FindByPK(id);
                    model.IsDeleted = true;
                    ProjectWorkerBO.Instance.Update(model);
                }
                int type = TextUtils.ToInt(treeListData.Tag);
                loadDataWorker();
            }
        }
        private void btnApprovedTBP_Click(object sender, EventArgs e)
        {
            approvedTBP(true);
        }
        private void btnUnapprovedTBP_Click(object sender, EventArgs e)
        {
            approvedTBP(false);
        }
        private void cboStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = TextUtils.ToInt(treeListData.Tag);
            loadDataWorker();
        }
        private void cboDeleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = TextUtils.ToInt(treeListData.Tag);
            loadDataWorker();
        }

        private void btnWorkerSynthetic_Click(object sender, EventArgs e)
        {
            frmProjectWorkerSynthetic frm = new frmProjectWorkerSynthetic();
            frm.projectID = project.ID;
            frm.ShowDialog();
        }
        private void btlExcel_Click(object sender, EventArgs e)
        {
            int versionId = 0;
            string versionCode = "";
            int projectTypeID = 0;
            string projectTypeName = "";
            if (isFocusVersionGP)
            {
                versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                versionCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
                projectTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectTypeID));
                projectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectTypeName));
            }
            else if (isFocusVersionPO)
            {
                versionId = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                versionCode = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colCode));
                projectTypeID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colProjectTypeID));
                projectTypeName = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colProjectTypeName));
            }

            frmProjectWorkerImportExcel frm = new frmProjectWorkerImportExcel();
            frm.var = new frmProjectWorkerImportExcel.Variable()
            {
                ProjectID = project.ID,
                ProjectCode = project.ProjectCode,
                ProjectSolutionID = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID")),
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int type = TextUtils.ToInt(treeListData.Tag);
                loadDataWorker();
            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //treeListData.OptionsPrint.AutoWidth = false;
            //treeListColumn5.VisibleIndex = -1;
            //try
            //{
            //    string versionCode = isFocusVersionGP ? TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectTypeName")) : TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("ProjectTypeName"));

            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            //    saveFileDialog.FileName = $"NhanConguDuAn_{project.ProjectCode}_{versionCode}.xlsx";
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
            //        optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            //        optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.True;

            //        PrintingSystem printingSystem = new PrintingSystem();

            //        PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
            //        printableComponentLink1.Component = treeListData;

            //        CompositeLink compositeLink = new CompositeLink(printingSystem);
            //        compositeLink.Links.Add(printableComponentLink1);
            //        compositeLink.ExportToXlsx(saveFileDialog.FileName, optionsEx);
            //        Process.Start(saveFileDialog.FileName);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    treeListColumn5.VisibleIndex = 1;
            //}


            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                int versionID = 0;
                string projectTypeName = "";
                if (isFocusVersionGP)
                {
                    versionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                    projectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectTypeName"));
                }
                else if (isFocusVersionPO)
                {
                    versionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                    projectTypeName = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("ProjectTypeName"));
                }


                string filepath = Path.Combine(f.SelectedPath, $"NhanCongDuAn_{project.ProjectCode}_{projectTypeName}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                frmProjectWorkerSummary frm = new frmProjectWorkerSummary();
                frm.dt = dt;
                frm.project = project;
                frm.workerVersionID = versionID;
                frm.LoadData();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = frm.grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;


                frm.grvData.OptionsPrint.AutoWidth = false;
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

        private void grvDataSolution_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //e.Appearance.BackColor = Color.DeepPink;
            //e.Appearance.ForeColor = Color.White;
            //e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int partlistVersionID = 0;
            string projectTypeName = "";
            if (isFocusVersionGP)
            {
                partlistVersionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                projectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectTypeName"));
            }
            else if (isFocusVersionPO)
            {
                partlistVersionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                projectTypeName = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("ProjectTypeName"));
            }

            frmProjectWorkerSummary frm = new frmProjectWorkerSummary();
            frm.dt = dt;
            frm.project = project;

            //int partlistVersionID = 0;
            //if (isFocusVersionGP) partlistVersionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //else if (isFocusVersionPO) partlistVersionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));

            frm.workerVersionID = partlistVersionID;
            frm.projectTypeName = projectTypeName;
            frm.Show();
        }

        private void treeListData_NodeCellStyle_1(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                //return;
            }

            bool isDeleted = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsDeleted));
            //bool isProblem = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsProblem));

            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            //else if (isProblem)
            //{
            //    e.Appearance.BackColor = Color.Orange;
            //}
        }

        private void treeListData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void treeListData_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AmountPeople"
               || e.Column.FieldName == "NumberOfDay"
               || e.Column.FieldName == "TotalWorkforce"
               || e.Column.FieldName == "Price")
            {
                //int parentID = TextUtils.ToInt(.GetRowCellValue(e.ListSourceRowIndex, "ParentID"));
                //int countChild = TextUtils.ToInt(grvData.GetRowCellValue(e.ListSourceRowIndex, "CountChild"));
                if (e.Node.HasChildren)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}