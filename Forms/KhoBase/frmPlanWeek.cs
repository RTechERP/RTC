using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Forms.Classes;
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
    public partial class frmPlanWeek : _Forms
    {
        public frmPlanWeek()
        {
            InitializeComponent();
        }

        private void frmWeekReport_Load(object sender, EventArgs e)
        {
            //GetDayOfWeek();
            dtpDateStart.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            dtpDateEnd.Value = dtpDateStart.Value.AddDays(6);

            loadDepartment();
            loadUser();
            LoadGroupSales();
            loadPlanWeek();
        }

        

        void loadPlanWeek()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int userID = TextUtils.ToInt(cbUser.EditValue);
            int groupSaleId = TextUtils.ToInt(treeListLookUpEdit1.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetPlanWeek", "A",
                                        new string[] { "@DateStart", "@DateEnd", "@Department", "@UserID", "@GroupSaleID" },
                                        new object[] { dateStart, dateEnd, departmentID, userID, groupSaleId });

            AddColumn();
            grdData.DataSource = dt;
        }

        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;
            cboDepartment.EditValue = Global.DepartmentID;
        }

        void loadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cbUser.Properties.ValueMember = "UserID";
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.DataSource = dt;
            //cbUser.EditValue = Global.UserID;

            bool isAdmin = (!Global.IsAdmin && !Global.IsAdminSale && Global.UserID != 1177 && Global.UserID != 1313 && Global.UserID != 23 && Global.UserID != 1380);
            if (isAdmin)
            {
                cbUser.EditValue = Global.UserID;
                cbUser.Enabled = false;
            }
        }

        void LoadGroupSales()
        {
            //List<GroupSalesModel> list = SQLHelper<GroupSalesModel>.FindAll();
            //cboGroupSale.Properties.ValueMember = "ID";
            //cboGroupSale.Properties.DisplayMember = "GroupSalesName";
            //cboGroupSale.Properties.DataSource = list;

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeManager","A", new string[] { "@group" }, new object[] { 0 });
            treeListLookUpEdit1.Properties.ValueMember = "ID";
            treeListLookUpEdit1.Properties.DisplayMember = "FullName";
            treeListLookUpEdit1.Properties.DataSource = dt;
            treeListLookUpEdit1.Properties.AutoExpandAllNodes = true;


            var row = dt.AsEnumerable().Where(x => x.Field<int>("ParentID") == Global.UserID).Select(x => x.Field<int>("ID")).ToList();
            treeListLookUpEdit1.EditValue = TextUtils.ToInt(row.FirstOrDefault());
        }

        void GetDayOfWeek()
        {
            string date = DateTime.Now.DayOfWeek.ToString();
            switch (date)
            {
                case cConsts.Monday:
                    dtpDateStart.Value = DateTime.Now;
                    dtpDateEnd.Value = DateTime.Now.AddDays(6);
                    break;
                case cConsts.Tuesday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-1);
                    dtpDateEnd.Value = DateTime.Now.AddDays(5);
                    break;
                case cConsts.Wednesday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-2);
                    dtpDateEnd.Value = DateTime.Now.AddDays(4);
                    break;
                case cConsts.Thursday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-3);
                    dtpDateEnd.Value = DateTime.Now.AddDays(3);
                    break;
                case cConsts.Friday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-4);
                    dtpDateEnd.Value = DateTime.Now.AddDays(2);
                    break;
                case cConsts.Saturday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-5);
                    dtpDateEnd.Value = DateTime.Now.AddDays(1);
                    break;
                case cConsts.Sunday:
                    dtpDateStart.Value = DateTime.Now.AddDays(-6);
                    dtpDateEnd.Value = DateTime.Now;
                    break;
                default:
                    break;
            }
            dtpDateStart.Value = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            dtpDateEnd.Value = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPlanWeekDetail frm = new frmPlanWeekDetail();
            frm.dateStart = dtpDateStart.Value;
            frm.userID = Global.UserID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPlanWeek();
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            //int UserID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));
            //DataTable dt = (DataTable)grdData.DataSource;
            //DataRow[] dtr = dt.Select($"UserID ={UserID}");
            int rowhandle = grvData.FocusedRowHandle;
            GridColumn col = grvData.FocusedColumn;
            if (col == null)
            {
                return;
            }
            int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("UserID"));
            DateTime? date = TextUtils.ToDate4(col.FieldName);

            if (!date.HasValue)
            {
                return;
            }
            DateTime dateStart = date.Value.AddDays(-(int)date.Value.DayOfWeek + (int)DayOfWeek.Monday);
            frmPlanWeekDetail frm = new frmPlanWeekDetail();
            frm.userID = userID;
            frm.dateStart = dateStart;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPlanWeek();
                grvData.FocusedRowHandle = rowhandle;
                grvData.FocusedColumn = col;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateStart.Value.AddDays(7);
            dtpDateEnd.Value = dtpDateEnd.Value.AddDays(7);
            loadPlanWeek();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateStart.Value.AddDays(-7);
            dtpDateEnd.Value = dtpDateEnd.Value.AddDays(-7);
            loadPlanWeek();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPlanWeek();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmPlanWeekExcel frm = new frmPlanWeekExcel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPlanWeek();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            grvData.OptionsPrint.AutoWidth = false;
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"KeHoachTuan_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

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


        void AddColumn()
        {
            grvData.Columns.Clear();
            DateTime dateStart = dtpDateStart.Value;
            DateTime dateEnd = dtpDateEnd.Value;

            //Add column default
            GridColumn colCode = new GridColumn();
            colCode.Visible = true;
            colCode.Caption = "Mã nhân viên";
            colCode.FieldName = "Code";
            colCode.ColumnEdit = repositoryItemMemoEdit2;
            colCode.Fixed = FixedStyle.Left;
            colCode.Width = 150;
            colCode.OptionsColumn.AllowSort = DefaultBoolean.False;

            GridColumn colFullName = new GridColumn();
            colFullName.Visible = true;
            colFullName.Caption = "Họ tên";
            colFullName.FieldName = "FullName";
            colFullName.ColumnEdit = repositoryItemMemoEdit2;
            colFullName.Fixed = FixedStyle.Left;
            colFullName.Width = 200;

            grvData.Columns.Add(colCode);
            grvData.Columns.Add(colFullName);
            grvData.Columns.AddField("UserID");


            while (dateStart <= dateEnd)
            {
                GridColumn col = new GridColumn();
                col.Visible = true;
                col.Caption = dateStart.ToString("dd/MM/yyyy");
                col.FieldName = dateStart.ToString("yyyy-MM-dd");
                col.ColumnEdit = repositoryItemMemoEdit2;
                col.Width = 150;
                col.OptionsFilter.AllowFilter = false;
                col.OptionsColumn.AllowSort = DefaultBoolean.False;
                col.BestFit();
                grvData.Columns.Add(col);

                dateStart = dateStart.AddDays(+1);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPlanWeek();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadPlanWeek();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("UserID"));
            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue("FullName"));

            
            if (Global.UserID != userID && !Global.IsAdmin)
            {
                MessageBox.Show($"Bạn không thể xoá kế hoạch của nhân viên [{fullName}]!", "Thông báo");
                return;
            }

            GridColumn column = grvData.FocusedColumn;
            if (column == null)
            {
                return;
            }


            DateTime? date = TextUtils.ToDate4(column.FieldName);
            if (!date.HasValue)
            {
                return;
            }

            var exp1 = new Expression("UserID", userID);
            var exp2 = new Expression("DatePlan", date.Value.ToString("yyyy-MM-dd"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá kế hoạch ngày [{date.Value.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                WeekPlanBO.Instance.DeleteByExpression(exp1.And(exp2));
                loadPlanWeek();
                grvData.FocusedRowHandle = rowhandle;
                grvData.FocusedColumn = column;
            }
            
        }

        private void cboGroupSale_EditValueChanged(object sender, EventArgs e)
        {
            loadPlanWeek();
        }

        private void treeListLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            loadPlanWeek();
        }
    }
}
