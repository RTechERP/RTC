using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectWorker : _Forms
    {
        public int projectID = 0;
        public string projectCode = "";
        public string projectName = "";
        public frmProjectWorker()
        {
            InitializeComponent();
        }

        private void frmProjectWorker_Load(object sender, EventArgs e)
        {
            this.Text = $"Nhân công dự án {projectCode} - {projectName}";
            cboStatusTBP.SelectedIndex = 0;
            cboDeleted.SelectedIndex = 1;
            loadType();
            LoadVersion();
            loadData();
        }

        void loadType()
        {
            List<ProjectWorkerTypeModel> listType = SQLHelper<ProjectWorkerTypeModel>.FindAll();
            cboProjectWorkerType.Properties.DataSource = listType;
            cboProjectWorkerType.Properties.ValueMember = "ID";
            cboProjectWorkerType.Properties.DisplayMember = "Name";

            //if (listType.Count <= 0) return;
            //cboProjectWorkerType.EditValue = listType[0].ID;
        }

        void loadData()
        {
            //loadType();
            int type = TextUtils.ToInt(cboProjectWorkerType.EditValue);
            int isApprovedTBP = cboStatusTBP.SelectedIndex - 1;
            int isDeleted = cboDeleted.SelectedIndex - 1;
            int version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectWorker", "A",
                new string[] { "@ProjectID", "@ProjectWorkerTypeID", "@IsApprovedTBP", "@IsDeleted", "@KeyWord", "@ProjectWorkerVersion" },
                new object[] { projectID, type, isApprovedTBP, isDeleted, txtFind.Text.Trim(),version });
            if(dt.Rows.Count > 0)
            {
                CalculateWork(dt);
            }
            treeListData.DataSource = dt;
            treeListData.ParentFieldName = "ParentID";
            treeListData.KeyFieldName = "ID";
            treeListData.ExpandAll();
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
            dataTable.Rows[rowIndex]["AmountPeople"] = numberOfPeople + totalamountPeopleFromChildren;
            dataTable.Rows[rowIndex]["NumberOfDay"] = numberOfDays + totalnumberOfDaysFromChildren;
            dataTable.Rows[rowIndex]["Price"] = laborCostPerDay + totallaborCostPerDayFromChildren;
            dataTable.Rows[rowIndex]["TotalWorkforce"] = totalLabor;
            dataTable.Rows[rowIndex]["TotalPrice"] = totalCost;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btlExcel_Click(object sender, EventArgs e)
        {
            frmProjectWorkerImportExcel frm = new frmProjectWorkerImportExcel();

            frm.projectID = projectID;
            frm.projectCode = projectCode;
            frm.version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            frm.Text = frm.Text + " - " + TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProjectWorkerDetail frm = new frmProjectWorkerDetail();
            frm.projectID = projectID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
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
            frm.projectID = projectID;
            frm.ID = workerID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
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
                loadData();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.True;
            //    optionsEx.AllowFixedColumns = DevExpress.Utils.DefaultBoolean.True;
            //    optionsEx.SheetName = cboProjectWorkerType.Text;

            //    try
            //    {
            //        string filepath = $"{f.SelectedPath}/NhanConguDuAn_{projectCode}.xls";

            //        // Export the TreeList data including appearance settings to Excel
            //        treeListData.ExportToXls(filepath, optionsEx);

            //        // Open the exported Excel file
            //        Process.Start(filepath);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //    treeListData.ClearSelection();
            //}

            treeListData.OptionsPrint.AutoWidth = false;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"NhanConguDuAn_{projectCode}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    //optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.True;

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

        void approvedTBP(bool isApproved)
        {
            string approvedText = isApproved ? "duyệt" : "hủy duyệt";

            var listNodes = treeListData.GetAllCheckedNodes();
            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in listNodes)
            {
                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));

                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                if (isDelete)
                {
                    MessageBox.Show($"Không thể {approvedText} vì nhân công thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (isApproved && isApprovedTBP)
                //{
                //    MessageBox.Show($"Không thể {approvedText} vì nhân công thứ tự [{stt}] đã được TBP duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //if (!isApproved && !isApprovedTBP)
                //{
                //    MessageBox.Show($"Không thể {approvedText} vì nhân công thứ tự [{stt}] chưa được TBP duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
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
                loadData();
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

        private void treeListData_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            treeListData.FocusedNode = e.Node;
        }
        private void treeListData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
            }
        }
        private void treeListData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {

            bool value = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsDeleted));
            if (value)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void cboDeleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboProjectWorkerType_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnWorkerSynthetic_Click(object sender, EventArgs e)
        {
            frmProjectWorkerSynthetic frm = new frmProjectWorkerSynthetic();
            frm.projectID = projectID;
            frm.ShowDialog();
        }

        private void repositoryItemTextEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void repositoryItemTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listNodes = treeListData.GetAllCheckedNodes();
                if (listNodes.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                barManager1.CloseMenus();
                int value = TextUtils.ToInt(txtPrice.EditValue);
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn lưu Đơn giá là: {value.ToString("#,###.##", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"))}", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    txtPrice.EditValue = "";
                    foreach (var node in listNodes)
                    {
                        int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                        ProjectWorkerModel model = SQLHelper<ProjectWorkerModel>.FindByID(id);
                        model.Price = value;
                        ProjectWorkerBO.Instance.Update(model);
                    }
                    loadData();
                }
                else
                {

                }
            }
        }

        private void repositoryItemTextEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void repositoryItemTextEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listNodes = treeListData.GetAllCheckedNodes();
                if (listNodes.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                barManager1.CloseMenus();
                int value = TextUtils.ToInt(txtNumberOfDay.EditValue);
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn lưu Số ngày là: {value.ToString("#,###.##", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"))}", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    txtPrice.EditValue = "";
                    foreach (var node in listNodes)
                    {
                        int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                        ProjectWorkerModel model = SQLHelper<ProjectWorkerModel>.FindByID(id);
                        model.NumberOfDay = value;
                        ProjectWorkerBO.Instance.Update(model);
                    }
                    loadData();
                }
                else
                {

                }
            }
        }

        private void repositoryItemTextEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void repositoryItemTextEdit3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                var listNodes = treeListData.GetAllCheckedNodes();
                if (listNodes.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                barManager1.CloseMenus();
                int value = TextUtils.ToInt(txtAmountPeople.EditValue);
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn lưu Số người là: {value.ToString("#,###.##", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"))}", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    txtPrice.EditValue = "";
                    foreach (var node in listNodes)
                    {
                        int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                        ProjectWorkerModel model = SQLHelper<ProjectWorkerModel>.FindByID(id);
                        model.AmountPeople = value;
                        ProjectWorkerBO.Instance.Update(model);
                    }
                    loadData();
                }
                else
                {

                }
            }
        }

        private void treeListData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }



        #region QUẢN LÝ PHIÊN BẢN

        void LoadVersion()
        {
            List<ProjectWorkerVersionModel> listVersion = SQLHelper<ProjectWorkerVersionModel>.FindByAttribute("ProjectID", projectID).OrderByDescending(x => x.STT).ToList();
            grdData.DataSource = listVersion;
        }

        private void btnAddVersion_Click(object sender, EventArgs e)
        {
            frmProjectPartListVersion frm = new frmProjectPartListVersion(1);
            frm.projectSolutionId = projectID;
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
            frm.projectSolutionId = projectID;
            frm.workerVersion = versionModel;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVersion();
                grvData.FocusedRowHandle = rowHandle;
                loadData();
            }
        }

        private void btnRemoveVersion_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }

            bool isActive = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsActive));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectWorkerVersionBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
                loadData();

                ProjectWorkerBO.Instance.DeleteByAttribute("ProjectWorkerVersionID", id);
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadData();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateVersion_Click(null, null);
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmProjectWorkerImportExcel frm = new frmProjectWorkerImportExcel();
            frm.projectID = projectID;
            frm.projectCode = projectCode;
            frm.version = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            frm.Text = frm.Text + " - " + TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadType();
                loadData();
            }
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

        private void mnuImportExcel_Click(object sender, EventArgs e)
        {
            btnImportExcel_Click(null, null);
        }
        

        private void btnExportExcelVersion_Click(object sender, EventArgs e)
        {
            grvData.OptionsPrint.AutoWidth = false;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"PhienBanNhanConguDuAn_{projectCode}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    //optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.True;

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

        #endregion
    }
}