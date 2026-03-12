using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraEditors.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Forms.KPI_PO;
using Forms.PO;
using System.Collections;

namespace BMS
{
    public partial class frmPOKH_KPI_05102022 : _Forms
    {
        int warehouseID = 0;
        public frmPOKH_KPI_05102022(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            this.Text += $" - {this.Tag}";

            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-3);
            loadcbColor();
            txtPageNumber.Text = "1";
            loadgroupSale();
            loadCustomer();
            loadUser();
            loadMainIndex();
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
        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;


        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

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

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetPOKH"
                                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@CustomerID", "@UserID", "@POType", "@Status", "@Group", "@StartDate", "@EndDate", "@WarehouseID" }
                                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                        txtFilterText.Text.Trim(), TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbUser.EditValue), TextUtils.ToInt(cbStatus.EditValue),TextUtils.ToInt(cbColor.EditValue),TextUtils.ToInt( cbGroup.EditValue),dateTimeS,dateTimeE,warehouseID});
            grdMaster.DataSource = oDataSet.Tables[0];


            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        int IDDetail = 0;

        /// <summary>
        /// New POKHDetail
        /// </summary>
        void loadPOKHDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int[] RowIndex = grvMaster.GetSelectedRows();
            string IDMaster = "";
            Dictionary<int, bool> dic = new Dictionary<int, bool>();
            for (int i = 0; i < RowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(RowIndex[i], colIDMaster));
              //  bool Merge = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colMerge));
                IDMaster += id + ",";
                //dic.Add(id, Merge);
            }
            
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKH_KPI", "A", new string[] { "@IDMaster" }, new object[] { IDMaster });
            grdData.DataSource = dt;

            //foreach (var item in dic)
            //{
            //    if (item.Value)
            //    {
            //        grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //        colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //        colNgayNhanPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //        colPONumber.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //        grvData.OptionsView.AllowCellMerge = true;
            //        colGroup.GroupIndex = 0;
            //    }
            //    else
            //    {
            //        grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //        grvData.OptionsView.AllowCellMerge = false;
            //        colGroup.GroupIndex = -1;
            //    }
            //}

        }
        private void grvMaster_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            loadPOKHDetail();
        }

        /// <summary>
        /// load POKH Detail
        /// </summary>
        //void loadPOKHDetail()
        //{
        //    if (grvMaster.RowCount <= 0) return;
        //    int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
        //    DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetail", "A", new string[] { "@ID", "@IDDetail" }, new object[] { id, IDDetail });
        //    grdData.DataSource = dt;
        //    bool Merge = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colMerge));

        //    grdData.DataSource = dt;

        //    if (Merge)
        //    {
        //        grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
        //        colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
        //        colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
        //        grvData.OptionsView.AllowCellMerge = true;
        //        colGroup.GroupIndex = 0;
        //    }
        //    else
        //    {
        //        grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
        //        grvData.OptionsView.AllowCellMerge = false;
        //        colGroup.GroupIndex = -1;
        //    }

        //}
        #endregion
        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string checkvalue = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, colIntoMoney));
            if ((e.Column == colIntoMoney && checkvalue != "") || e.Column == colUnitPrice || e.Column == colQty || e.Column == colVAT || e.Column == colTotalPriceIncludeVAT || e.Column == colBillNumber)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == 0);
                e.Handled = true;
                return;
            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == "");
                e.Handled = true;
            }

        }
        #region Buttons Events
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmPOKHDetail frm = new frmPOKHDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKHDetail();
                loadPOKH();
            }
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(id);
            frmPOKHDetail frm = new frmPOKHDetail(warehouseID);
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
                        grvData.DeleteSelectedRows();
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
            frmPOKHExcel frmExcel = new frmPOKHExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
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

        }

        private void cbStatus_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
            loadPOKHDetail();
        }


        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            //DateTime time = TextUtils.ToDate(grvData.GetRowCellValue(e.RowHandle, colActualDeliveryDate).ToString());
            //DateTime time1 = TextUtils.ToDate(grvData.GetRowCellValue(e.RowHandle, colDeliveryRequestedDate).ToString());
            //DateTime timeMoney = TextUtils.ToDate(grvData.GetRowCellValue(e.RowHandle, colRecivedMoneyDate).ToString());

            //if (e.Column == colDeliveryRequestedDate)
            //{
            //    int c = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colColorDelivery));
            //    if (time == null)
            //    {
            //        switch (c)
            //        {
            //            case 0:
            //                e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
            //                break;
            //            case 1:
            //                e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
            //                break;
            //            case 2:
            //                e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
            //                break;
            //            case 3:
            //                e.Appearance.BackColor = Color.FromArgb(59, 89, 152);
            //                break;
            //            default:
            //                e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
            //                break;
            //        }

            //    }
            //    else
            //    {
            //        switch (c)
            //        {
            //            case 0:
            //                if (time1 < time)
            //                {
            //                    e.Appearance.BackColor = Color.Red;
            //                }
            //                else
            //                {
            //                    e.Appearance.BackColor = Color.Green;
            //                }
            //                break;
            //            case 1 :
            //                e.Appearance.BackColor = Color.Yellow;
            //                break;
            //            case 2:
            //                e.Appearance.BackColor = Color.Yellow;
            //                break;
            //            case 3:
            //                if (time1 < time)
            //                {
            //                    e.Appearance.BackColor = Color.Red;
            //                }
            //                else
            //                {
            //                    e.Appearance.BackColor = Color.Green;
            //                }
            //                break;
            //            default:
            //                e.Appearance.BackColor = Color.FromArgb(0, 255, 0);

            //                break;
            //        }
            //    }
            //}
            //if (e.Column == colPayDate)
            //{
            //    if (timeMoney == null)
            //    {
            //        int c = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colColorPay));
            //        switch (c)
            //        {
            //            case 0:
            //                e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
            //                break;
            //            case 1:
            //                e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
            //                break;
            //            case 2:
            //                e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
            //                break;
            //            default:
            //                e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
            //                break;
            //        }
            //    }
            //}

            
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
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void lịchSửTiềnVềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string project = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProject));
            frmHistoryMoney frm = new frmHistoryMoney();
            frm.project = project;
            frm.ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
                loadPOKHDetail();
            }
        }

        private void grvMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void cbvColor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(cbvColor.GetRowCellValue(e.RowHandle, colColor));
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
            frmViewPOKH frm = new frmViewPOKH(warehouseID);
            frm.ShowDialog();
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
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (MessageBox.Show("Bạn có muốn đổi trạng thái thành Đã xuất không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TextUtils.ExcuteSQL($"Update POKHDetail set IsExport= 1 where POKHID ={ID}");
                loadPOKHDetail();
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cHITIẾTTIỀNVỀToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewHistoryMoney frm = new frmViewHistoryMoney();
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
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

            if (id <= 0) return;

            frmPORequestBuyRTC frm = new frmPORequestBuyRTC();
            frm.id = id;
            frm.ShowDialog();
        }

       
    }
}



