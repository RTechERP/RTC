using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms.PO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPOKH_New : _Forms
    {
        int warehouseID = 0;
        string warehouseCode = "";

        decimal Qty, QtyReturn;
        public frmPOKH_New(int warehouseID)
        {
            if (!Global.IsAdmin)
            {
                MessageBox.Show("Chức năng đã có trên web!\nVui lòng truy cập: https://erp.rtc.edu.vn/", "Thông báo");
                return;
            }

            InitializeComponent();
            this.warehouseID = warehouseID;
            this.warehouseCode = warehouseID == 1 ? "HN" : warehouseID == 2 ? "HCM" : warehouseID == 3 ? "BN" : "HP";
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            //WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            this.Text += $" - {this.Tag}";

            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-3);
            loadcbColor();
            txtPageNumber.Text = "1";
            loadgroupSale();
            loadCustomer();
            loadUser();
            loadMainIndex();
            LoadEmployeeTeamSale();
            loadPOKH();
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "p0";
            repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }


        void LoadEmployeeTeamSale()
        {
            List<EmployeeTeamSaleModel> teams = SQLHelper<EmployeeTeamSaleModel>.FindByAttribute(EmployeeTeamSaleModel_Enum.ParentID.ToString(), 0);

            cboEmployeeTeamSale.Properties.DisplayMember = "Name";
            cboEmployeeTeamSale.Properties.ValueMember = "ID";
            cboEmployeeTeamSale.Properties.DataSource = teams;
        }
        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");

            var users = SQLHelper<EmployeeModel>.FindByExpression(new Expression("UserID", 0, "<>"));
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = users;

        }
        void loadcbColor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add(i);
            }

            cbColor.Properties.DisplayMember = "ID";
            cbColor.Properties.ValueMember = "ID";
            cbColor.Properties.DataSource = dt;
        }
        /// <summary>
        /// load type
        /// </summary>
        public void loadMainIndex()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN(8,9,10,11,12,13)");
            cbStatus.Properties.DisplayMember = "MainIndex";
            cbStatus.Properties.ValueMember = "ID";
            cbStatus.Properties.DataSource = dt;
        }

        /// <summary> 
        /// load POKH
        /// </summary>
        private void loadPOKH()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int employeeTeamSaleID = TextUtils.ToInt(cboEmployeeTeamSale.EditValue);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetPOKH"
                                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@CustomerID", "@UserID", "@POType", "@Status", "@Group", "@StartDate", "@EndDate", "@WarehouseID", "@EmployeeTeamSaleID" }
                                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                        txtFilterText.Text.Trim(), TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbUser.EditValue), TextUtils.ToInt(cbStatus.EditValue),TextUtils.ToInt(cbColor.EditValue),TextUtils.ToInt( cbGroup.EditValue),dateTimeS,dateTimeE,warehouseID,employeeTeamSaleID});
            grdMaster.DataSource = oDataSet.Tables[0];


            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);

            loadPOKHDetail();
            LoadDataFile();
        }
        int IDDetail = 0;
        /// <summary>
        /// load POKH Detail
        /// </summary>
        void loadPOKHDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetail", "A", new string[] { "@ID", "@IDDetail" }, new object[] { id, IDDetail });
            TreeData.DataSource = dt;
            TreeData.ExpandAll();
        }


        void LoadDataFile()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            List<POKHFileModel> files = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", id);
            grdDataFile.DataSource = files;
        }
        #endregion

        #region Buttons Events
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmPOKHDetail_New frm = new frmPOKHDetail_New(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
                loadPOKHDetail();
            }
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(id);
            frmPOKHDetail_New frm = new frmPOKHDetail_New(warehouseID);

            // VTNam update trạng thái thanh toán
            string StatusText = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colStatus));
            frm.statusText = StatusText;
            //End update 

            frm.oPOKH = model;
            frm.ID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount > 0)
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                if (ID == 0) return;
                DataTable dttt = new DataTable();
                dttt = TextUtils.LoadDataFromSP("spLoadPOKHDetail", "A", new string[] { "@ID" }, new object[] { ID });
                if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu [{0}] hay không ?", poCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    POKHBO.Instance.Delete(ID);
                    if (dttt.Rows.Count > 0)
                    {
                        HistoryMoneyPOBO.Instance.DeleteByAttribute("POKHID", ID);
                        POKHDetailBO.Instance.DeleteByAttribute("POKHID", ID);
                        POKHDetailMoneyBO.Instance.DeleteByAttribute("POKHID", ID);
                        TreeData.DeleteSelectedNodes();
                    }
                    POKHBO.Instance.Delete(ID);
                    grvMaster.DeleteSelectedRows();
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                    grvMaster_FocusedRowChanged(null, null);
                }
            }
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] đã được duyệt.", poCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa được duyệt.", poCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }


        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            //frmPOKHExcel frmExcel = new frmPOKHExcel();
            frmPOKHImportExcel_New frm = new frmPOKHImportExcel_New(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPOKH();

        }

        private void btnConvertPO_Click(object sender, EventArgs e)
        {
            try
            {
                int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                bool Isoder = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsOder));
                if (Isoder)
                {
                    MessageBox.Show(String.Format("PO đã được đặt hàng!"), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                if (IDMaster == 0) return;
                POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(IDMaster);
                frmFollowProjectDetail frm = new frmFollowProjectDetail();
                frm.oConvert = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadPOKH();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));

                string sql = string.Format(@"UPDATE dbo.POKH SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadPOKH();
                loadPOKHDetail();
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            int GroupId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (GroupId > 0)
            {
                loadPOKHDetail();
                LoadDataFile();
                //SetCheckBoxes(TreeData.Nodes, true);
            }
        }

        //private void SetCheckBoxes(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes, bool isChecked)
        //{
        //    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in nodes)
        //    {
        //        node.Checked = isChecked;
        //        // Đệ quy gọi cho tất cả các node con
        //        if (node.Nodes.Count > 0)
        //        {
        //            SetCheckBoxes(node.Nodes, isChecked);
        //        }
        //    }
        //}

        private void cbStatus_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
        }
        private void TreeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {



            if (e.Column == colDeliveryRequestedDate)
            {
                DateTime time = TextUtils.ToDate(TreeData.GetRowCellValue(e.Node, colActualDeliveryDate).ToString());
                DateTime time1 = TextUtils.ToDate(TreeData.GetRowCellValue(e.Node, colDeliveryRequestedDate).ToString());
                int c = TextUtils.ToInt(TreeData.GetRowCellValue(e.Node, colColorDelivery));
                if (time == null)
                {
                    switch (c)
                    {
                        case 0:
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                            break;
                        case 1:
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                            break;
                        case 3:
                            e.Appearance.BackColor = Color.FromArgb(59, 89, 152);
                            break;
                        default:
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);

                            break;
                    }
                }
                else
                {
                    switch (c)
                    {
                        case 0:
                            if (time1 < time)
                            {
                                e.Appearance.BackColor = Color.Red;
                            }
                            else
                            {
                                e.Appearance.BackColor = Color.Green;
                            }
                            break;
                        case 1:
                            e.Appearance.BackColor = Color.Yellow;
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.Yellow;
                            break;
                        case 3:
                            if (time1 < time)
                            {
                                e.Appearance.BackColor = Color.Red;
                            }
                            else
                            {
                                e.Appearance.BackColor = Color.Green;
                            }
                            break;
                        default:
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);

                            break;
                    }
                }
            }
            if (e.Column == colPayDate)
            {
                DateTime timeMoney = TextUtils.ToDate(TreeData.GetRowCellValue(e.Node, colRecivedMoneyDate).ToString());
                if (timeMoney == null)
                {
                    int c = TextUtils.ToInt(TreeData.GetRowCellValue(e.Node, colColorPay));
                    switch (c)
                    {
                        case 0:
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                            break;
                        case 1:
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                            break;
                        default:
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                            break;
                    }
                }
            }
        }


        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEditGroup_Click(null, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TreeData.OptionsPrint.AutoWidth = false;
                //TreeData.OptionsPrint.ExpandAllDetails = false;
                TreeData.OptionsPrint.PrintAllNodes = true;
                TreeData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    TreeData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }
        int RowIndex;
        private void lịchSửTiềnVềToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmHistoryMoney_New frm = new frmHistoryMoney_New();
            frm.txtFilter.Text = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
            frm.Show();
            //return;

            //RowIndex = grvMaster.FocusedRowHandle;
            //string project = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProject));

            //double totalMoneyPO = TextUtils.ToDouble(grvMaster.GetFocusedRowCellValue(colTotalMoneyPO)); //VtNam update 18/09/2024

            //frmHistoryMoney frm = new frmHistoryMoney();
            ////int poid = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster)); //Duy Anh update 25/2/2024
            ////frmHistoryMoneyNew frm = new frmHistoryMoneyNew(poid);

            ////frm.project = project;
            ////frm.ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));

            ////frm.totalMoneyPO = totalMoneyPO;//VtNam update 18/09/2024
            ////frm.totalQuantityRequest = TextUtils.ToDecimal(TreeData.GetSummaryValue(colQty)); //VtNam update 18/09/2024
            ////frm.totalQuantityReturn = TextUtils.ToDecimal(TreeData.GetSummaryValue(colQuantityReturn)); //VtNam update 18/09/2024
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //    loadPOKH();
            //    loadPOKHDetail();
            //}
            //grvMaster.FocusedRowHandle = RowIndex;
        }

        #region VtNam update trạng thái thanh toán
        //decimal Qty, QtyReturn;

        //void ProcessNodes(TreeListNodes nodes)
        //{
        //    foreach (TreeListNode node in nodes)
        //    {
        //        var value1 = node.GetValue($"{colQty.FieldName}");
        //        var value2 = node.GetValue($"{colQuantityReturn.FieldName}");

        //        Qty += TextUtils.ToDecimal(value1);
        //        QtyReturn += TextUtils.ToDecimal(value2);

        //        if (node.HasChildren)
        //        {
        //            ProcessNodes(node.Nodes);
        //        }
        //    }
        //}

        private void grvMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            DateTime dateUpdateStatus = new DateTime(2024, 09, 18, 12, 0, 0);
            if (DateTime.Now <= dateUpdateStatus)
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colColorStatus));
                if (e.Column == colStatus)
                {
                    switch (status)
                    {
                        case 0:
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                            break;
                        case 3:
                            e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                            break;
                        case 4:
                            e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                            break;
                        case 5:
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colPaymentStatus));
                if (e.Column == colStatus)
                {
                    switch (status)
                    {
                        //VTNam update trạng thái thanh toán đổi màu theo trạng thái thanh toán
                        //case 0:
                        //    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);

                        //    break;
                        case 1:
                            e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                            break;
                        case 2:
                            e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                            break;
                        case 3:
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                            break;
                        //case 4:
                        //    e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                        //    break;
                        //case 5:
                        //    e.Appearance.BackColor = Color.FromArgb(155, 194, 230);

                        //break;
                        default:
                            break;
                    }
                }
            }

        }
        #endregion



        private void cbvColor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(cbvColor.GetRowCellValue(e.RowHandle, colColor));
            switch (status)
            {
                //VTNam update trạng thái thanh toán đổi màu theo trạng thái thanh toán
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 3:
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case 4:
                    e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;

            }

        }

        private void cbColor_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
            int color = TextUtils.ToInt(cbColor.EditValue);
            switch (color)
            {
                case 0:
                    cbColor.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    cbColor.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case 2:
                    cbColor.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 3:
                    cbColor.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 4:
                    cbColor.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvMaster.OptionsPrint.AutoWidth = false;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvMaster.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //frmViewPOKH frm = new frmViewPOKH(warehouseID);
            frmViewPOKH_TA frm = new frmViewPOKH_TA(warehouseID);
            frm.Show();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadPOKH();
            loadPOKHDetail();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadPOKH();
            loadPOKHDetail();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadPOKH();
            loadPOKHDetail();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadPOKH();
            loadPOKHDetail();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadPOKH();
            loadPOKHDetail();
        }

        private void đãTạoPhiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RowIndex = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (MessageBox.Show("Bạn có muốn đổi trạng thái thành Đã xuất không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TextUtils.ExcuteSQL($"Update POKHDetail set IsExport= 1 where POKHID ={ID}");
                loadPOKHDetail();
            }
            grvMaster.FocusedRowHandle = RowIndex;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cHITIẾTTIỀNVỀToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RowIndex = grvMaster.FocusedRowHandle;
            frmViewHistoryMoney frm = new frmViewHistoryMoney();
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
            grvMaster.FocusedRowHandle = RowIndex;
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            frmViewHistoryMoney frm = new frmViewHistoryMoney();
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
        }


        private void btnRequestBuy_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int isApproved = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
            if (id <= 0) return;
            //if (isApproved == 0)
            //{
            //    MessageBox.Show($"PO Khách hàng có mã '{code}' chưa được duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            frmPORequestBuyRTC frm = new frmPORequestBuyRTC();
            frm.id = id;
            frm.ShowDialog();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmPOKHDetail_New frm = new frmPOKHDetail_New(warehouseID);
            frm.Show();

        }

        private void btnTestEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(id);
            frmPOKHDetail_New frm = new frmPOKHDetail_New(warehouseID);
            frm.oPOKH = model;
            frm.ID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnTreeFolder_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "POKH").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                //string path = @"\\192.168.1.190";
                string path = config.KeyValue.Trim();
                if (Global.IsOnline) path = path.Replace("192.168.1.190", "113.190.234.64");
                if (warehouseID == 1) path = path.Replace("Sales HCM", "PO sales MRO base");

                string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("PONumber"));
                path = Path.Combine(path, poCode);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnWarehouseReleaseRequest_Click(object sender, EventArgs e)
        {
            //frmPOKHDataNew frm = new frmPOKHDataNew(warehouseID);
            frmPOKHDataNew_Clone frm = new frmPOKHDataNew_Clone(warehouseID);
            frm.Tag = warehouseCode;
            frm.Show();
            return;

            int focusedRowHandle = grvMaster.FocusedRowHandle;
            int POKHID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (POKHID <= 0) return;

            var nodeSelecteds = TreeData.GetAllCheckedNodes();
            if (nodeSelecteds.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu xuất kho!", "Thông báo");
                return;
            }
            //DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu nhập kho danh sách sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            {
                List<frmBillExportDetailNew> formList = new List<frmBillExportDetailNew>();
                // tạo bảng bằng cách clone từ spGetBillExportDetail
                DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { -100 });
                DataTable dtDetailForForm = dtDetail.Clone();
                dtDetailForForm.Columns.Add("productGroupID");

                BillExportModel model = new BillExportModel();
                var sortNodeSelecteds = nodeSelecteds.OrderBy(node => (int)node.GetValue("ProductGroupID")).ToList();
                int stt = 1;
                for (int i = 0; i < sortNodeSelecteds.Count; i++)
                {
                    int projectID = TextUtils.ToInt(sortNodeSelecteds[i]["ProjectID"]);
                    int POKHDetailID = TextUtils.ToInt(sortNodeSelecteds[i]["ID"]);
                    decimal Quantity = TextUtils.ToDecimal(sortNodeSelecteds[i]["Qty"]);

                    model.CustomerID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colCustomerID));
                    model.UserID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUserID));
                    model.KhoTypeID = TextUtils.ToInt(sortNodeSelecteds[i]["ProductGroupID"]);
                    model.ProductType = 1;
                    model.IsMerge = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsMerge));
                    model.Status = 6;

                    model.RequestDate = DateTime.Now; /////////////////-------------------------------------------------- 14/06/2024



                    ProductGroupModel pgModel = new ProductGroupModel();
                    if (model.KhoTypeID > 0)
                    {
                        pgModel = SQLHelper<ProductGroupModel>.FindByID(TextUtils.ToInt(model.KhoTypeID));
                    }

                    ProjectModel projectModel = new ProjectModel();
                    if (projectID > 0)
                    {
                        projectModel = SQLHelper<ProjectModel>.FindByID(projectID);
                    }

                    List<BillExportDetailModel> listExport = POKHDetailID > 0 ? SQLHelper<BillExportDetailModel>.FindByExpression(new Expression("POKHDetailID", POKHDetailID)) : new List<BillExportDetailModel>();

                    // tính tổng số lượng của BillExportDetail nếu = 0 thì bỏ qua nếu khác = thì tiếp tục
                    if (listExport.Count > 0)
                    {
                        decimal sumQty = listExport.Sum(itemListExport => TextUtils.ToDecimal(itemListExport.Qty));
                        decimal sumNumber = TextUtils.ToDecimal(Quantity - sumQty);
                        Quantity = sumNumber;

                        if (Quantity == 0)
                        {
                            // add thông báo đã có phiếu xuất kho
                            continue;
                        }
                    }

                    //nếu productGroupID có sự thay đổi thì add dữ liệu dable vào form mới và xóa dữ liệu bảng đấy đi
                    if (dtDetailForForm.Rows.Count > 0)
                    {
                        int dtProductGroupID = TextUtils.ToInt(dtDetailForForm.Rows[0]["productGroupID"]);
                        if (dtProductGroupID != model.KhoTypeID)
                        {
                            AddFormNew(dtDetailForForm, model, formList);
                            stt = 1;
                        }
                    }

                    //string poNumber = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPONumber));

                    DataRow item = dtDetailForForm.NewRow();
                    item["ProductID"] = TextUtils.ToInt(sortNodeSelecteds[i]["ProductID"]);
                    //item["Qty"] = TextUtils.ToDecimal(Quantity);
                    item["Qty"] = TextUtils.ToInt(sortNodeSelecteds[i]["QuantityRemain"]);
                    item["ProjectID"] = TextUtils.ToInt(projectModel.ID);
                    //item["Note"] = TextUtils.ToString(sortNodeSelecteds[i]["Note"]);
                    item["Note"] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPONumber));
                    item["ProductCode"] = TextUtils.ToString(sortNodeSelecteds[i]["ProductCode"]);
                    item["ProductNewCode"] = TextUtils.ToString(sortNodeSelecteds[i]["ProductNewCode"]);
                    item["ProductName"] = TextUtils.ToString(sortNodeSelecteds[i]["ProductName"]);
                    item["Unit"] = TextUtils.ToString(sortNodeSelecteds[i]["Unit"]);
                    item["ProductGroupName"] = TextUtils.ToString(pgModel.ProductGroupName);
                    item["ItemType"] = TextUtils.ToString(sortNodeSelecteds[i]["ItemType"]);
                    item["ProjectNameText"] = TextUtils.ToString(projectModel.ProjectName);
                    item["ProjectCodeText"] = TextUtils.ToString(projectModel.ProjectCode);
                    item["productGroupID"] = model.KhoTypeID;
                    item["POKHID"] = POKHID;
                    item["POKHDetailID"] = POKHDetailID;
                    item["STT"] = stt++;
                    item["UserReceiver"] = TextUtils.ToString(sortNodeSelecteds[i]["UserReceiver"]);
                    item["QuantityRemain"] = TextUtils.ToInt(sortNodeSelecteds[i]["QuantityRemain"]);
                    dtDetailForForm.Rows.Add(item);
                }
                // tạo form mới nếu là row cuối cùng
                if (dtDetailForForm.Rows.Count > 0)
                {
                    AddFormNew(dtDetailForForm, model, formList);
                }

                // Hiển thị các form đã được tạo
                foreach (frmBillExportDetailNew frmMain in formList)
                {
                    if (frmMain.ShowDialog() == DialogResult.OK)
                    {
                        loadPOKH();
                        grvMaster.FocusedRowHandle = focusedRowHandle;
                        //grvMaster_FocusedRowChanged(null, null);
                    }
                }

            }
        }


        private void AddFormNew(DataTable dtDetailForForm, BillExportModel model, List<frmBillExportDetailNew> formList)
        {
            frmBillExportDetailNew frmEnd = new frmBillExportDetailNew(false);
            //frmEnd.customerID = customerID;
            //frmEnd.KhoTypeID = productGroupID;
            //frmEnd.saleAdminID = saleAdminID;
            frmEnd.billExport = model;
            frmEnd.dtDetail = dtDetailForForm.Copy();
            frmEnd.WarehouseCode = warehouseCode;
            frmEnd.isPOKH = true;
            formList.Add(frmEnd);
            dtDetailForForm.Clear();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            TreeData.CheckAll();
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            TreeData.UncheckAll();
        }

        private void btnFollowProductReturn_Click(object sender, EventArgs e)
        {
            frmChiTietPOKH frm = new frmChiTietPOKH();
            frm.dateStart = dtpStartDate.Value;
            //frm.dtpEndDate.Value = dtpEndDate.Value;
            frm.Show();
        }

        private void btnListRequestBuy_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

            frmProjectPartlistPurchaseRequest frm = new frmProjectPartlistPurchaseRequest();
            frm.poKHID = id;
            frm.listRequestBuySelect = true;
            frm.Show();
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(id);
            frmPOKHDetail_New frm = new frmPOKHDetail_New(warehouseID);
            frm.oPOKH = model;
            frm.ID = id;
            frm.isCopy = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
                //grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnRequestPrice_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            frmPORequestPriceRTC frm = new frmPORequestPriceRTC();
            frm.POKHID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnListRequestPrice_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

            frmProjectPartlistPriceRequestNew frm = new frmProjectPartlistPriceRequestNew(0);
            frm.poKHID = id;
            frm.Show();
        }

        private void TreeData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(TreeData.GetFocusedRowCellValue(TreeData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvDataFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //string pathDownload = Path.Combine(KnownFolders.Downloads.Path, "DeNghiThanhToan");
                //string pathDownload = Path.Combine(Application.StartupPath, "DeNghiThanhToan");

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "POKH");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "POKH").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}

                //DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
                //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
                //string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                string pathServer = "pokhhn";
                if (warehouseID == 2) pathServer = "pokhhcm";
                string poNumber = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("PONumber"));

                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/{pathServer}/{poNumber}/{fileName}";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnCopyContext_Click_Click(object sender, EventArgs e)
        {
            btnCopy_Click(null, null);
        }
    }
}



