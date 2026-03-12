using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraReports.Design;

namespace BMS
{
    public partial class frmFoodOrder : _Forms
    {
        ArrayList lstIDDelete = new ArrayList();
        public frmFoodOrder()
        {
            InitializeComponent();
        }

        private void frmFoodOrder_Load(object sender, EventArgs e)
        {
            loadData();
            txtPageNumber.Text = "1";

        }
        void loadData()
        {

            grdData.DataSource = null;
            gridControl1.DataSource = null;

            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);

            DataSet dts = TextUtils.LoadDataSetFromSP("spGetFoodOrder", 
                new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@EmployeeID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text, 0 });


            var dtLocation1 = dts.Tables[0].Select("Location = 1 OR Location IS NULL");
            var dtLocation2 = dts.Tables[0].Select("Location = 2");


            if (dtLocation1.Length > 0)
            {
                grdData.DataSource = dtLocation1.CopyToDataTable();
            }

            if (dtLocation2.Length > 0)
            {
                gridControl1.DataSource = dtLocation2.CopyToDataTable(); ;
            }

            //grdData.DataSource = dts.Tables[0];
            //if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);

        }

        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {

            int location = xtraTabControl1.SelectedTabPageIndex + 1;
            frmFoodOrderDetail frm = new frmFoodOrderDetail(location);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }


        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có chắc muốn duyệt danh sách cơm ca không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    grvData.FocusedRowHandle = -1;
            //    for (int i = 0; i < grvData.RowCount; i++)
            //    {
            //        grvData.SetRowCellValue(i, colIsApproved, true);
            //    }

            //    btnSave_Click(null, null);
            //}

            Approved(true);
        }

        bool save()
        {

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                EmployeeFoodOrderModel m = (EmployeeFoodOrderModel)(EmployeeFoodOrderBO.Instance.FindByPK(ID));
                m.IsApproved = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsApproved));
                EmployeeFoodOrderBO.Instance.Update(m);
            }
            if (lstIDDelete.Count > 0)
                EmployeeFoodOrderBO.Instance.Delete(lstIDDelete);

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            if (save())
                this.DialogResult = DialogResult.OK;

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {

            txtPageNumber.Text = "1";

            loadData();
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {

            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));

            //if (!isApproved)
            //{
            //    MessageBox.Show($"Nhân viên [{code} - {fullName}] chưa được duyệt!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    if (MessageBox.Show($"Bạn có chắc muốn hủy duyệt nhân viên [{code} - {fullName}] không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        EmployeeFoodOrderModel foodOrderModel = (EmployeeFoodOrderModel)(EmployeeFoodOrderBO.Instance.FindByPK(ID));
            //        foodOrderModel.IsApproved = false;
            //        EmployeeFoodOrderBO.Instance.Update(foodOrderModel);
            //        grvData.SetFocusedRowCellValue(colIsApproved, false);
            //    }

            //}

            Approved(false);

        }

        void Approved(bool isApproved)
        {

            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            //int[] RowIndex = grvData.GetSelectedRows();
            int[] RowIndex = gridView.GetSelectedRows();
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";

            List<int> listID = new List<int>();

            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} danh sách đặt cơm không ?", approved), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < RowIndex.Length; i++)
                {
                    listID.Add(TextUtils.ToInt(gridView.GetRowCellValue(RowIndex[i], colID.FieldName)));
                }

                TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                    new string[] { "@TableName", "@FieldName", "@Value", "@ID" },
                    new object[] { "EmployeeFoodOrder", "IsApproved", isApproved ? 1 : 0, string.Join(",", listID) });
                loadData();
            }
        }



        private void grvData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column == colStatus)
            //{
            //    string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //    if (TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus)) == false)
            //    {
            //        if (MessageBox.Show(string.Format($"Bạn có chắc muốn duyệt nhân viên có mã {code}  không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            if (grdData.DataSource == null)
            //                return;
            //            grvData.SetFocusedRowCellValue(colStatus, true);
            //            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //            EmployeeFoodOrderModel orderModel = (EmployeeFoodOrderModel)EmployeeFoodOrderBO.Instance.FindByPK(id);
            //            orderModel.IsApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus));
            //            EmployeeFoodOrderBO.Instance.Update(orderModel);

            //        }

            //        grvData.FocusedRowHandle = -1;
            //    }
            //    else
            //    {
            //        if (MessageBox.Show(string.Format($"Bạn có chắc muốn huỷ duyệt nhân viên có mã {code}  không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            if (grdData.DataSource == null)
            //                return;
            //            grvData.SetFocusedRowCellValue(colStatus, false);
            //            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //            EmployeeFoodOrderModel orderModel = (EmployeeFoodOrderModel)EmployeeFoodOrderBO.Instance.FindByPK(id);
            //            orderModel.IsApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus));
            //            EmployeeFoodOrderBO.Instance.Update(orderModel);

            //        }
            //        grvData.FocusedRowHandle = -1;
            //    }
            //}
            // grvData.FocusedColumn = colID;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            //sfd.FileName = $"DanhSachDatCom_{dtpStart.Value.ToString("ddMMyy")}";

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    grvData.OptionsPrint.AutoWidth = false;
            //    grvData.OptionsPrint.ExpandAllDetails = false;
            //    grvData.OptionsPrint.PrintDetails = true;
            //    grvData.OptionsPrint.UsePrintStyles = true;
            //    try
            //    {
            //        grvData.ExportToXls(sfd.FileName, optionsEx);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}


            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachDatCom_{dtpStart.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        foreach (XtraTabPage item in xtraTabControl1.TabPages)
                        {
                            if (item.Controls.Count <= 0) continue;
                            GridControl gridControl = (GridControl)item.Controls[0];

                            if (gridControl == null) continue;

                            //LoadData(gridControl);

                            PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);
                            printableComponentLink.Component = gridControl;

                            compositeLink.Links.Add(printableComponentLink);
                        }
                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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
        }

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            try
            {
                e.SheetName = xtraTabControl1.TabPages[e.Index].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        private void btnComCa_Click(object sender, EventArgs e)
        {
            //frmEmployeeFoodOrder frm = new frmEmployeeFoodOrder();
            frmSummaryFoodOrder frm = new frmSummaryFoodOrder();
            frm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (grdData.DataSource == null)
            //    return;


            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            //int[] RowIndex = grvData.GetSelectedRows();
            int[] RowIndex = gridView.GetSelectedRows();

            if (RowIndex.Length > 0)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    List<int> ids = new List<int>();

                    foreach (int row in RowIndex)
                    {
                        int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                        if (id <= 0) continue;

                        string code = TextUtils.ToString(gridView.GetRowCellValue(row, colCode.FieldName));
                        string fullName = TextUtils.ToString(gridView.GetRowCellValue(row, colFullname.FieldName));
                        bool isApproved = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApproved.FieldName));

                        if (isApproved)
                        {
                            MessageBox.Show($"Nhân viên [{code} - {fullName}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!", "Thông báo");
                            return;
                        }


                        ids.Add(id);
                    }

                    if (ids.Count <= 0) return;

                    var myDict = new Dictionary<string, object>()
                    {
                        {"IsDeleted",true },
                        {"UpdatedDate",DateTime.Now },
                        {"UpdatedBy",Global.AppCodeName },
                    };
                    var exp = new Expression("ID", string.Join(",", ids), "IN");
                    SQLHelper<EmployeeFoodOrderModel>.UpdateFields(myDict, exp);
                    loadData();
                }
            }


            //if (TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved)) == true)
            //{
            //    MessageBox.Show(string.Format($"Nhân viên [{code} - {fullName}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        EmployeeFoodOrderBO.Instance.Delete(strID);
            //        grvData.DeleteSelectedRows();
            //        if (strID > 0)
            //        {
            //            lstIDDelete.Add(strID);
            //        }

            //    }
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            string code = TextUtils.ToString(gridView.GetFocusedRowCellValue(colCode.FieldName));
            string fullName = TextUtils.ToString(gridView.GetFocusedRowCellValue(colFullname.FieldName));
            if (TextUtils.ToBoolean(gridView.GetFocusedRowCellValue(colIsApproved.FieldName)) == true)
            {
                MessageBox.Show(string.Format($"Nhân viên [{code} - {fullName}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int strID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID.FieldName));
                //EmployeeFoodOrderModel orderModel = (EmployeeFoodOrderModel)EmployeeFoodOrderBO.Instance.FindByPK(strID);
                EmployeeFoodOrderModel orderModel = SQLHelper<EmployeeFoodOrderModel>.FindByID(strID);
                frmFoodOrderDetail frm = new frmFoodOrderDetail(orderModel.Location);
                frm.model = orderModel;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData();
                }
            }
        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
