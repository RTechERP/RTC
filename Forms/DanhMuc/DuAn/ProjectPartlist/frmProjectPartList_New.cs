using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Forms.DanhMuc.DuAn.ProjectPartlist;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPartList_New : _Forms
    {
        //public int ProjectID;
        //public string projectCode;
        //public string projectName;

        bool isFocusVersionGP = false;
        bool isFocusVersionPO = false;

        public ProjectModel project = new ProjectModel();
        DataTable dt = new DataTable();
        bool isTBP = false;

        //TODO: HuyNT update 10/09/2024
        public bool isSelectPartlist = false;
        public DataTable dtAddDetail = new DataTable();
        //DataTable dt = new DataTable();
        public List<int> listIDInsert = new List<int>();
        public int nodeMinLevelCount = 0;


        // VTN update 24/8---------------------------------------
        public decimal AmountSpent1, AmountSpent2, AmountSpent3;
        public int rowNumber;
        public string ct1, ct2, ct3, NameNode;
        public int PLV1, PLV2;
        //-------------------------------------------------------


        List<int> listPartListVersionId = new List<int>();
        List<int> listPartListVersionChilId = new List<int>();
        public string reasionProblem = "";
        public frmProjectPartList_New(bool tbp)
        {
            InitializeComponent();

            //if (tbp) //LinhTN update 17/08/2024
            {
                isTBP = tbp;
                stackPanel2.Visible = tbp;

                toolStrip2.Visible = !tbp;
                toolStrip3.Visible = !tbp;
                toolStrip4.Visible = !tbp;

                btnNew.Visible = !tbp;
                btnEdit.Visible = !tbp;
                btnDelete.Visible = !tbp;
                btnPriceRequest.Visible = !tbp;
                btnCancelPriceRequest.Visible = !tbp;
                btnApprovedBuy.Visible = !tbp;
                btnUnapprovedTBP.Visible = !tbp;
                //btnRequestExport.Visible = !tbp;
                //btnRequestImport.Visible = !tbp;

                btnTBPApprovedNew.Visible = !tbp; // VTN update 6325
                btnUnApprovedNew.Visible = !tbp; // VTN update 6325

                toolStripSeparator1.Visible = !tbp;
                toolStripSeparator2.Visible = !tbp;
                toolStripSeparator3.Visible = !tbp;
                toolStripSeparator12.Visible = !tbp;
                toolStripSeparator30.Visible = !tbp;
                toolStripSeparator6.Visible = !tbp;
                toolStripSeparator8.Visible = !tbp;
                toolStripSeparator29.Visible = !tbp;

                contextMenuStrip1.Enabled = !tbp;
                contextMenuStrip2.Enabled = !tbp;
                contextMenuStrip3.Enabled = !tbp;

                btnImportExcelPartlist.Visible = !tbp;
                splitContainerControl5.PanelVisibility = tbp ? SplitPanelVisibility.Panel2 : SplitPanelVisibility.Both;
            }
        }

        private void frmProjectPartList_New_Load(object sender, EventArgs e)
        {
            string caption = isTBP ? "TBP DUYỆT" : "";
            this.Text = $"{caption} VẬT TƯ DỰ ÁN {project.ProjectCode} - {project.ProjectName}";

            cboIsDeleted.SelectedIndex = 1;
            cboStatusTBP.SelectedIndex = 0;
            cboStatusPur.SelectedIndex = 0;

            //loadPartListType();
            loadPersonManager();

            LoadProject();
            LoadProjectSolution();
            LoadVersion();
            //loadData();

            //Thread.Sleep(2000);
            //dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;

        }

        void loadPersonManager()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM dbo.Employee");
            //cboUserCharge.DataSource = dt;
            //cboUserCharge.DisplayMember = "FullName";
            //cboUserCharge.ValueMember = "ID";
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }

        void LoadProjectSolution()
        {
            //int projectID = isTBP ? TextUtils.ToInt(cboProject.EditValue) : project.ID;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectSolution", "A", new string[] { "@ProjectID" }, new object[] { project.ID });
            grdDataSolution.DataSource = dt;

            if (isTBP) grvDataSolution.FocusedRowHandle = 0;
            grvDataSolution.FocusedRowHandle = 0;
        }

        //private void loadPartListType()
        //{
        //    List<ProjectPartListTypeModel> listType = SQLHelper<ProjectPartListTypeModel>.FindAll();
        //    cboPartListType.Properties.DataSource = listType;
        //    cboPartListType.Properties.ValueMember = "ID";
        //    cboPartListType.Properties.DisplayMember = "Name";
        //    if (listType.Count <= 0) return;
        //    cboPartListType.EditValue = listType[0].ID;
        //}

        void LoadVersion()
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListVersion", "A",
                                                    new string[] { "@ProjectSolutionID", "@StatusVersion" },
                                                    new object[] { projectSolutionId, 1 });
            grdData.DataSource = dt;

            treeListData.DataSource = null;
        }

        void LoadVersionPO()
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListVersion", "A",
                                                    new string[] { "@ProjectSolutionID", "@StatusVersion" },
                                                    new object[] { projectSolutionId, 2 });
            grdDataPO.DataSource = dt;
            treeListData.DataSource = null;
            //if (isTBP) grvDataPO.FocusedRowHandle = 0;
        }

        void loadData()
        {
            treeListData.DataSource = null;
            int version = 0;
            int partListTypeID = 0;
            //var isFocusGP = grvData.IsFocusedView;
            //var isFocusPO = grvDataPO.IsFocusedView;

            //int versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //int versionId = 0;
            if (isFocusVersionGP)
            {
                version = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                partListTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectTypeID"));
            }
            else if (isFocusVersionPO)
            {
                version = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                partListTypeID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ProjectTypeID"));
            }

            //version = 19;

            int isDelete = cboIsDeleted.SelectedIndex - 1;
            int isApprovedTBP = cboStatusTBP.SelectedIndex - 1;
            int isApprovedPur = cboStatusPur.SelectedIndex - 1;
            //int type = TextUtils.ToInt(cboPartListType.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
                dt = TextUtils.LoadDataFromSP("spGetProjectPartList_Khanh", "A",
                                        new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                        new object[] { project.ID, partListTypeID, isDelete, txtKeyword.Text.Trim(), isApprovedTBP, isApprovedPur, version });
                if (dt.Rows.Count > 0)
                {
                    //CalculateWork(dt);
                    //CalculateAllWork(dt);
                }

                //treeListData.BeginUpdate();
                //treeListData.LockReloadNodes();
                //Lib.LockEvents = true;
                //treeListData.DataSource = null;
                treeListData.DataSource = dt;

                //Lib.LockEvents = false;
                //treeListData.UnlockReloadNodes();
                //treeListData.EndUpdate();

                //treeListData.Refresh();
                //treeListData.ParentFieldName = "ParentID";
                //treeListData.KeyFieldName = "ID";
                treeListData.ExpandAll();
                //popupMenu1.HidePopup();

                //treeListData.FocusedNode = treeListData.Nodes.LastNode;

                CalculatorData();
                CheckUpdateIsApprovedNewCode(true, null);
            }
        }

        void CalculateWork(DataTable dataTable, int rowIndex = 0)
        {
            int qtyFull = TextUtils.ToInt(dataTable.Rows[rowIndex]["QtyFull"]);
            decimal price = TextUtils.ToDecimal(dataTable.Rows[rowIndex]["Price"]);

            decimal priceQuote = TextUtils.ToDecimal(dataTable.Rows[rowIndex]["UnitPriceQuote"]);
            decimal pricePurchase = TextUtils.ToDecimal(dataTable.Rows[rowIndex]["UnitPricePurchase"]);

            decimal totalPriceFromChildren = 0;
            decimal totalAmountFromChildren = 0;

            decimal totalPriceFromChildrenQuote = 0;
            decimal totalAmountFromChildrenQuote = 0;

            decimal totalPriceFromChildrenPurchase = 0;
            decimal totalAmountFromChildrenPurchase = 0;

            decimal totalPriceExchangePurchaseChild = 0;
            decimal totalPriceExchangeQuoteChild = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int childRowIndex = dataTable.Rows.IndexOf(row);

                int parentID = TextUtils.ToInt(row["ParentID"]);
                int id = TextUtils.ToInt(dataTable.Rows[rowIndex]["ID"]);

                if (row["ParentID"] != DBNull.Value && parentID == id)
                {
                    CalculateWork(dataTable, childRowIndex);


                    decimal laborFromChild = TextUtils.ToDecimal(row["Price"]);
                    decimal costFromChild = TextUtils.ToDecimal(row["Amount"]);
                    decimal unitPriceQuote = TextUtils.ToDecimal(row["UnitPriceQuote"]);
                    decimal totalPriceQuote = TextUtils.ToDecimal(row["TotalPriceQuote"]);
                    decimal unitPricePurchase = TextUtils.ToDecimal(row["UnitPricePurchase"]);
                    decimal totalPricePurchase = TextUtils.ToDecimal(row["TotalPricePurchase"]);

                    totalPriceFromChildren += laborFromChild;
                    totalAmountFromChildren += costFromChild;

                    totalPriceFromChildrenQuote += unitPriceQuote;
                    totalAmountFromChildrenQuote += totalPriceQuote;

                    totalPriceFromChildrenPurchase += unitPricePurchase;
                    totalAmountFromChildrenPurchase += totalPricePurchase;

                    totalPriceExchangePurchaseChild += TextUtils.ToDecimal(row[colTotalPriceExchangePurchase.FieldName]);
                    totalPriceExchangeQuoteChild += TextUtils.ToDecimal(row[colTotalPriceExchangeQuote.FieldName]);
                }
            }


            decimal totalLabor = price + totalPriceFromChildren;
            decimal totalCost = qtyFull * price + totalAmountFromChildren;

            decimal totalUnitQuote = priceQuote + totalPriceFromChildrenQuote;
            decimal totalAmountQuote = qtyFull * priceQuote + totalAmountFromChildrenQuote;

            decimal totalUnitPurchase = pricePurchase + totalPriceFromChildrenPurchase;
            decimal totalAmountPurchase = qtyFull * pricePurchase + totalAmountFromChildrenPurchase;

            decimal totalPriceExchangePurchase = pricePurchase + totalPriceExchangePurchaseChild;
            decimal totalPriceExchangeQuote = qtyFull * pricePurchase + totalPriceExchangeQuoteChild;

            dataTable.Rows[rowIndex]["Price"] = totalLabor;
            dataTable.Rows[rowIndex]["Amount"] = totalCost;

            dataTable.Rows[rowIndex]["TotalPriceQuote"] = totalAmountQuote;
            dataTable.Rows[rowIndex]["TotalPricePurchase"] = totalAmountPurchase;

            dataTable.Rows[rowIndex][colTotalPriceExchangePurchase.FieldName] = totalPriceExchangePurchase;
            dataTable.Rows[rowIndex][colTotalPriceExchangeQuote.FieldName] = totalPriceExchangeQuote;


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
            int statusSolution = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue(colStatusSolution));
            if (isApproved && status == 2)
            {
                if (statusSolution == 0)
                {
                    MessageBox.Show("Bạn không thể duyệt PO cho giải pháp không có PO!", "Thông báo");
                    return;
                }
            }
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
            int rowHandle = grvData.FocusedRowHandle;
            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} của phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectPartListVersionModel version = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
                if (version == null) return;
                version.IsApproved = isApproved;
                version.ApprovedID = Global.EmployeeID;

                SQLHelper<ProjectPartListVersionModel>.Update(version);
                LoadVersion();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btlExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //frmProjectPartListDetail frm = new frmProjectPartListDetail();
            //frm.ProjectID = project.ID;
            //frm.projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            //frm.cboVersion.EditValue = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //loadPartListType();
            //    loadData();
            //}


            frmProjectPartlistDetailNew frm = new frmProjectPartlistDetailNew();
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            int versionId = 0;

            if (isFocusVersionGP) versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            else if (isFocusVersionPO) versionId = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            frm.var = new frmProjectPartlistDetailNew.Variable() { ProjectID = project.ID, ProjectSolutionID = projectSolutionId, VersionID = versionId };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.isUpdate) loadData();
                treeListData.ExpandAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            //var nodeHandle = treeListData.FocusedNode;
            //bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetFocusedRowCellValue(colIsApprovedPurchase));
            //bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetFocusedRowCellValue(colIsApprovedTBP));
            //string stt = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colTT));
            //if (treeListData.FocusedNode.HasChildren) return;
            ////if (isApprovedPurchase) //Nếu mua đã duyệt và k phải thuộc phòng mua thì k đc sửa
            ////{
            ////    MessageBox.Show("Vật tư đã được phòng Mua duyệt.\nVui lòng huỷ duyệt trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ////    return;
            ////}
            ////if (isApprovedTBP && Global.DepartmentID != 4 && !Global.IsAdmin)
            ////{
            ////    MessageBox.Show("Vật tư đã được TBP duyệt.\nVui lòng huỷ duyệt trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ////    return;
            ////}

            //if (isApprovedPurchase)
            //{
            //    MessageBox.Show($"Vật tư TT [{stt}] đã được yêu cầu mua.\nVui lòng huỷ yêu cầu mua trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (isApprovedTBP)
            //{
            //    MessageBox.Show($"Vật tư TT [{stt}] đã được TBP duyệt.\nVui lòng huỷ duyệt trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //int id = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
            //if (id == 0) return;
            //ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            //frmProjectPartListDetail frm = new frmProjectPartListDetail();
            //frm.ProjectID = project.ID;
            //frm.model = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //loadPartListType();
            //    loadData();
            //    treeListData.FocusedNode = nodeHandle;
            //}

            var focusNode = treeListData.FocusedNode;
            int focusNodeIndex = treeListData.GetVisibleIndexByNode(focusNode);

            int id = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            int versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            //string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue("GroupMaterial"));
            //if (id <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa lại!", "Thông báo", MessageBoxButtons.OK);
            //    return;
            //}

            ProjectPartListModel partListModel = SQLHelper<ProjectPartListModel>.FindByID(id);
            if (partListModel == null) return;
            frmProjectPartlistDetailNew frm = new frmProjectPartlistDetailNew();
            frm.partList = partListModel;
            //frm.productName = productName;
            frm.var = new frmProjectPartlistDetailNew.Variable()
            {
                ProjectID = project.ID,
                ProjectSolutionID = projectSolutionId,
                VersionID = versionId,
                IsPriceCheck = TextUtils.ToBoolean(treeListData.GetFocusedRowCellValue(colIsCheckPrice))
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.isUpdate) loadData();
                treeListData.ExpandAll();

                //treeListData.FocusedNode = treeListData.Nodes[focusNodeIndex];
                //treeListData.SelectNode(treeListData.Nodes[treeListData.Nodes.Count - 1]);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var list = treeListData.GetAllCheckedNodes();
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (list.Count() == 0) return;

            foreach (var node in list)
            {
                bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
                int isPriceRequest = TextUtils.ToInt(treeListData.GetRowCellValue(node, colStatusPriceRequest));
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));
                if (isApprovedPurchase)
                {
                    MessageBox.Show($"Vật tư TT [{stt}] đã được yêu cầu mua.\nVui lòng huỷ yêu cầu mua trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isApprovedTBP)
                {
                    MessageBox.Show($"Vật tư TT [{stt}] đã được TBP duyệt.\nVui lòng huỷ duyệt trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (isPriceRequest != 0)
                //{
                //    MessageBox.Show($"Vật tư TT [{stt}] đã Y/c báo giá.\nBạn không thể xoá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }

            if (MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
                frm.Text = "XOÁ PHIÊN BẢN PARTLIST";
                frm.label1.Text = "Lý do xoá";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in list)
                    {
                        int id = TextUtils.ToInt(treeListData.GetRowCellValue(item, colID));
                        //ProjectPartListModel partList = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                        ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByID(id);
                        partList.IsDeleted = true;
                        partList.ReasonDeleted = frm.txtReasonCancel.Text.Trim();
                        //ProjectPartListBO.Instance.Update(partList);
                        SQLHelper<ProjectPartListModel>.Update(partList);
                        //EmailSender.setEmail(projectCode, partList, false);
                    }

                    loadData();
                }


            }
        }

        private void treeListData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren) return;
            bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsNewCode));
            if (!isNewCode) return;

            if (e.Column != colGroupMaterial && e.Column != colProductCode && e.Column != colManufacturer && e.Column != colUnit) return;

            //check tên thiết bị
            if (e.Column == colGroupMaterial)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameProductName));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }

            //check mã thiết bị
            if (e.Column == colProductCode)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameProductCode));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }


                //bool isFix = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsFix));
                //if (isFix)
                //{
                //    e.Appearance.BackColor = Color.DeepPink;
                //}
            }

            //check hãng
            if (e.Column == colManufacturer)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameMaker));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }

            //check đơn vị
            if (e.Column == colUnit)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameUnit));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }


        }

        private void treeListData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {

            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                //return;
            }

            bool isDeleted = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsDeleted));
            bool isProblem = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsProblem));
            decimal quantityReturn = TextUtils.ToDecimal(treeListData.GetRowCellValue(e.Node, colQuantityReturn));

            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            else if (isProblem)
            {
                e.Appearance.BackColor = Color.Orange;
            }


            if (quantityReturn > 0)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }

            #region VTN Update 6325
            //bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsNewCode));
            //if (isNewCode)
            //{
            //    e.Appearance.BackColor = Color.Pink;
            //}
            #endregion
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //ExportExcel();
            //return;

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                int partlistVersionID = 0;
                string projectTypeName = "";
                int partListTypeID = 0;
                string versionCode = "";
                string codeSolution = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue("Code"));
                if (isFocusVersionGP)
                {
                    versionCode = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
                    partlistVersionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                    projectTypeName = $"{codeSolution}_GP_{versionCode}_" + TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectTypeName"));
                    partListTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectTypeID"));
                }
                else if (isFocusVersionPO)
                {
                    versionCode = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
                    partlistVersionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                    projectTypeName = $"{codeSolution}_PO_{versionCode}_" + TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("ProjectTypeName"));
                    partListTypeID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ProjectTypeID"));
                }

                //string 

                string filepath = Path.Combine(f.SelectedPath, $"DanhMucVatTuDuAn_{project.ProjectCode}_{projectTypeName}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";


                PrintingSystem printingSystem = new PrintingSystem();

                frmProjectPartList_New_Detail frm = new frmProjectPartList_New_Detail();
                frm.dt = dt;
                frm.project = project;
                frm.partlistVersionID = partlistVersionID;
                frm.partListTypeID = partListTypeID;
                frm.loadData();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = frm.grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    //compositeLink.Links.Add(printableComponentLink2);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                    compositeLink.CreatePageForEachLink();

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                }
            }
        }


        void ExportExcel()
        {
            try
            {
                FolderBrowserDialog f = new FolderBrowserDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string filepath = Path.Combine(f.SelectedPath, $"DanhMucVatTuDuAn_test.xlsx");

                    treeListData.ForceInitialize();

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = treeListData;

                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                    //ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.Status = -1;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
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
                    //ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.Status = 2;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
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
                    //ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.Status = 1;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
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
                    //ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.Status = 0;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
                }
                loadData();
                list.Clear();
            }

        }

        private void comboEdit_EditValueChanged(object sender, EventArgs e)
        {

        }


        void clear()
        {

        }


        private void barEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void barEditItem4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void barEditItem5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboIsDeleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
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
                MessageBox.Show("Vui lòng chọn vật tư!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in listNodes)
            {
                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));

                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));

                if (isDelete)
                {
                    MessageBox.Show($"Không thể {approvedText} vì vật tư thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!isApproved && isApprovedPurchase)
                {
                    MessageBox.Show($"Không thể {approvedText} vì vật tư thứ tự [{stt}] đã được Yêu cầu mua!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (node.HasChildren) continue;

                //PQ.Chien - UPDATE - 08/08/2025
                string errorMessage;
                if (!ValidateProduct(node, out errorMessage, false))
                {
                    MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //END
            }
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn {approvedText} các vật tư đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<FirmModel> firms = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 1);
                foreach (var node in listNodes)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);

                    model.IsApprovedTBP = isApproved; // VTN update 6325'

                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
                }
                loadData();
            }
        }

        void aprrovedPurchase(bool isApproved)
        {
            string approvedText = isApproved ? "Yêu cầu mua" : "Hủy yêu cầu mua";

            var listNodes = treeListData.GetAllCheckedNodes();
            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn vật tư!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isApproved)
            {
                if (!CheckValidate()) return;
            }

            foreach (var node in listNodes)
            {
                if (node.HasChildren) continue;

                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));

                bool isApprovedNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBPNewCode)); //VTN update 6325
                bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode)); //VTN update 6325


                if (isDelete)
                {
                    MessageBox.Show($"Không thể {approvedText} vì vật tư thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isApproved && !isApprovedTBP)
                {
                    MessageBox.Show($"Không thể {approvedText} vì vật tư thứ tự [{stt}] chưa được TBP duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isApproved && !isApprovedNewCode && isNewCode) //VTN update 6325
                {
                    MessageBox.Show($"Không thể {approvedText} vì vật tư thứ tự [{stt}] chưa được TBP duyệt mới!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isApproved && isApprovedPurchase)
                {
                    MessageBox.Show($"Vật tư thứ tự [{stt}] đã được Y/c mua.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }

            //DialogResult result = MessageBox.Show($"Bạn có chắc muốn {approvedText} các vật tư đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            List<ProjectPartListModel> listPartlists = new List<ProjectPartListModel>();
            if (isApproved)
            {
                frmPriceRequestDetail frm = new frmPriceRequestDetail();
                frm.Text = "YÊU CẦU MUA HÀNG";
                frm.lblMessage.Text = "Bạn có chắc muốn yêu cầu mua những sản phẩm đã chọn không?";
                frm.label1.Text = "Deadline hàng về";
                if (frm.ShowDialog() != DialogResult.OK) return;

                int partListTypeId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectTypeID"));
                int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
                int statusVersion = 1;
                var exp1 = new Expression("ProjectTypeID", partListTypeId);
                var exp2 = new Expression("ProjectSolutionID", projectSolutionId);
                var exp3 = new Expression("StatusVersion", statusVersion);

                var version = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
                if (version == null) version = new ProjectPartListVersionModel();

                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartList_Khanh", "A",
                                    new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                    new object[] { project.ID, partListTypeId, 0, txtKeyword.Text.Trim(), -1, -1, version.ID });


                //DataTable dt = (DataTable)treeListData.DataSource;

                List<DataRow> listWarning = new List<DataRow>();//ndnhat update 28/08/2025
                                                                //DataTable dt = (DataTable)treeListData.DataSource;
                DateTime deadlinePur = frm.dtpDeadlinePriceRequest.Value;
                foreach (var node in listNodes)
                {
                    if (node.HasChildren) continue;

                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    string tt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT)).Trim();

                    //ndnhat update 28/08/2025
                    //int deadlineQuote = TextUtils.ToInt(treeListData.GetRowCellValue(node, colTotalDayLeadTimeQuote));
                    //DateTime dtNow = DateTime.Now;
                    //DateTime dateTimeDeadline = dtNow.AddDays(deadlineQuote);

                    //if (deadlinePur < dateTimeDeadline)
                    //{
                    //    DataRow datamail = treeListData.GetDataRow(node.Id) as DataRow;
                    //    if (datamail != null) listWarning.Add(datamail);
                    //}

                    //end ndnhat update 28/08/2025

                    DataRow[] dataQuotes = dt.Select($"TT = '{tt}'");

                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.IsApprovedPurchase = isApproved;
                    model.RequestDate = DateTime.Now;
                    model.ExpectedReturnDate = frm.dtpDeadlinePriceRequest.Value;
                    model.Status = 1;

                    int supplierSaleId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colSupplierSaleQuoteID));
                    string unitMoney = TextUtils.ToString(treeListData.GetRowCellValue(node, colcolUnitMoney));
                    decimal unitPriceQuote = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colUnitPriceQuote));
                    decimal totalPriceQuote = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colTotalPriceQuote));
                    decimal qtyFull = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colQtyFull));
                    string leadTime = TextUtils.ToString(treeListData.GetRowCellValue(node, colLeadTime));

                    model.SupplierSaleID = TextUtils.ToInt(treeListData.GetRowCellValue(node, colSupplierSaleQuoteID));
                    if (supplierSaleId <= 0)
                    {
                        if (dataQuotes.Length > 0) model.SupplierSaleID = TextUtils.ToInt(dataQuotes[0]["SupplierSaleQuoteID"]);
                    }

                    model.UnitMoney = TextUtils.ToString(treeListData.GetRowCellValue(node, colcolUnitMoney));
                    if (string.IsNullOrEmpty(unitMoney))
                    {
                        if (dataQuotes.Length > 0) model.UnitMoney = TextUtils.ToString(dataQuotes[0]["UnitMoney"]);
                    }

                    model.PriceOrder = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colUnitPriceQuote));
                    if (unitPriceQuote <= 0)
                    {
                        if (dataQuotes.Length > 0) model.PriceOrder = TextUtils.ToDecimal(dataQuotes[0]["PriceOrder"]);
                    }

                    model.TotalPriceOrder = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colTotalPriceQuote));
                    if (totalPriceQuote <= 0)
                    {
                        if (dataQuotes.Length > 0) model.TotalPriceOrder = TextUtils.ToDecimal(dataQuotes[0]["TotalPriceOrder"]);
                    }

                    model.QtyFull = TextUtils.ToDecimal(treeListData.GetRowCellValue(node, colQtyFull));
                    if (qtyFull <= 0)
                    {
                        if (dataQuotes.Length > 0) model.QtyFull = TextUtils.ToDecimal(dataQuotes[0]["QtyFull"]);
                    }

                    model.LeadTime = TextUtils.ToString(treeListData.GetRowCellValue(node, colLeadTime));
                    if (string.IsNullOrEmpty(leadTime))
                    {
                        if (dataQuotes.Length > 0) model.LeadTime = TextUtils.ToString(dataQuotes[0]["LeadTime"]);
                    }

                    //ProjectPartListBO.Instance.Update(model);

                    SQLHelper<ProjectPartListModel>.Update(model);

                    //if (node.HasChildren) continue;
                    listPartlists.Add(model);
                }

                //ndnhat update 28/08/2025
                //if (listWarning.Count > 0)
                //{
                //    if (listWarning.Count > 0)
                //    {
                //        // Tạo DataTable hiển thị trên frmWarningLeadtime
                //        DataTable dtmail = new DataTable();
                //        dtmail.Columns.Add("TT", typeof(string));
                //        dtmail.Columns.Add("ProductCode", typeof(string));
                //        dtmail.Columns.Add("GroupMaterial", typeof(string));
                //        dtmail.Columns.Add("QtyFull", typeof(int));
                //        dtmail.Columns.Add("TotalDayLeadtimeQuote", typeof(int));
                //        dtmail.Columns.Add("DeadlinePur", typeof(DateTime));
                //        dtmail.Columns.Add("DatetimeDeadline", typeof(DateTime));

                //        foreach (DataRow row in listWarning)
                //        {
                //            string tt = TextUtils.ToString(row["TT"]);
                //            string productCode = TextUtils.ToString(row["ProductCode"]);
                //            string productName = TextUtils.ToString(row["GroupMaterial"]);
                //            int quantity = TextUtils.ToInt(row["QtyFull"]);
                //            int deadlineQuote = TextUtils.ToInt(row["TotalDayLeadTimeQuote"]);
                //            //DateTime dateTimeDeadline = TextUtils.ToDate5(row["DateTimeDeadline"]);
                //            DateTime dtNow = DateTime.Now;
                //            DateTime dateTimeDeadline = dtNow.AddDays(deadlineQuote);

                //            dtmail.Rows.Add(
                //                tt,
                //                productCode,
                //                productName,
                //                quantity,
                //                deadlineQuote,
                //                deadlinePur.ToString("dd/MM/yyyy"),
                //                dateTimeDeadline.ToString("dd/MM/yyyy")
                //            );
                //        }

                //        frmWarningLeadtime frmWa = new frmWarningLeadtime();
                //        frmWa.DataTable = dtmail;

                //        if (frmWa.ShowDialog() == DialogResult.OK)
                //        {
                //            SendWarningMail(listWarning, deadlinePur);
                //        }
                //    }
                //    //End ndnhat update 28/08/2025
                //}
            }
            else
            {
                var exp2 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.IsDeleted.ToString(), 0);
                var exp3 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.EmployeeIDRequestApproved.ToString(), 0, ">");
                foreach (var node in listNodes)
                {
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    var exp1 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ProjectPartListID.ToString(), id);

                    var purchaseRequest = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(exp1.And(exp2).And(exp3));
                    string tt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT)).Trim();
                    if (purchaseRequest.Count > 0)
                    {
                        MessageBox.Show($"Vật tư thứ tự [{tt}] đang được check đặt hàng. Bạn không thể hủy.\nVui lòng liên hệ nhân viên Pur để hủy!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                foreach (var node in listNodes)
                {
                    //if (node.HasChildren) continue;

                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.IsApprovedPurchase = isApproved;
                    model.RequestDate = null;
                    model.ExpectedReturnDate = null;
                    model.Status = 2;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);
                    //if (node.HasChildren) continue;
                    listPartlists.Add(model);
                }
            }

            UpdatePurchaseRequest(listPartlists);
            loadData();
        }

        //Update yêu cầu mua hàng
        void UpdatePurchaseRequest(List<ProjectPartListModel> listPartlists)
        {
            foreach (ProjectPartListModel item in listPartlists)
            {
                if (item.ID <= 0) continue;
                var exp1 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ProjectPartListID, item.ID);
                var exp2 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.IsDeleted, 1, "<>");
                //ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByAttribute("ProjectPartListID", item.ID).FirstOrDefault();
                List<ProjectPartlistPurchaseRequestModel> requests = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(exp1.And(exp2));

                ProjectPartlistPurchaseRequestModel request = requests.FirstOrDefault();
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
                request.ProjectPartlistPurchaseRequestTypeID = 1;

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

                    if (requests.Count > 0)
                    {

                    }
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                }

            }
        }

        private void btnApprovedTBP_Click(object sender, EventArgs e)
        {
            approvedTBP(true);
        }

        private void btnApprovedBuy_Click(object sender, EventArgs e)
        {
            aprrovedPurchase(true);
        }

        private void btnUnapprovedTBP_Click(object sender, EventArgs e)
        {
            approvedTBP(false);
        }

        private void btnUnAprrovedBuy_Click(object sender, EventArgs e)
        {
            aprrovedPurchase(false);
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

        private void btnIsUnApprovedTBP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            approvedTBP(false);
        }

        private void btnIsUnApprovedPurchase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            aprrovedPurchase(false);
        }

        private void btnIsApprovedTBP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            approvedTBP(true);
        }

        private void btnIsApprovedPurchase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            aprrovedPurchase(true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void cboStatusPur_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void treeListData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }


        private void cboVersion_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
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
            //isFocusVersionGP = true;
            //isFocusVersionPO = false;
            ////Stopwatch stopwatch = new Stopwatch();
            ////stopwatch.Start();
            //btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = false;
            //loadData();


        }
        private void grvDataPO_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ////grvData.FocusedRowHandle = -1;
            //isFocusVersionGP = false;
            //isFocusVersionPO = true;
            ////Stopwatch stopwatch = new Stopwatch();
            ////stopwatch.Start();
            //btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = true;
            //loadData();
            ////stopwatch.Stop();
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

            //if (versionId <= 0)return;
            frmPartListImportExcel frm = new frmPartListImportExcel();

            frm.var = new frmPartListImportExcel.Variable()
            {
                VersionID = versionId,
                VersionCode = versionCode,
                ProjectTypeID = projectTypeID,
                ProjectTypeName = projectTypeName,
                ProjectID = project.ID,
                ProjectCode = project.ProjectCode,
                ProjectSolutionID = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID")),
            };

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

            frmProjectPartListVersion frm = new frmProjectPartListVersion(0);
            frm.statusVersion = 1;
            frm.projectId = project.ID;
            frm.projectSolutionId = projectSolutionId;
            //if (frm.ShowDialog() == DialogResult.OK)
            {
                //LoadVersion();
                frm.SaveEvent += () => LoadVersion();
            }
            frm.Show();
        }

        private void btnUpdateVersion_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }
            ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
            frmProjectPartListVersion frm = new frmProjectPartListVersion(0);
            frm.projectId = project.ID;
            frm.projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            frm.partListVersion = versionModel;
            //if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.SaveEvent += () => LoadVersion();
                //LoadVersion();
                grvData.FocusedRowHandle = rowHandle;
                //loadData();
            }
            frm.Show();
        }

        private void btnRemoveVersion_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }

            bool isActive = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsActive"));
            bool isApproved = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsApproved"));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }

            if (isApproved)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }

            frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
            frm.Text = "XOÁ PHIÊN BẢN PARTLIST";
            frm.label1.Text = "Lý do xoá";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ProjectPartListVersionBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
                //ProjectPartListBO.Instance.DeleteByAttribute("ProjectPartListVersionID", id);

                string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1," +
                            $"ReasonDeleted = N'{frm.txtReasonCancel.Text.Trim()}'," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = '{Global.LoginName}' " +
                            $"WHERE ProjectPartListVersionID = {id}";

                TextUtils.ExcuteSQL(sql);
                loadData();
            }
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
                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    //optionsEx.ExportMode = DevExpress.Export.mod;
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

                bool isApprovedNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBPNewCode)); //VTN update 6325
                bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode)); //VTN update 6325

                if (partListId <= 0) continue;
                if (node.HasChildren) continue;

                if (!isApprovedNewCode && isNewCode)
                {
                    MessageBox.Show($"Vật tư Stt [{stt}] chưa được TBP duyệt mới.\nVui lòng kiểm tra lại!", "Thông báo");
                    return;
                }

                var exp1 = new Expression(ProjectPartlistPriceRequestModel_Enum.ProjectPartListID.ToString(), partListId);
                var exp2 = new Expression(ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(), "1", "<>");

                ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2))
                                                                                                           .OrderByDescending(x => x.StatusRequest).FirstOrDefault();
                requestModel = requestModel ?? new ProjectPartlistPriceRequestModel();

                if (requestModel.StatusRequest > 0)
                {
                    MessageBox.Show($"Vật tư Stt [{stt}] đã được Y/c báo giá.\nVui lòng kiểm tra lại!", "Thông báo");
                    return;
                }
                else
                {
                    //bool isApprovedTBP = 
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
                //priceRequest.Deadline = TextUtils.ToDate4(grvDataSolution.GetFocusedRowCellValue(colPriceReportDeadline));
                priceRequest.Quantity = TextUtils.ToInt(treeListData.GetRowCellValue(node, colQtyFull));

                SQLHelper<ProjectPartlistPriceRequestModel>.Insert(priceRequest);


                //string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
                //string productName = TextUtils.ToString(grvData.GetRowCellValue(row, colProductName));
                string textNotify = $"Mã sản phầm: {priceRequest.ProductCode}\n" +
                                    $"Deadline: {priceRequest.Deadline}";

                //int employee = TextUtils.ToInt(grvData.GetRowCellValue(row, colEmployeeID));
                //TextUtils.AddNotify("YÊU CẦU BÁO GIÁ", textNotify, 0, 4);
            }

            loadData();
        }

        private void stackPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmProjectPartList_New_KeyDown(object sender, KeyEventArgs e)
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
            //if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.SaveEvent += () =>
                {
                    LoadProjectSolution();
                };

                frm.Show();

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
            //if (frm.ShowDialog() == DialogResult.OK)
            {


                frm.SaveEvent += () =>
                {
                    LoadProjectSolution();
                    grvDataSolution.FocusedRowHandle = rowHandle;
                };

                frm.Show();
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
            var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (versions.Count > 0)
            {
                MessageBox.Show($"Danh mục [{projectTypeName}] đã có phiên bản PO!", "Thông báo");
            }
            else
            {
                ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(id);

                ProjectPartListVersionModel newVersion = new ProjectPartListVersionModel();

                newVersion.IsApproved = false;
                newVersion.IsActive = false;
                newVersion.StatusVersion = 2;
                newVersion.ProjectID = versionModel.ProjectID;
                newVersion.STT = versionModel.STT;
                newVersion.Code = versionModel.Code;
                newVersion.DescriptionVersion = versionModel.DescriptionVersion;
                newVersion.ProjectSolutionID = versionModel.ProjectSolutionID;
                newVersion.ProjectTypeID = versionModel.ProjectTypeID;
                newVersion.ApprovedID = versionModel.ApprovedID;

                newVersion.ID = (int)ProjectPartListVersionBO.Instance.Insert(newVersion);

                UpdatePartlist(newVersion.ID, versionModel.ID);

                //TextUtils.ExcuteProcedure("spUpdateProjectPartList", new string[] { "@VersionID", "@NewVersionID" }, new object[] { versionModel.ID, newVersion.ID });

                LoadVersion();
                LoadVersionPO();

                grvDataPO.FocusedRowHandle = grvDataPO.RowCount - 1;


                //Add Notify
                string text = $"Yêu cầu duyệt partlist\n" +
                                $"Dự án: {project.ProjectCode}\n" +
                                $"Danh mục: {projectTypeName}\n" +
                                $"Phiên bản: {newVersion.Code}";

                ProjectTypeModel projectType = SQLHelper<ProjectTypeModel>.FindByID(TextUtils.ToInt(newVersion.ProjectTypeID));
                TextUtils.AddNotify("DUYỆT PARTLIST", text, TextUtils.ToInt(projectType.ApprovedTBPID));
            }

        }

        void UpdatePartlist(int newVersionId, int oldVersionId)
        {
            try
            {

                List<ProjectPartListDTO> listPartlists = SQLHelper<ProjectPartListDTO>.ProcedureToList("spGetProjectPartList_Khanh",
                                    new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                    new object[] { project.ID, 0, -1, "", -1, -1, oldVersionId });

                Regex regex = new Regex(@"^-?[\d\.]+$");
                foreach (ProjectPartListDTO item in listPartlists)
                {
                    string stt = item.TT;

                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;

                    ProjectPartListModel partList = new ProjectPartListModel();
                    partList.ProjectID = item.ProjectID;
                    partList.TT = stt;

                    partList.ParentID = GetParentId(stt, newVersionId, false);


                    partList.ProjectTypeID = item.ProjectTypeID;
                    partList.ProjectPartListVersionID = newVersionId;

                    partList.GroupMaterial = item.GroupMaterial;
                    partList.ProductCode = item.ProductCode;
                    partList.OrderCode = item.OrderCode;
                    partList.Manufacturer = item.Manufacturer;
                    partList.Model = item.Model;
                    partList.QtyMin = item.QtyMin;
                    partList.QtyFull = item.QtyFull;
                    partList.Unit = item.Unit;
                    partList.Price = item.Price;//<= 0 ? item.UnitPriceQuote : item.Price;
                    partList.Amount = item.Amount;// partList.Price * partList.QtyFull;
                    partList.LeadTime = item.LeadTime; // tien do
                    partList.NCC = item.NCC;
                    partList.RequestDate = item.RequestDate;
                    partList.LeadTimeRequest = item.LeadTimeRequest;
                    partList.QuantityReturn = item.QuantityReturn;
                    partList.NCCFinal = item.NCCFinal;
                    partList.PriceOrder = item.PriceOrder;
                    partList.OrderDate = item.OrderDate;
                    partList.ExpectedReturnDate = item.ExpectedReturnDate;
                    partList.Status = item.Status;
                    partList.Quality = item.Quality;
                    partList.Note = item.Note;
                    partList.ReasonProblem = item.ReasonProblem;
                    partList.IsProblem = item.IsProblem;
                    partList.IsDeleted = item.IsDeleted;

                    partList.SpecialCode = item.SpecialCode;
                    if (partList.ID > 0)
                    {
                        SQLHelper<ProjectPartListModel>.Update(partList);
                    }
                    else
                    {
                        SQLHelper<ProjectPartListModel>.Insert(partList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int GetParentId(string tt, int versionId, bool isProblem)
        {
            int parentId = 0;
            if (!tt.Contains(".")) return parentId;

            string parentTt = tt.Substring(0, tt.LastIndexOf(".")).Trim();
            int isProblemValue = isProblem ? 1 : 0;

            var exp1 = new Expression("TT", parentTt);
            var exp2 = new Expression("ProjectPartListVersionID", versionId);
            var exp3 = new Expression("IsDeleted", 1, "<>");
            var exp4 = new Expression("IsProblem", isProblemValue);
            ProjectPartListModel parent = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault();
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

            frmProjectPartListVersion frm = new frmProjectPartListVersion(0);
            frm.statusVersion = 2;
            frm.projectId = project.ID;
            frm.projectSolutionId = projectSolutionId;
            //if (frm.ShowDialog() == DialogResult.OK)
            {
                //LoadVersionPO();
                frm.SaveEvent += () => LoadVersionPO();
            }
            frm.Show();
        }

        private void btnEditVersionPO_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataPO.FocusedRowHandle;
            int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }
            ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
            frmProjectPartListVersion frm = new frmProjectPartListVersion(0);
            frm.projectId = project.ID;
            frm.projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            frm.partListVersion = versionModel;
            //if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.SaveEvent += () => LoadVersionPO();
                //LoadVersionPO();
                grvDataPO.FocusedRowHandle = rowHandle;
            }
            frm.Show();
        }

        private void btnDeleteVersionPO_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue(colIDVersion));
            if (id <= 0)
            {
                return;
            }

            bool isActive = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsActive"));
            bool isApproved = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsApproved"));
            string code = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colCode));
            if (isActive)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }

            if (isApproved)
            {
                MessageBox.Show($"Phiên bản [{code}] đang được sử dụng.\nBạn không thể xoá!", "Thông báo");
                return;
            }

            frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
            frm.Text = "XOÁ PHIÊN BẢN PARTLIST";
            frm.label1.Text = "Lý do xoá";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ProjectPartListVersionBO.Instance.Delete(id);
                var myDictVersion = new Dictionary<string, object>()
                {
                    {ProjectPartListVersionModel_Enum.IsDeleted.ToString(),true },
                    {ProjectPartListVersionModel_Enum.ReasonDeleted.ToString(),frm.txtReasonCancel.Text.Trim() },
                    {ProjectPartListVersionModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                    {ProjectPartListVersionModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                };
                SQLHelper<ProjectPartListModel>.UpdateFieldsByID(myDictVersion, id);

                grvDataPO.DeleteSelectedRows();

                //string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1," +
                //            $"ReasonDeleted = N'{frm.txtReasonCancel.Text.Trim()}'," +
                //            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                //            $"UpdatedBy = '{Global.LoginName}' " +
                //            $"WHERE ProjectPartListVersionID = {id}";

                //TextUtils.ExcuteSQL(sql);


                var myDict = new Dictionary<string, object>()
                {
                    { ProjectPartListModel_Enum.IsDeleted.ToString(),true},
                    { ProjectPartListModel_Enum.ReasonDeleted.ToString(),frm.txtReasonCancel.Text.Trim()},
                    { ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                SQLHelper<ProjectPartListModel>.UpdateFields(myDict, new Expression(ProjectPartListModel_Enum.ProjectPartListVersionID, id));
                loadData();
            }
        }

        private void btnRefreshVersionPO_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataPO.FocusedRowHandle;
            LoadVersionPO();
            grvDataPO.FocusedRowHandle = rowHandle;
        }

        void ApprovedVersionPO(bool isApproved)
        {
            int rowHandle = grvDataPO.FocusedRowHandle;
            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            int id = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue(colCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} của phiên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectPartListVersionModel version = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
                if (version == null) return;
                version.IsApproved = isApproved;
                version.ApprovedID = Global.EmployeeID;

                SQLHelper<ProjectPartListVersionModel>.Update(version);
                LoadVersionPO();
                grvDataPO.FocusedRowHandle = rowHandle;
            }
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

            //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartListByID", new string[] { "@ID" }, new object[] { idText });
            //DataTable dt = dataSet.Tables[1];
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListByID_RequestExport", "A", new string[] { "@ID" }, new object[] { idText });

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



                int id = TextUtils.ToInt(dataRow["ID"]);
                TreeListNode nodeSelected = treeListData.FindNodeByFieldValue("ID", id);
                int warehouseID = 0;
                int warehouse1 = TextUtils.ToInt(nodeSelected["Warehouse1"]);
                int warehouse2 = TextUtils.ToInt(nodeSelected["Warehouse2"]);
                int warehouse3 = TextUtils.ToInt(nodeSelected["Warehouse3"]);
                int warehouse4 = TextUtils.ToInt(nodeSelected["Warehouse4"]);

                if (warehouse1 > 0) warehouseID = warehouse1;
                else if (warehouse2 > 0) warehouseID = warehouse2;
                else if (warehouse3 > 0) warehouseID = warehouse3;
                else if (warehouse4 > 0) warehouseID = warehouse4;

                DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                    new string[] { "@WarehouseID", "@ProductGroupID" },
                                                    new object[] { warehouseID, productGroupID });

                bill.Status = 6;
                //bill.SenderID = 0;
                bill.SenderID = dtGroupWarehouse.Rows.Count > 0 ? TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]) : 0;
                bill.UserID = Global.UserID;
                bill.WarehouseType = SQLHelper<ProductGroupModel>.FindByID(productGroupID).ProductGroupName;
                bill.KhoTypeID = productGroupID;
                bill.GroupID = TextUtils.ToString(productGroupID);
                bill.WarehouseID = warehouseID;
                bill.RequestDate = DateTime.Now;
                bill.CustomerID = TextUtils.ToInt(dataRow["CustomerID"]);
                bill.Address = TextUtils.ToString(dataRow["Address"]);

                int billExportID = SQLHelper<BillExportModel>.Insert(bill).ID;

                for (int i = 0; i < dtDetails.Length; i++)
                {
                    var dataRowDetail = dtDetails[i];
                    TreeListNode node = treeListData.FindNodeByFieldValue("ID", dataRowDetail["ID"]);
                    decimal remainQuantity = TextUtils.ToDecimal(node["RemainQuantity"]);
                    //decimal qty = TextUtils.ToDecimal(node["Qty"]);
                    if (remainQuantity <= 0) continue;

                    BillExportDetailModel detail = new BillExportDetailModel();
                    detail.BillID = billExportID;
                    detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                    detail.Qty = remainQuantity;
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

                //frmBillExportDetail frm = new frmBillExportDetail();
                //frm.WarehouseCode = "HN";
                //frm.billExport = bill;
                //frm.IDDetail = billExportID;

                //frm.Show();


                //Add Notify
                string text = $"Mã phiếu xuất: {bill.Code}\n" +
                            $"Người yêu cầu: {Global.AppFullName}";
                int employeeID = dtGroupWarehouse.Rows.Count > 0 ? TextUtils.ToInt(dtGroupWarehouse.Rows[0]["EmployeeID"]) : 0;
                TextUtils.AddNotify("YÊU CẦU XUẤT KHO", text, employeeID);
            }


            MessageBox.Show("Yêu cầu xuất kho thành công!", "Thông báo");
            loadData();
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

            //if (billExport.ID > 0 && !Global.IsAdmin) return;
            //int billtype = TextUtils.ToInt(cboStatusNew.EditValue);
            string code = TextUtils.GetBillCode("BillExport", 6);
            return code;

            //DateTime dateNow = DateTime.Now;
            //string year = dateNow.ToString("yy");
            //string month = dateNow.ToString("MM");
            //string day = dateNow.ToString("dd");

            //string code = $"PXK{year}{month}{day}";
            ////Get bill code mới nhất
            //var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            //var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            //var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            //BillExportModel bill = SQLHelper<BillExportModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();

            //string currentCode = bill == null ? "" : bill.Code.Trim();
            //int stt = string.IsNullOrEmpty(currentCode) ? 1 : TextUtils.ToInt(currentCode.Substring(currentCode.Length - 3)) + 1;
            //string sttText = stt.ToString();
            //while (sttText.Length < 3)
            //{
            //    sttText = "0" + sttText;
            //}
            //code = code + sttText;
            //return code;
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


        void RequestExport(ToolStripMenuItem menuItem, bool isTranfer)
        {

            try
            {
                if (menuItem == null) return;
                string warehouseCode = TextUtils.ToString(menuItem.Tag);
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", warehouseCode).FirstOrDefault() ?? new WarehouseModel();
                if (warehouse.ID <= 0) return;

                var selectedNodes = treeListData.GetAllCheckedNodes();
                if (selectedNodes.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu xuất kho!", "Thông báo");
                    return;
                }

                //string productNewCode = "";
                List<string> productNewCodes = new List<string>();
                foreach (TreeListNode node in selectedNodes)
                {
                    if (!ValidateKeep(node, warehouse.ID, out string productNewCode))
                    {
                        if (productNewCodes.Contains(productNewCode) || string.IsNullOrWhiteSpace(productNewCode)) continue;
                        productNewCodes.Add(productNewCode);
                    }
                }


                //return;

                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu xuất kho danh sách vật tư đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;

                List<int> ids = new List<int>();
                foreach (TreeListNode node in selectedNodes)
                {
                    if (!ValidateKeep(node, warehouse.ID, out string productNewCode)) continue;

                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    if (id <= 0) continue;
                    ids.Add(id);
                }
                if (ids.Count <= 0) return;

                if (productNewCodes.Count > 0)
                {
                    MessageBox.Show($"Các sản phẩm có mã nội bộ [{string.Join(";", productNewCodes)}] sẽ không được yêu cầu xuất kho vì không đủ số lượng!", "Thông báo");
                }

                string idText = string.Join(",", ids);

                //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartListByID", new string[] { "@ID" }, new object[] { idText });
                //DataTable dt = dataSet.Tables[1];
                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListByID_RequestExport", "spGetProjectPartListByID_RequestExport", new string[] { "@ID" }, new object[] { idText });

                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, new string[] { "ProductGroupID" });


                //foreach (DataRow row in distinctValues.Rows)
                for (int j = 0; j < distinctValues.Rows.Count; j++)
                {
                    DataRow row = distinctValues.Rows[j];
                    int productGroupID = TextUtils.ToInt(row["ProductGroupID"]);
                    DataRow[] dtDetails = dt.Select($"ProductGroupID = {productGroupID}");
                    if (dtDetails.Length <= 0) continue;
                    var dataRow = dtDetails[0];

                    BillExportModel bill = new BillExportModel();
                    bill.Code = GetBillExportCode();
                    //bill.WarehouseID = 1;
                    //bill.RequestDate = DateTime.Now;


                    int id = TextUtils.ToInt(dataRow["ID"]);
                    TreeListNode nodeSelected = treeListData.FindNodeByFieldValue("ID", id);
                    //int warehouseID = 0;
                    //int warehouse1 = TextUtils.ToInt(nodeSelected["Warehouse1"]);
                    //int warehouse2 = TextUtils.ToInt(nodeSelected["Warehouse2"]);
                    //int warehouse3 = TextUtils.ToInt(nodeSelected["Warehouse3"]);
                    //int warehouse4 = TextUtils.ToInt(nodeSelected["Warehouse4"]);

                    //if (warehouse1 > 0) warehouseID = warehouse1;
                    //else if (warehouse2 > 0) warehouseID = warehouse2;
                    //else if (warehouse3 > 0) warehouseID = warehouse3;
                    //else if (warehouse4 > 0) warehouseID = warehouse4;

                    DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "spGetProductGroupWarehouse",
                                                        new string[] { "@WarehouseID", "@ProductGroupID" },
                                                        new object[] { warehouse.ID, productGroupID });

                    bill.Status = 6;
                    //bill.SenderID = 0;
                    bill.SenderID = dtGroupWarehouse.Rows.Count > 0 ? TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]) : 0;
                    bill.UserID = Global.UserID;
                    bill.WarehouseType = SQLHelper<ProductGroupModel>.FindByID(productGroupID).ProductGroupName;
                    bill.KhoTypeID = productGroupID;
                    bill.GroupID = TextUtils.ToString(productGroupID);
                    bill.WarehouseID = warehouse.ID;
                    bill.RequestDate = DateTime.Now;
                    bill.CustomerID = TextUtils.ToInt(dataRow["CustomerID"]);
                    bill.Address = TextUtils.ToString(dataRow["Address"]);

                    //int billExportID = SQLHelper<BillExportModel>.Insert(bill).ID;
                    DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "spGetBillExportDetail", new string[] { "@BillID" }, new object[] { 0 });
                    for (int i = 0; i < dtDetails.Length; i++)
                    {
                        var dataRowDetail = dtDetails[i];
                        TreeListNode node = treeListData.FindNodeByFieldValue("ID", dataRowDetail["ID"]);
                        //decimal remainQuantity = TextUtils.ToDecimal(node["RemainQuantity"]);
                        ////decimal qty = TextUtils.ToDecimal(node["Qty"]);
                        //if (remainQuantity <= 0) continue;


                        decimal remainQuantity = TextUtils.ToDecimal(node["RemainQuantity"]);
                        decimal quantityReturn = TextUtils.ToDecimal(node["QuantityReturn"]);
                        decimal qtyFull = TextUtils.ToDecimal(node["QtyFull"]);

                        // Nếu đã xuất hết rồi thì bỏ qua
                        if (remainQuantity <= 0)
                            continue;

                        // Nếu số lượng trả <= 0 thì bỏ qua luôn
                        if (quantityReturn <= 0)
                            continue;

                        // Tính số lượng cần xuất
                        decimal qtyToExport = (quantityReturn >= qtyFull)
                            ? remainQuantity
                            : Math.Min(remainQuantity, quantityReturn);

                        DataRow rowDetal = dtDetail.NewRow();
                        //BillExportDetailModel detail = new BillExportDetailModel();
                        //detail.BillID = billExportID;
                        rowDetal["ProductID"] = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                        rowDetal["Qty"] = qtyToExport;
                        rowDetal["ProductFullName"] = TextUtils.ToString(dataRowDetail["GroupMaterial"]);
                        rowDetal["ProjectName"] = TextUtils.ToString(dataRowDetail["ProjectName"]);
                        rowDetal["ProjectID"] = TextUtils.ToInt(dataRowDetail["ProjectID"]);
                        rowDetal["Note"] = TextUtils.ToString(dataRowDetail["Note"]);
                        rowDetal["TotalQty"] = TextUtils.ToInt(dataRowDetail["QtyFull"]);
                        rowDetal["SerialNumber"] = "";
                        rowDetal["ProjectPartListID"] = TextUtils.ToInt(dataRowDetail["ID"]);
                        rowDetal["STT"] = i + 1;

                        rowDetal["ProductCode"] = TextUtils.ToString(dataRowDetail["ProductCode"]);
                        rowDetal["ProductNewCode"] = TextUtils.ToString(node.GetValue(colProductNewCode.FieldName));
                        rowDetal["ProductName"] = TextUtils.ToString(node.GetValue(colGroupMaterial.FieldName));
                        rowDetal["Unit"] = TextUtils.ToString(node.GetValue(colUnit.FieldName));
                        rowDetal["ProjectCodeText"] = TextUtils.ToString(node.GetValue("ProjectCode"));
                        rowDetal["ProjectCodeExport"] = TextUtils.ToString(node.GetValue("ProjectCode"));
                        rowDetal["ChildID"] = i + 1;
                        rowDetal["ParentID"] = 0;

                        dtDetail.Rows.Add(rowDetal);

                        //SQLHelper<BillExportDetailModel>.Insert(detail);
                    }

                    if (dtDetail.Rows.Count <= 0) continue;
                    frmBillExportDetailNew frm = new frmBillExportDetailNew(isTranfer);
                    frm.billExport = bill;
                    frm.dtDetail = dtDetail;
                    //frm.WarehouseCode = TextUtils.ToString(this.Tag);
                    frm.WarehouseCode = warehouse.WarehouseCode;
                    frm.isPOKH = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (j == distinctValues.Rows.Count - 1)
                        {
                            //loadData();
                            selectedNodes.Clear();
                        }
                    }

                    //frmBillExportDetailNew frm = new frmBillExportDetailNew();
                    //frm.WarehouseCode = "HN";
                    //frm.billExport = bill;
                    ////frm.IDDetail = billExportID;

                    //frm.Show();


                    //Add Notify
                    string text = $"Mã phiếu xuất: {bill.Code}\n" +
                                $"Người yêu cầu: {Global.AppFullName}";
                    int employeeID = dtGroupWarehouse.Rows.Count > 0 ? TextUtils.ToInt(dtGroupWarehouse.Rows[0]["EmployeeID"]) : 0;
                    TextUtils.AddNotify("YÊU CẦU XUẤT KHO", text, employeeID);
                }


                //MessageBox.Show("Yêu cầu xuất kho thành công!", "Thông báo");
                //loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string[] unitNames = new string[] { "m", "mét" };
        /// <summary>
        /// Check validate hàng giữ
        /// </summary>
        bool ValidateKeep(TreeListNode node, int warehouseID, out string productNewCode)
        {
            productNewCode = "";
            if (node == null) return false;
            string unitName = TextUtils.ToString(node.GetValue("Unit"));
            if (unitNames.Contains(unitName.Trim().ToLower())) return true;

            //int billExportDetailID = TextUtils.ToInt(node.GetValue("ID"));
            int billExportDetailID = 0;
            int productID = TextUtils.ToInt(node.GetValue("ProductID"));
            int projectID = TextUtils.ToInt(node.GetValue("ProjectID"));
            //int pokhDetailID = TextUtils.ToInt(node.GetValue("POKHDetailIDActual"));
            int pokhDetailID = 0;
            //decimal totalQty = TextUtils.ToInt(node.GetValue("RemainQuantity"));

            decimal remainQuantity = TextUtils.ToDecimal(node["RemainQuantity"]);
            decimal quantityReturn = TextUtils.ToDecimal(node["QuantityReturn"]);
            decimal qtyFull = TextUtils.ToDecimal(node["QtyFull"]);

            // Nếu đã xuất hết rồi thì bỏ qua
            if (remainQuantity <= 0)
                return false;

            // Nếu số lượng trả <= 0 thì bỏ qua luôn
            if (quantityReturn <= 0)
                return false;

            // Tính số lượng cần xuất
            decimal totalQty = (quantityReturn >= qtyFull)
                ? remainQuantity
                : Math.Min(remainQuantity, quantityReturn);


            string productCode = TextUtils.ToString(node.GetValue("ProductNewCode"));
            string projectCode = TextUtils.ToString(node.GetValue("ProjectCode"));
            string poNumber = TextUtils.ToString(node.GetValue("PONumber"));

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetInventoryProjectImportExport",
                                    new string[] { "@WarehouseID", "@ProductID", "@ProjectID", "@POKHDetailID", "@BillExportDetailID" },
                                    new object[] { warehouseID, productID, projectID, pokhDetailID, billExportDetailID });

            var inventoryProjects = dataSet.Tables[0];

            var dtImport = dataSet.Tables[1];
            var dtExport = dataSet.Tables[2];
            var dtStock = dataSet.Tables[3];

            decimal totalQuantityKeep = inventoryProjects.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(inventoryProjects.Rows[0]["TotalQuantity"]); ; //Sl giữ
            totalQuantityKeep = Math.Max(totalQuantityKeep, 0);
            decimal totalQuantityLast = dtStock.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtStock.Rows[0]["TotalQuantityLast"]); ; //SL tồn CK
            totalQuantityLast = Math.Max(totalQuantityLast, 0);

            decimal totalImport = dtImport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtImport.Rows[0]["TotalImport"]);
            decimal totalExport = dtExport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtExport.Rows[0]["TotalExport"]);

            decimal quantityRemain = totalImport - totalExport;
            quantityRemain = Math.Max(quantityRemain, 0);


            decimal totalStock = totalQuantityKeep + quantityRemain + totalQuantityLast;
            if (totalQty > totalStock)
            {
                //MessageBox.Show($"Số lượng còn lại của sản phẩm [{productCode}] không đủ!\n" +
                //    $"SL xuất: {totalQty}\n" +
                //    $"SL giữ: {totalQuantityKeep} | Tồn CK: {totalQuantityLast} | Tổng: {totalStock}", "Thông báo");

                productNewCode = productCode;
                return false;
            }


            return true;
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
                if (Global.EmployeeID != employeeId && !Global.IsAdmin) continue;
                if (node.HasChildren) continue;

                var exp1 = new Expression("ProjectPartListID", partListId);
                var exp2 = new Expression("EmployeeID", employeeId);
                //ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                List<ProjectPartlistPriceRequestModel> priceRequests = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2));
                //foreach (ProjectPartlistPriceRequestModel priceRequest in priceRequests)
                //{
                //    if (priceRequest != null)
                //    {
                //        priceRequest.IsDeleted = true;
                //        priceRequest.StatusRequest = 0;

                //        SQLHelper<ProjectPartlistPriceRequestModel>.Update(priceRequest);
                //    }
                //}
                if (priceRequests.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(),true },
                        {ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(),0 },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    var listIDs = priceRequests.Select(x => x.ID).ToList();
                    var exp = new Expression(ProjectPartlistPriceRequestModel_Enum.ID, string.Join(",", listIDs), "IN");
                    SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);
                }

                ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByID(partListId);
                if (partList == null || partList.ID <= 0) continue;
                partList.StatusPriceRequest = 0;
                partList.DatePriceRequest = null;
                partList.DeadlinePriceRequest = null;
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
            //grvDataPO.FocusedRowHandle = -1;
            isFocusVersionGP = true;
            isFocusVersionPO = false;
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();


            btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = false;
            colIsApprovedTBPText.Visible = false;

            btnTechBought.Visible = btnUnTechBought.Visible = false;

            // PQ.Chien - UPDATE - 23/07/2025
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                treeListBand5.Caption = "";
            }

            // Process the first selected row
            int selectedRow = selectedRows[0];
            string code = TextUtils.ToString(grvData.GetRowCellValue(selectedRow, colCode));
            string projectTypeName = TextUtils.ToString(grvData.GetRowCellValue(selectedRow, colProjectTypeName));

            // Update tree list caption
            treeListBand5.Caption = $"Vật tư dự án - {project.ProjectCode} - Giải pháp - {projectTypeName} - {code}".ToUpper();
            treeListBand5.AppearanceHeader.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            treeListBand5.AppearanceHeader.ForeColor = Color.Black;
            //END

            loadData();
        }

        private void grvDataPO_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            isFocusVersionGP = false;
            isFocusVersionPO = true;
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            btnApprovedTBP.Visible = btnUnapprovedTBP.Visible = btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = true;
            colIsApprovedTBPText.Visible = true;

            btnTechBought.Visible = btnUnTechBought.Visible = true;
            //if (isTBP) //LinhTN update 17/08/2024
            //{

            //}

            btnApprovedBuy.Visible = btnUnAprrovedBuy.Visible = !isTBP;

            // PQ.Chien - UPDATE - 23/07/2025
            var selectedRows = grvDataPO.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                treeListBand5.Caption = "";
            }

            // Process the first selected row
            int selectedRow = selectedRows[0];
            string code = TextUtils.ToString(grvDataPO.GetRowCellValue(selectedRow, gridColumn6));
            string projectTypeName = TextUtils.ToString(grvDataPO.GetRowCellValue(selectedRow, gridColumn12));

            // Update tree list caption
            treeListBand5.Caption = $"Vật tư dự án - {project.ProjectCode} - PO - {projectTypeName} - {code}".ToUpper();
            treeListBand5.AppearanceHeader.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            treeListBand5.AppearanceHeader.ForeColor = Color.Black;
            //END

            loadData();
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
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT)).Trim();

                string Manufacturer = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer)).Trim();
                string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit)).Trim();
                string ProductCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode)).Trim();
                string GroupMaterial = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial)).Trim();

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
                    MessageBox.Show($"[Mã thiết bị] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(Manufacturer))
                {
                    MessageBox.Show($"[Hãng SX] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(unit))
                {
                    MessageBox.Show($"[Đơn vị] có số thứ tự [{stt}] không được trống!\nVui lòng kiểm tra lại!", TextUtils.Caption);
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
            //treeListData.CheckAll();

            foreach (var item in treeListData.GetNodeList())
            {
                item.Checked = true;
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            //treeListData.UncheckAll();
            foreach (var item in treeListData.GetNodeList())
            {
                item.Checked = false;
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {

            string solutionCode = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution)).Trim();

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
                        int projectId = TextUtils.ToInt(treeListData.GetRowCellValue(node, colProjectID));

                        ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                        if (project == null) continue;
                        if (!project.CreatedDate.HasValue) continue;

                        string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solutionCode}/2D/GC/DH";

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

        private void btnQuotePriceHistory_Click(object sender, EventArgs e)
        {
            try
            {
                int ProjectPartListID = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
                string ProductCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode));
                if (ProjectPartListID <= 0) return;
                var lsPRequet = SQLHelper<ProjectPartlistPriceRequestModel>.FindByAttribute("ProjectPartListID", ProjectPartListID);
                if (lsPRequet != null && lsPRequet.Count > 0)
                {
                    var lsModel = lsPRequet[0];
                    if (lsModel.ID > 0)
                    {
                        string stt = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colTT));
                        string unitCount = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colUnit));

                        frmQuotePriceHistory frm = new frmQuotePriceHistory();
                        frm.modelPPLPR = lsModel;
                        frm.tt = stt;
                        frm.unitCount = unitCount;
                        foreach (GridColumn column in frm.grvData.Columns)
                        {
                            column.OptionsColumn.AllowEdit = false;
                            column.OptionsColumn.ReadOnly = true;
                        }
                        frm.grvData.Columns["Note"].OptionsColumn.AllowEdit = true;
                        frm.grvData.Columns["Note"].OptionsColumn.ReadOnly = false;

                        frm.grvData.Columns["NoSTT"].Visible = false;
                        //frm.grvData.Columns[1].Visible = false;

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            loadData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Vật tư có mã thiết bị [{ProductCode}] chưa yêu cầu báo giá!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnProjectMachinePrice_Click(object sender, EventArgs e)
        {
            // ct1 = "Chi tiết tiêu chuẩn"
            // ct2 = "Chi tiết gia công"
            // ct3 = "Phụ kiện"
            decimal TotalPriceQuote1, TotalPriceQuote2, TotalPriceQuote3;
            bool checkNode = false;
            int type = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectTypeID));
            int typePO = TextUtils.ToInt(grvDataPO.GetRowCellValue(grvDataPO.FocusedRowHandle, colProjectTypeID));
            int ID1 = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colIDVersion));
            int ID2 = TextUtils.ToInt(grvDataPO.GetRowCellValue(grvDataPO.FocusedRowHandle, gridColumn8));

            AmountSpent1 = AmountSpent2 = AmountSpent3 = TotalPriceQuote1 = TotalPriceQuote2 = TotalPriceQuote3 = 0;
            if ((rowNumber == 0 || rowNumber == 2) && (type != 2 && typePO != 2))
            {
                MessageBox.Show("Vui lòng chọn cụm Cơ khí!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (rowNumber == 1 && (type != 8 && typePO != 8))
            {
                MessageBox.Show("Vui lòng chọn cụm Điện!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            List<TreeListNode> checkedNodes = new List<TreeListNode>();
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeListData.Nodes)
            {
                if (node.Checked) checkedNodes.Add(node);

                if (checkedNodes.Count > 1)
                {
                    MessageBox.Show("Vui lòng chọn một cụm duy nhất!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (checkedNodes.Count == 0) NameNode = "Tổng vật tư dự án";

            }


            if (treeListData.DataSource != null)
            {
                if (dt.Rows.Count > 0)
                {

                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in treeListData.Nodes)
                    {
                        if (node.Checked)
                        {
                            NameNode = TextUtils.ToString(node.GetValue(colGroupMaterial)).Trim();
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
                            {
                                string checkcontent = TextUtils.ToString(childNode.GetValue(colGroupMaterial)).Trim().ToLower();

                                if ((checkcontent == ct1 || checkcontent == ct3) && (rowNumber == 0 || rowNumber == 2) && (type == 2 || typePO == 2))
                                {
                                    if (ID1 != 0) PLV1 = ID1;
                                    if (ID2 != 0) PLV1 = ID2;
                                    TotalPriceQuote1 += TextUtils.ToDecimal(childNode.GetValue(colTotalPriceQuote));
                                    AmountSpent1 += TextUtils.ToDecimal(childNode.GetValue(colAmount));
                                }
                                else if (checkcontent == ct2 && (rowNumber == 0 || rowNumber == 2) && (type == 2 || typePO == 2))
                                {
                                    TotalPriceQuote3 += TextUtils.ToDecimal(childNode.GetValue(colTotalPriceQuote));
                                    AmountSpent3 += TextUtils.ToDecimal(childNode.GetValue(colAmount));

                                }
                                else if ((type == 8 || typePO == 8) && (rowNumber == 1))
                                {
                                    if (ID1 != 0) PLV2 = ID1;
                                    if (ID2 != 0) PLV2 = ID2;
                                    TotalPriceQuote2 = TextUtils.ToDecimal(treeListData.GetSummaryValue(treeListData.Columns[$"{colTotalPriceQuote.FieldName}"]));
                                    AmountSpent2 = TextUtils.ToDecimal(treeListData.GetSummaryValue(treeListData.Columns[$"{colAmount.FieldName}"]));
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            checkNode = true;
                        }
                    }

                    if (checkNode == false)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            string checkcontent = TextUtils.ToString(r[$"{colGroupMaterial.FieldName}"]).Trim().ToLower();

                            if ((checkcontent == ct1 || checkcontent == ct3) && (rowNumber == 0 || rowNumber == 2) && (type == 2 || typePO == 2))
                            {
                                if (ID1 != 0) PLV1 = ID1;
                                if (ID2 != 0) PLV1 = ID2;
                                TotalPriceQuote1 += TextUtils.ToDecimal(r[$"{colTotalPriceQuote.FieldName}"]);
                                AmountSpent1 += TextUtils.ToDecimal(r[$"{colAmount.FieldName}"]);

                            }
                            else if (checkcontent == ct2 && (rowNumber == 0 || rowNumber == 2) && (type == 2 || typePO == 2))
                            {
                                TotalPriceQuote3 += TextUtils.ToDecimal(r[$"{colTotalPriceQuote.FieldName}"]);
                                AmountSpent3 += TextUtils.ToDecimal(r[$"{colAmount.FieldName}"]);

                            }
                            else if ((type == 8 || typePO == 8) && (rowNumber == 1))
                            {
                                if (ID1 != 0) PLV2 = ID1;
                                if (ID2 != 0) PLV2 = ID2;
                                TotalPriceQuote2 = TextUtils.ToDecimal(treeListData.GetSummaryValue(treeListData.Columns[$"{colTotalPriceQuote.FieldName}"]));
                                AmountSpent2 = TextUtils.ToDecimal(treeListData.GetSummaryValue(treeListData.Columns[$"{colAmount.FieldName}"]));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn cụm Cơ khí hoặc Điện!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

            }
            if (TotalPriceQuote1 != 0) AmountSpent1 = TotalPriceQuote1;
            if (TotalPriceQuote2 != 0) AmountSpent2 = TotalPriceQuote2;
            if (TotalPriceQuote3 != 0) AmountSpent3 = TotalPriceQuote3;
            frmProjectPartList_New_FormClosed(null, null);
        }

        private void frmProjectPartList_New_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnRequestExportHN_Click(object sender, EventArgs e)
        {
            RequestExport((ToolStripMenuItem)sender, false);
        }

        private void btnRequestExportHCM_Click(object sender, EventArgs e)
        {
            RequestExport((ToolStripMenuItem)sender, false);
        }

        private void btnRequestExportBN_Click(object sender, EventArgs e)
        {
            RequestExport((ToolStripMenuItem)sender, false);
        }

        private void btnRequestExportHP_Click(object sender, EventArgs e)
        {
            RequestExport((ToolStripMenuItem)sender, false);
        }

        public int Compare(object x, object y)
        {
            // Kiểm tra nếu là giá trị null
            //if (x == null || y == null)
            //    return 0;

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

        private void treeListData_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
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
                    e.Result = Compare(e.NodeValue1, e.NodeValue2);
                    //e.Handled = true; // Đánh dấu rằng bạn đã xử lý sự kiện này
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", $"Thông báo");
            }
        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int partlistVersionID = 0;
            string projectTypeName = "";
            int partListTypeID = 0;
            if (isFocusVersionGP)
            {
                partlistVersionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                projectTypeName = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectTypeName"));
                partListTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectTypeID"));
            }
            else if (isFocusVersionPO)
            {
                partlistVersionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));
                projectTypeName = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("ProjectTypeName"));
                partListTypeID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ProjectTypeID"));
            }

            frmProjectPartList_New_Detail frm = new frmProjectPartList_New_Detail();
            frm.dt = dt;
            frm.project = project;

            //int partlistVersionID = 0;
            //if (isFocusVersionGP) partlistVersionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //else if (isFocusVersionPO) partlistVersionID = TextUtils.ToInt(grvDataPO.GetFocusedRowCellValue("ID"));

            frm.partlistVersionID = partlistVersionID;
            frm.projectTypeName = projectTypeName;
            frm.partListTypeID = partListTypeID;
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //loadData();
            //}
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        void UpdateAdditionalPartlistPO(int newVersionId, int oldVersionId)
        {
            try
            {
                Regex regex = new Regex(@"^-?[\d\.]+$");

                List<ProjectPartListDTO> listPartlists = SQLHelper<ProjectPartListDTO>.ProcedureToList("spGetProjectPartList_Khanh",
                                new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                new object[] { project.ID, 0, -1, "", -1, -1, oldVersionId });


                foreach (var node in treeListData.GetAllCheckedNodes())
                {
                    if (!listPartListVersionId.Contains(TextUtils.ToInt(node.GetValue(colID))))
                    {
                        listPartListVersionId.Add(TextUtils.ToInt(node.GetValue(colID)));
                    }

                    TreeListNode parent = node.ParentNode;
                    while (parent != null)
                    {
                        int parentID = TextUtils.ToInt(parent.GetValue(colID));
                        if (!listPartListVersionId.Contains(parentID))
                        {
                            listPartListVersionId.Add(parentID);
                        }
                        parent = parent.ParentNode;
                    }

                    if (!node.HasChildren)
                    {
                        if (!listPartListVersionChilId.Contains(TextUtils.ToInt(node.GetValue(colID))))
                        {
                            listPartListVersionChilId.Add(TextUtils.ToInt(node.GetValue(colID)));
                        }
                    }
                }

                listPartListVersionId.Sort();
                bool isAddPartlist = false;
                foreach (int id in listPartListVersionId)
                {
                    var item = listPartlists.Where(x => x.ID == id).FirstOrDefault();

                    if (item != null)
                    {
                        string stt = item.TT;
                        if (string.IsNullOrEmpty(stt)) continue;
                        if (!regex.IsMatch(stt)) continue;

                        var exp1 = new Expression("ProjectPartListVersionID", newVersionId);
                        var exp2 = new Expression("TT", stt);
                        var exp3 = new Expression("IsDeleted", 0);
                        ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
                        if (partList != null) continue;
                        else partList = new ProjectPartListModel();

                        partList.ProjectID = item.ProjectID;
                        partList.TT = stt;

                        partList.ParentID = GetParentId(stt, newVersionId, false);
                        partList.ProjectTypeID = item.ProjectTypeID;
                        partList.ProjectPartListVersionID = newVersionId;

                        partList.GroupMaterial = item.GroupMaterial;
                        partList.ProductCode = item.ProductCode;
                        partList.OrderCode = item.OrderCode;
                        partList.Manufacturer = item.Manufacturer;
                        partList.Model = item.Model;
                        partList.QtyMin = item.QtyMin;
                        partList.QtyFull = item.QtyFull;
                        partList.Unit = item.Unit;
                        partList.Price = item.Price; ;
                        partList.Amount = item.Amount;
                        partList.LeadTime = item.LeadTime;
                        partList.NCC = item.NCC;
                        partList.RequestDate = item.RequestDate;
                        partList.LeadTimeRequest = item.LeadTimeRequest;
                        partList.QuantityReturn = item.QuantityReturn;
                        partList.NCCFinal = item.NCCFinal;
                        partList.PriceOrder = item.PriceOrder;
                        partList.OrderDate = item.OrderDate;
                        partList.ExpectedReturnDate = item.ExpectedReturnDate;
                        partList.Status = item.Status;
                        partList.Quality = item.Quality;
                        partList.Note = item.Note;
                        partList.ReasonProblem = listPartListVersionChilId.Contains(item.ID) ? reasionProblem : "";
                        partList.IsProblem = partList.ReasonProblem != "" ? true : false;
                        partList.IsDeleted = item.IsDeleted;

                        partList.SpecialCode = item.SpecialCode;
                        SQLHelper<ProjectPartListModel>.Insert(partList);
                        isAddPartlist = true;
                    }

                }

                if (isAddPartlist == true)
                {
                    MessageBox.Show("Đã bổ sung partlist PO!", "Thông báo", MessageBoxButtons.OK);
                    LoadVersionPO();
                }
                else MessageBox.Show("Vật tư được chọn đã tồn tại PO vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdditionalPartlistPO_Click(object sender, EventArgs e)
        {
            if (treeListData.GetAllCheckedNodes().Count <= 0)
            {
                MessageBox.Show("Vui lòng tích chọn partlist cần bổ sung!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
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
            var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault() ?? new ProjectPartListVersionModel();
            if (versions.ID > 0)
            {
                ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(id);
                frmAdditionalReasonProblem frm = new frmAdditionalReasonProblem();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    reasionProblem = frm.resionProblem;
                    UpdateAdditionalPartlistPO(versions.ID, versionModel.ID);
                }

            }
            else
            {
                ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(id);

                ProjectPartListVersionModel newVersion = new ProjectPartListVersionModel();

                newVersion.IsApproved = false;
                newVersion.IsActive = false;
                newVersion.StatusVersion = 2;
                newVersion.ProjectID = versionModel.ProjectID;
                newVersion.STT = versionModel.STT;
                newVersion.Code = versionModel.Code;
                newVersion.DescriptionVersion = versionModel.DescriptionVersion;
                newVersion.ProjectSolutionID = versionModel.ProjectSolutionID;

                newVersion.ProjectTypeID = versionModel.ProjectTypeID;
                newVersion.ApprovedID = versionModel.ApprovedID;

                newVersion.ID = SQLHelper<ProjectPartListVersionModel>.Insert(newVersion).ID;
                frmAdditionalReasonProblem frm = new frmAdditionalReasonProblem();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    reasionProblem = frm.resionProblem;
                    UpdateAdditionalPartlistPO(newVersion.ID, versionModel.ID);
                }
            }
        }

        private void btnTechBought_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
            string billCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colBillCodePurchase));
            string productCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode));
            if (!string.IsNullOrWhiteSpace(billCode))
            {
                MessageBox.Show($"Mã sản phẩm [{productCode}] đã có đơn mua [{billCode}], bạn không thể mua!", "Thông báo");
                return;
            }

            frmProjectPartlistPurchaseRequestDetail frm = new frmProjectPartlistPurchaseRequestDetail();
            frm.requestBought = new ProjectPartlistPurchaseRequestModel()
            {
                IsTechBought = id > 0,
                ProjectPartListID = id,
                ProductCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode)),
                ProductName = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colGroupMaterial)),
                Quantity = TextUtils.ToDecimal(treeListData.GetFocusedRowCellValue(colQtyFull)),
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnUnTechBought_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(treeListData.GetFocusedRowCellValue(colID));
            string billCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colBillCodePurchase));
            string productCode = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode));
            if (!string.IsNullOrWhiteSpace(billCode))
            {
                MessageBox.Show($"Mã sản phẩm [{productCode}] đã có đơn mua [{billCode}], bạn không thể hủy!", "Thông báo");
                return;
            }


            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn Hủy đã mua sản phẩm [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {ProjectPartlistPurchaseRequestModel_Enum.IsDeleted.ToString(),true },
                    {ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                    {ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                };
                var exp = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ProjectPartListID, id);
                SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);
                loadData();
            }
        }

        private void stackPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        #region VTN Update 6325
        //bool isApprovedNewCode = false;
        private void btnTBPApprovedNew_Click(object sender, EventArgs e)
        {
            //isApprovedNewCode = true;
            approvedNewCode(true);
        }

        private void btnUnApprovedNew_Click(object sender, EventArgs e)
        {
            //isApprovedNewCode = true;
            approvedNewCode(false);
        }

        void approvedNewCode(bool isApproved)
        {
            
            string approvedText = isApproved ? "duyệt" : "hủy duyệt";

            var listNodes = treeListData.GetAllCheckedNodes();

            if (listNodes.Count <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm cần duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            //-------------------------------------Hàm validate-------------------- -
            foreach (var node in listNodes)
            {
                if (node.HasChildren) continue;

                string errorMessage;
                if (!ValidateProduct(node, out errorMessage, false))
                {
                    MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            //---------------------------------------------------------------------


            DialogResult result = MessageBox.Show($"Bạn có chắc muốn {approvedText} các vật tư mới đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<FirmModel> firms = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 1);
                List<FirmModel> firmDemos = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 2);
                foreach (var node in listNodes)
                {
                    if (node.HasChildren) continue;

                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode));

                    if (!isNewCode && isApproved) continue;

                    string maker = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer)).ToUpper().Trim();
                    string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit));
                    string productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                    string productName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));

                    if (string.IsNullOrWhiteSpace(productCode) || string.IsNullOrEmpty(productCode)) continue;

                    Expression ex1 = new Expression("IsDeleted", 0);
                    Expression ex2 = new Expression("ProductCode", productCode);
                    Expression ex3 = new Expression("IsFix", 1);
                    var productSales = SQLHelper<ProductSaleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new ProductSaleModel();

                    List<string> errors = new List<string>();
                    if (productSales.IsFix && isApproved) continue; //Nếu mã hàng đã được tích xanh

                    ProjectPartListModel model = SQLHelper<ProjectPartListModel>.FindByID(id);
                    model.IsApprovedTBPNewCode = isApproved;
                    model.DateApprovedNewCode = DateTime.Now;
                    model.EmployeeApprovedNewCode = Global.EmployeeID;
                    //ProjectPartListBO.Instance.Update(model);
                    SQLHelper<ProjectPartListModel>.Update(model);

                    if (!isApproved) CheckUpdateIsApprovedNewCode(isApproved, node);


                    //Update thông tin mã đang có trong kho
                    FirmModel firm = firms.FirstOrDefault(x => x.FirmName.ToUpper().Trim() == maker && x.FirmType == 2) ?? new FirmModel();
                    //if (firm.ID <= 0) continue;
                    var myDict = new Dictionary<string, object>()
                    {
                        { "FirmID",firm.ID},
                        { "Unit",unit},
                        { "ProductName",productName},
                        { "Maker",maker},
                        { "UpdatedDate",DateTime.Now},
                        { "UpdatedBy",Global.AppUserName},
                    };

                    SQLHelper<ProductSaleModel>.UpdateFields(myDict, new Expression("ProductCode", productCode));

                    //Update sản phẩm kho demo
                    var unitCountKT = SQLHelper<UnitCountKTModel>.FindByAttribute("UnitCountName", unit).FirstOrDefault() ?? new UnitCountKTModel();
                    FirmModel firmDemo = firmDemos.FirstOrDefault(x => x.FirmName.ToUpper().Trim() == maker && x.FirmType == 2) ?? new FirmModel();
                    var myDictDemo = new Dictionary<string, object>()
                    {
                        { ProductRTCModel_Enum.FirmID.ToString(),firmDemo.ID},
                        //{ ProductRTCModel_Enum.UnitCountID.ToString(),unitCountKT.ID},
                        { ProductRTCModel_Enum.ProductName.ToString(),productName},
                        { ProductRTCModel_Enum.Maker.ToString(),maker},
                    };

                    SQLHelper<ProductRTCModel>.UpdateFields(myDictDemo, new Expression("ProductCode", productCode));

                    //Update sản phẩm yêu cầu báo giá
                    var myDictPriceRequest = new Dictionary<string, object>()
                    {
                        //{ "FirmID",firm.ID},
                        //{ ProjectPartlistPriceRequestModel_Enum.Unit.ToString(),unit},
                        { ProjectPartlistPriceRequestModel_Enum.ProductName.ToString(),productName},
                        { ProjectPartlistPriceRequestModel_Enum.Maker.ToString(),maker},
                        { ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    };

                    SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDictPriceRequest, new Expression("ProductCode", productCode));


                    //Update sản phẩm partlist
                    var myDictPartlist = new Dictionary<string, object>()
                    {
                        //{ "FirmID",firm.ID},
                        { ProjectPartListModel_Enum.Unit.ToString(),unit},
                        { ProjectPartListModel_Enum.GroupMaterial.ToString(),productName},
                        { ProjectPartListModel_Enum.Manufacturer.ToString(),maker},
                        { ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    };

                    SQLHelper<ProjectPartListModel>.UpdateFields(myDictPartlist, new Expression("ProductCode", productCode));
                }
                loadData();
            }
        }

        #endregion 



        private void btnShowProduct_Click(object sender, EventArgs e)
        {
            //string code = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode));
            //if (string.IsNullOrWhiteSpace(code)) return;
            //frmInventoryByProduct frm = new frmInventoryByProduct();

            ////Mặc định lọc những hạng mục chưa làm hoặc đang làm
            //string filterString = $"([ProductCode] = '{code}')";
            //frm.grvData.Columns["ProductCode"].FilterInfo = new ColumnFilterInfo(filterString);
            //frm.Show();


            string code = TextUtils.ToString(treeListData.GetFocusedRowCellValue(colProductCode));
            if (string.IsNullOrWhiteSpace(code)) return;
            frmViewProjectPartlist frm = new frmViewProjectPartlist(code);
            //frm.productCode = code;
            frm.Show();
        }

        private void btnGetPriceHistory_Click(object sender, EventArgs e)
        {
            var listNodes = treeListData.GetAllCheckedNodes();
            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn cập nhật đơn giá từ giá lịch sử!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn cập nhật đơn giá từ giá lịch sử danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (var item in listNodes)
                {
                    if (item.HasChildren) continue;
                    int id = TextUtils.ToInt(item.GetValue(colID.FieldName));
                    if (id <= 0) continue;

                    decimal unitPriceHistory = TextUtils.ToDecimal(item.GetValue(colUnitPriceHistory.FieldName));
                    decimal qtyFull = TextUtils.ToDecimal(item.GetValue(colQtyFull.FieldName));
                    decimal amount = unitPriceHistory * qtyFull;


                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartListModel_Enum.Price.ToString(),unitPriceHistory },
                        {ProjectPartListModel_Enum.Amount.ToString(),amount },
                        {ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    SQLHelper<ProjectPartListModel>.UpdateFieldsByID(myDict, id);
                }

                loadData();
            }
        }

        private void btnIsFix_Click(object sender, EventArgs e)
        {

            IsFixEvent(true);
            return;
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

            var listNodes = treeListData.GetAllCheckedNodes()
                           .Where(node => !node.HasChildren)
                           .ToList();

            var list = treeListData.GetNodeList();
            if (!isAciveVersion)
            {
                MessageBox.Show($"Vui lòng chọn sử dụng phiên bản [{version}] trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn vật tư!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in listNodes)
            {
                if (node.HasChildren) continue;

                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
                bool isNew = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode));
                if (isNew) continue;
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));

                if (isDelete)
                {
                    MessageBox.Show($"Không thể duyệt tích xanh vì vật tư thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //PQ.CHIEN - UPDATE - 08/08/2025
                string errorMessage;
                if (!ValidateProduct(node, out errorMessage, true))
                {
                    MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //END
            }
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn duyệt tích xanh các vật tư đã chọn?\n" +
                $"Những mã vật tư mới sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<FirmModel> firms = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 1);
                List<FirmModel> firmDemos = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 2);
                foreach (var node in listNodes)
                {
                    if (node.HasChildren) continue;

                    bool isNew = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode));
                    if (isNew) continue;

                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    string productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                    string productName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));
                    string maker = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer));
                    string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit));
                    FirmModel firm = firms.FirstOrDefault(x => x.FirmName.ToUpper().Trim() == maker && x.FirmType == 2) ?? new FirmModel();


                    //PQ.CHIEN - UPDATE - 08/08/2025
                    UpdateProductSale(node, true, firm);
                    //END

                    //ProjectPartListBO.Instance.Update(model);
                }
                loadData();
            }
        }


        private void IsFixEvent(bool isFix)
        {
            //bool isAciveVersion = false;
            //string version = "";

            //if (isFocusVersionGP)
            //{
            //    //isAciveVersion = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue("IsActive"));
            //    version = TextUtils.ToString(grvData.GetFocusedRowCellValue("Code"));
            //}
            //else if (isFocusVersionPO)
            //{
            //    //isAciveVersion = TextUtils.ToBoolean(grvDataPO.GetFocusedRowCellValue("IsActive"));
            //    version = TextUtils.ToString(grvDataPO.GetFocusedRowCellValue("Code"));
            //}

            var listNodes = treeListData.GetAllCheckedNodes()
                           .Where(node => !node.HasChildren)
                           .ToList();

            var list = treeListData.GetNodeList();
            //if (!isAciveVersion)
            //{
            //    MessageBox.Show($"Vui lòng chọn sử dụng phiên bản [{version}] trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (listNodes.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn vật tư!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in listNodes)
            {
                if (node.HasChildren) continue;

                bool isDelete = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsDeleted));
                bool isApprovedPurchase = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedPurchase));
                bool isApprovedTBP = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsApprovedTBP));
                bool isNew = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode));
                if (isNew && isFix) continue;
                string stt = TextUtils.ToString(treeListData.GetRowCellValue(node, colTT));

                if (isDelete)
                {
                    MessageBox.Show($"Không thể duyệt tích xanh vì vật tư thứ tự [{stt}] đã bị xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //PQ.CHIEN - UPDATE - 08/08/2025

                //if (!ValidateProduct(node, out errorMessage, isFix))
                //{
                //    MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //END
            }
            var message = isFix ? "Duyệt" : "Huỷ duyệt";
            var message2 = isFix ? "Những mã vật tư mới sẽ tự động được bỏ qua!" : "";
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn {message} tích xanh các vật tư đã chọn?\n" +
                $"{message2}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<FirmModel> firms = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 1);
                List<FirmModel> firmDemos = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 2);

                List<string> errorMessages = new List<string>();
                foreach (var node in listNodes)
                {
                    if (node.HasChildren) continue;

                    bool isNew = TextUtils.ToBoolean(treeListData.GetRowCellValue(node, colIsNewCode));
                    if (isNew && isFix) continue;
                    string errorMessage;
                    if (!ValidateProduct(node, out errorMessage, isFix))
                    {
                        errorMessages.Add(errorMessage);
                        continue;
                    }
                    int id = TextUtils.ToInt(treeListData.GetRowCellValue(node, colID));
                    string productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
                    string productName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));
                    string maker = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer));
                    string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit));
                    FirmModel firm = firms.FirstOrDefault(x => x.FirmName.ToUpper().Trim() == maker && x.FirmType == 2) ?? new FirmModel();


                    //PQ.CHIEN - UPDATE - 08/08/2025
                    UpdateProductSale(node, isFix, firm);
                    //END

                    //ProjectPartListBO.Instance.Update(model);
                }
                if (errorMessages.Count() > 0)
                {
                    MessageBox.Show($"{string.Join("\n", errorMessages)}", "Thông báo");
                }
                loadData();
            }
        }
        private void treeListData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(treeListData.GetFocusedRowCellValue(treeListData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }


        private void stackPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRestoreDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var nodes = treeListData.GetAllCheckedNodes();
                if (nodes.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn khôi phục!", "Thông báo");
                    return;
                }


                List<int> ids = new List<int>();
                foreach (var node in nodes)
                {
                    int id = TextUtils.ToInt(node.GetValue(colID.FieldName));
                    if (!ids.Contains(id)) ids.Add(id);
                }

                if (ids.Count <= 0) return;

                string idsText = string.Join(",", ids);
                var exp = new Expression(ProjectPartListModel_Enum.ID, idsText, "IN");
                var myDict = new Dictionary<string, object>
                {
                    { ProjectPartListModel_Enum.IsDeleted.ToString(),false},
                    { ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    { ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                SQLHelper<ProjectPartListModel>.UpdateFields(myDict, exp);
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadProjectSolution();
            LoadVersion();
            loadData();
        }

        private void treeListData_CustomRowFilter(object sender, CustomRowFilterEventArgs e)
        {
            //if (e.Node != null)
            //    e.Handled = !e.Node.Visible;
        }

        private void treeListData_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                CheckVisibleChildren(e.Node, e.Node.Checked);
            }
        }

        private void CheckVisibleChildren(TreeListNode parent, bool isChecked)
        {
            foreach (TreeListNode child in parent.Nodes)
            {
                if (child.Visible)
                    child.Checked = isChecked;

                if (child.HasChildren)
                    CheckVisibleChildren(child, isChecked);
            }
        }

        private void btnUnFix_Click(object sender, EventArgs e)
        {
            IsFixEvent(false);
        }

        private void btnRequestExportDP_Click(object sender, EventArgs e)
        {
            RequestExport((ToolStripMenuItem)sender, false);
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            //ProjectModel projectSelected = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
            project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
            string caption = isTBP ? "TBP DUYỆT" : "";
            this.Text = $"{caption} VẬT TƯ DỰ ÁN {project.ProjectCode}";


            LoadProjectSolution();
            LoadVersion();
            loadData();
        }

        private void btnSelectProductPO_Click(object sender, EventArgs e)
        {
            if (treeListData.GetAllCheckedNodes().Count <= 0)
            {
                this.Close();
                return;
            }
            int maxSTT = 1;
            //int childSTT = 0;
            int[] nodeLevelSTT = new int[10];
            foreach (DataRow row in dtAddDetail.Rows)
            {
                int STT = TextUtils.ToInt(row["STT"]);
                if (STT > maxSTT) maxSTT = STT;

            }
            List<TreeListNode> checkNodes = treeListData.GetAllCheckedNodes();
            int minLevel = checkNodes.Min(node => node.Level);
            foreach (var node in treeListData.GetAllCheckedNodes())
            {
                DataRow dr = dtAddDetail.NewRow();

                // dr["TT"] = node.GetValue(colTT);
                dr["ProductID"] = node.GetValue(colProductID);
                dr["ProductCode"] = node.GetValue(colProductCode);
                dr["ProductName"] = node.GetValue(colGroupMaterial);
                dr["GuestCode"] = node.GetValue(colProductCode);
                dr["Maker"] = node.GetValue(colManufacturer);
                dr["Qty"] = node.GetValue(colQtyFull);
                dr["Unit"] = node.GetValue(colUnit);
                dr["TT"] = node.GetValue(colTT);
                dr["ProjectPartListID"] = node.GetValue(colID);

                if (node.Level == minLevel)
                {
                    dr["STT"] = nodeMinLevelCount + 1;
                    nodeMinLevelCount++;
                }
                else if (/*nodeLevelSTT[node.Level] == null || */nodeLevelSTT[node.Level] == 0)
                {
                    dr["STT"] = 1;
                    nodeLevelSTT[node.Level] = 1;
                }
                else
                {
                    dr["STT"] = nodeLevelSTT[node.Level] + 1;
                    nodeLevelSTT[node.Level]++;
                }
                string childTT = node.GetValue(colTT).ToString();
                //int indexFirst = childTT.IndexOf('.');
                //if (indexFirst >= 0)
                //{
                //    string newTT = (countOldNode + 1).ToString() + childTT.Substring(1, childTT.Length - 1);
                //    dr["TT"] = newTT;
                //}
                //else
                //{
                //    dr["TT"] = (countOldNode + 1);
                //}

                int indexLast = childTT.LastIndexOf('.');
                if (indexLast >= 0)
                {
                    string parentTT = childTT.Remove(indexLast);
                    if (childTT == parentTT)
                    {
                        dr["ParentID"] = 0;
                    }
                    else
                    {
                        //TreeListNode parentNode = treeListData.FindNodeByFieldValue("TT", parentTT);
                        dr["ParentID"] = node.GetValue(colParentID);
                    }
                }
                else
                {
                    dr["ParentID"] = 0;
                }

                int id = Lib.ToInt(node.GetValue(colID));
                dr["ID"] = id;
                listIDInsert.Add(id);
                dtAddDetail.Rows.Add(dr);
            }
            this.DialogResult = DialogResult.OK;
        }


        private void CalculatorData()
        {
            List<TreeListNode> lst = treeListData.GetNodeList();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                TreeListNode node = lst[i];
                if (!node.HasChildren) continue;

                node.SetValue(colIsNewCode.FieldName, false);
                node.SetValue(colIsApprovedTBPNewCode.FieldName, false);

                decimal totalAmount = 0;
                decimal totalAmountQuote = 0;
                decimal totalAmountPurchase = 0;
                decimal totalPriceExchangePurchase = 0;
                decimal totalPriceExchangeQuote = 0;


                foreach (TreeListNode nodeChild in node.Nodes)
                {
                    totalAmount += TextUtils.ToDecimal(nodeChild["Amount"]);
                    totalAmountQuote += TextUtils.ToDecimal(nodeChild["TotalPriceQuote"]);
                    totalAmountPurchase += TextUtils.ToDecimal(nodeChild["TotalPricePurchase"]);
                    totalPriceExchangePurchase += TextUtils.ToDecimal(nodeChild[colTotalPriceExchangePurchase.FieldName]);
                    totalPriceExchangeQuote += TextUtils.ToDecimal(nodeChild[colTotalPriceExchangeQuote.FieldName]);
                }

                if (node.HasChildren) node[colPrice.FieldName] = 0;

                node["Amount"] = totalAmount;
                node["TotalPriceQuote"] = totalAmountQuote;
                node["TotalPricePurchase"] = totalAmountPurchase;
                node[colTotalPriceExchangePurchase.FieldName] = totalPriceExchangePurchase;
                node[colTotalPriceExchangeQuote.FieldName] = totalPriceExchangeQuote;
            }
        }

        private void CheckUpdateIsApprovedNewCode(bool isApprovedTBPNewCode, TreeListNode nodeSeleted)
        {
            List<ProjectPartListModel> partlists = new List<ProjectPartListModel>();
            if (!isApprovedTBPNewCode && nodeSeleted != null)
            {
                if (nodeSeleted.HasChildren) return;
                int id = TextUtils.ToInt(nodeSeleted.GetValue(ProjectPartListModel_Enum.ID.ToString()));

                if (id <= 0) return;
                bool isNew = TextUtils.ToBoolean(nodeSeleted.GetValue(ProjectPartListModel_Enum.IsNewCode.ToString()));
                if (!isNew) return;
                string productCode = TextUtils.ToString(nodeSeleted.GetValue(ProjectPartListModel_Enum.ProductCode.ToString()));
                string productName = TextUtils.ToString(nodeSeleted.GetValue(ProjectPartListModel_Enum.GroupMaterial.ToString()));
                string maker = TextUtils.ToString(nodeSeleted.GetValue(ProjectPartListModel_Enum.Manufacturer.ToString()));
                string unit = TextUtils.ToString(nodeSeleted.GetValue(ProjectPartListModel_Enum.Unit.ToString()));


                //Tìm xem partlist đã tồn tại chưa
                var exp1 = new Expression(ProjectPartListModel_Enum.ProductCode, productCode);
                var exp2 = new Expression(ProjectPartListModel_Enum.GroupMaterial, productName);
                var exp3 = new Expression(ProjectPartListModel_Enum.Manufacturer, maker);
                var exp4 = new Expression(ProjectPartListModel_Enum.Unit, unit);
                var exp5 = new Expression(ProjectPartListModel_Enum.IsDeleted, 1, "<>");
                partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));

                //if (!partlists.Any(x => x.IsApprovedTBPNewCode == true)) return; //Nếu không tìm thấy partlist nào có duyệt mới 

                //ids.Add(id);

                if (partlists.Count() > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartListModel_Enum.IsApprovedTBPNewCode.ToString(),isApprovedTBPNewCode },
                        {ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    var exp = new Expression(ProjectPartListModel_Enum.ID, string.Join(",", partlists.Select(x => x.ID)), "IN");
                    SQLHelper<ProjectPartListModel>.UpdateFields(myDict, exp);
                }
            }
            else
            {

                foreach (var node in treeListData.GetNodeList())
                {
                    if (node.HasChildren) continue;
                    int id = TextUtils.ToInt(node.GetValue(ProjectPartListModel_Enum.ID.ToString()));

                    if (id <= 0) continue;
                    bool isNew = TextUtils.ToBoolean(node.GetValue(ProjectPartListModel_Enum.IsNewCode.ToString()));
                    if (!isNew) continue;
                    //bool isApprovedNewCode = TextUtils.ToBoolean(node.GetValue(ProjectPartListModel_Enum.IsApprovedTBPNewCode.ToString()));
                    //if (isApprovedNewCode) continue;
                    string productCode = TextUtils.ToString(node.GetValue(ProjectPartListModel_Enum.ProductCode.ToString()));
                    string productName = TextUtils.ToString(node.GetValue(ProjectPartListModel_Enum.GroupMaterial.ToString()));
                    string maker = TextUtils.ToString(node.GetValue(ProjectPartListModel_Enum.Manufacturer.ToString()));
                    string unit = TextUtils.ToString(node.GetValue(ProjectPartListModel_Enum.Unit.ToString()));


                    //Tìm xem partlist đã tồn tại chưa
                    var exp1 = new Expression(ProjectPartListModel_Enum.ProductCode, productCode);
                    var exp2 = new Expression(ProjectPartListModel_Enum.GroupMaterial, productName);
                    var exp3 = new Expression(ProjectPartListModel_Enum.Manufacturer, maker);
                    var exp4 = new Expression(ProjectPartListModel_Enum.Unit, unit);
                    var exp5 = new Expression(ProjectPartListModel_Enum.IsDeleted, 1, "<>");
                    //var exp6 = new Expression(ProjectPartListModel_Enum.ID, id, "<>");
                    partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));

                    if (!partlists.Any(x => x.IsApprovedTBPNewCode == true)) continue; //Nếu không tìm thấy partlist nào có duyệt mới 

                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartListModel_Enum.IsApprovedTBPNewCode.ToString(),true },
                        {ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    var exp = new Expression(ProjectPartListModel_Enum.ID, string.Join(",", partlists.Select(x => x.ID)), "IN");
                    SQLHelper<ProjectPartListModel>.UpdateFields(myDict, exp);
                }


            }


        }


        #region Chiến update duyệt tích xanh
        private bool ValidateProduct(TreeListNode node, out string errorMessage, bool isFix)
        {
            errorMessage = "";
            string productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
            string productName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));
            string maker = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer));
            string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit));

            Expression ex1 = new Expression("IsDeleted", 0);
            Expression ex2 = new Expression("ProductCode", productCode);
            var productSales = SQLHelper<ProductSaleModel>.FindByExpression(ex1.And(ex2));

            //PQ,Chien - UPDATE - 12/08/2025
            if (isFix)
            {
                if (productSales.Count <= 0)
                {
                    errorMessage = $"Không thể duyệt tích xanh vì sản phẩm [{productCode}] chưa có trong kho sale";
                    return false;
                }
            }

            //END
            if (isFix)
            {
                var fixedProduct = productSales.FirstOrDefault(x => x.IsFix);

                if (fixedProduct != null)
                {
                    List<string> errors = new List<string>();
                    string productNameConvert = TextUtils.ConvertUnicode(fixedProduct.ProductName.ToLower(), 1);
                    string makerConvert = TextUtils.ConvertUnicode(fixedProduct.Maker.ToLower(), 1);
                    string unitConvert = TextUtils.ConvertUnicode(fixedProduct.Unit.ToLower(), 1);

                    if (productNameConvert != TextUtils.ConvertUnicode(productName.ToLower(), 1))
                    {
                        errors.Add($"\nMã sản phẩm (tích xanh: [{fixedProduct.ProductName}], hiện tại: [{productName}])");
                    }
                    if (makerConvert != TextUtils.ConvertUnicode(maker.ToLower(), 1))
                    {
                        errors.Add($"\nNhà sản xuất (tích xanh: [{fixedProduct.Maker}], hiện tại: [{maker}])");
                    }
                    if (unitConvert != TextUtils.ConvertUnicode(unit.ToLower(), 1))
                    {
                        errors.Add($"\nĐơn vị (tích xanh: [{fixedProduct.Unit}], hiện tại: [{unit}])");
                    }

                    if (errors.Any())
                    {
                        errorMessage = $"Sản phẩm có mã [{productCode}] đã có tích xanh.\nCác trường không khớp: {string.Join(" ", errors)}. \nVui lòng kiểm tra lại.";
                        return false;
                    }
                }
            }

            return true;
        }

        private void UpdateProductSale(TreeListNode node, bool isFix, FirmModel firm)
        {
            string productCode = TextUtils.ToString(treeListData.GetRowCellValue(node, colProductCode));
            string productName = TextUtils.ToString(treeListData.GetRowCellValue(node, colGroupMaterial));
            string maker = TextUtils.ToString(treeListData.GetRowCellValue(node, colManufacturer));
            string unit = TextUtils.ToString(treeListData.GetRowCellValue(node, colUnit));

            Dictionary<string, object> myDict;

            if (isFix)
            {
                myDict = new Dictionary<string, object>
                {
                    { "ProductName", productName },
                    { "Maker", maker },
                    { "Unit", unit },
                    { "IsFix", true },
                    { "FirmID", firm.ID },
                    { "UpdatedDate", DateTime.Now },
                    { "UpdatedBy", Global.AppUserName }
                };
            }
            else
            {
                myDict = new Dictionary<string, object>
                {
                    { "IsFix", false },
                    { "UpdatedDate", DateTime.Now },
                    { "UpdatedBy", Global.AppUserName }
                };
            }

            SQLHelper<ProductSaleModel>.UpdateFields(myDict, new Expression("ProductCode", productCode));
        }
        #endregion


        #region NDNhat update cảnh báo leadtime yêu cầu mua - 28/08/2025
        private void SendWarningMail(List<DataRow> listDataMail, DateTime deadlinePur)
        {
            if (listDataMail == null || listDataMail.Count == 0) return;

            int employeeId = Global.EmployeeID;
            EmployeeModel employeeKT = SQLHelper<EmployeeModel>.FindByID(employeeId); // Kỹ thuật đẩy mua

            // Nhóm theo FullNameQuote
            var groups = listDataMail
                .GroupBy(r => TextUtils.ToString(r["FullNameQuote"]))
                .ToList();

            foreach (var group in groups)
            {
                string fullNameQuote = group.Key;
                EmployeeModel employeePur = SQLHelper<EmployeeModel>
                    .FindByAttribute("FullName", fullNameQuote)
                    .FirstOrDefault();

                StringBuilder tableBuilder = new StringBuilder();
                tableBuilder.Append(@"
                                    <table border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse; font-family:Arial; font-size:13px; text-align:center;'>
                                        <thead style='background-color:#f2f2f2; font-weight:bold;'>
                                            <tr>
                                                <th>TT</th>
                                                <th>Mã SP</th>
                                                <th>Tên SP</th>
                                                <th>Số lượng</th>
                                                <th>Leadtime báo giá (ngày)</th>
                                                <th>Deadline yêu cầu mua</th>
                                                <th>Ngày dự kiến hàng về</th>
                                            </tr>
                                        </thead>
                                        <tbody>");

                foreach (DataRow row in group)
                {
                    string tt = TextUtils.ToString(row["TT"]);
                    string productCode = TextUtils.ToString(row["ProductCode"]);
                    string productName = TextUtils.ToString(row["GroupMaterial"]);
                    int quantity = TextUtils.ToInt(row["QtyFull"]);
                    int deadlineQuote = TextUtils.ToInt(row["TotalDayLeadTimeQuote"]);

                    DateTime dtNow = DateTime.Now;
                    DateTime dateTimeDeadline = dtNow.AddDays(deadlineQuote);

                    tableBuilder.Append($@"
                                            <tr>
                                                <td>{tt}</td>
                                                <td>{productCode}</td>
                                                <td>{productName}</td>
                                                <td>{quantity}</td>
                                                <td>{deadlineQuote}</td>
                                                <td>{deadlinePur:dd/MM/yyyy}</td>
                                                <td>{dateTimeDeadline:dd/MM/yyyy}</td>
                                            </tr>");
                }

                tableBuilder.Append("</tbody></table>");

                // Gửi mail cho từng người mua
                EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();
                sendEmail.Subject = $"[CẢNH BÁO] DEADLINE MUA HÀNG - {DateTime.Now:dd/MM/yyyy}";
                sendEmail.EmailTo = employeePur?.EmailCongTy ?? "";
                sendEmail.EmailCC = employeeKT?.EmailCongTy ?? "";
                sendEmail.Body = $@"
                                    <div> 
                                        <p style=""font-weight: bold; color: red;"">[NO REPLY]</p> 
                                        <p> Dear anh/chị {(employeePur != null ? employeePur.FullName : "phụ trách")}, </p>
                                    </div>
                                    <div style=""margin-top: 20px;"">
                                        <p> Hệ thống phát hiện <b>các vật tư có Deadline yêu cầu mua sớm hơn Ngày hàng về dự kiến</b> như sau: </p>
                                        {tableBuilder}
                                    </div>
                                    <div style=""margin-top: 20px;"">
                                        <p> Vui lòng kiểm tra và cập nhật lại!</p>
                                        <p> Trân trọng, </p>
                                    </div>";

                sendEmail.StatusSend = 1;
                sendEmail.EmployeeID = employeeId;      // Người kỹ thuật gửi
                sendEmail.Receiver = employeePur?.ID ?? 0;
                sendEmail.DateSend = DateTime.Now;

                SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
            }
        }
        #endregion
    }
}