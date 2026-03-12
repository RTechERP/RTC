using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMS.Utils;
using BMS.Business;
using BMS.Model;
using Forms.Employee.UserDetail;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections;
using DevExpress.XtraPrinting;

namespace BMS
{
    public partial class frmStaffManager : _Forms
    {
        private int _rownIndex = 0;
        public int check = 0;
        public frmStaffManager()
        {
            InitializeComponent();
        }

        private void frmStaffManager_Load(object sender, EventArgs e)
        {
            loadGrid();
            grvData.Appearance.FocusedCell.BackColor = Color.Yellow;
            grvData.Appearance.FocusedCell.BackColor2 = Color.Yellow;
        }

        void loadGrid()
        {
            //int checkAll = -1;
            //if (chkAll.Checked == true)
            //{
            //    checkAll = -1;
            //}
            //else
            //{
            //    checkAll = 0;
            //
            CheckStatusEmployee();
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { check });//spGetUser//spGetEmployee
            if (check == 0)
            {
                cboEmployeeStatus.Text = "Đang làm việc";
            }
            //DataTable dt = new DataTable();
            //if (chkAll.Checked == true)
            //{
            //   dt = TextUtils.Select("Select *,Case when Sex = 1 then N'Nam' else N'Nữ' end GioiTinh from Users WITH(NOLOCK)");
            //}
            //else
            //{
            //    dt = TextUtils.Select("Select *,Case when Sex = 1 then N'Nam' else N'Nữ' end GioiTinh from Users WITH(NOLOCK) where Status = 0");
            //}
            //grdData.DataSource = null;

            try
            {
                grdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //if (_rownIndex >= grvData.RowCount)
            //    _rownIndex = 0;
            //if (_rownIndex > 0)
            //    grvData.FocusedRowHandle = _rownIndex;
            //grvData.SelectRow(_rownIndex);
            //grvData.BestFitColumns();

            NhomPhong.DataSource = TextUtils.Select("SELECT * FROM UserGroup");
            NhomPhong.ValueMember = "ID";
            NhomPhong.DisplayMember = "Name";
        }
        void CheckStatusEmployee()
        {
            if (cboEmployeeStatus.Text.Contains("Tất cả"))
            {
                check = -1;
            }
            if (cboEmployeeStatus.Text.Contains("Đang làm việc"))
            {
                check = 0;
            }
            if (cboEmployeeStatus.Text.Contains("Nghỉ việc"))
            {
                check = 1;
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    grvData.OptionsPrint.AutoWidth = false;
            //    grvData.OptionsPrint.ExpandAllDetails = false;
            //    grvData.OptionsPrint.PrintDetails = true;
            //    grvData.OptionsPrint.UsePrintStyles = true;
            //    try
            //    {
            //        grvData.ExportToXls(sfd.FileName);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/DanhSachNhanVien_{DateTime.Now.ToString("ddMMyy")}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvData.ClearSelection();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmShowStaff frm = new frmShowStaff();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrid();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            _rownIndex = grvData.FocusedRowHandle;
            EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);
            frmShowStaff frm = new frmShowStaff();
            frm.Model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrid();
                grvData.FocusedRowHandle = row;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            string name = grvData.GetFocusedRowCellValue(colFullName).ToString();
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn chuyển trạng thái của nhân viên [" + name + "] thành Ngừng hoạt động?", TextUtils.Caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);
                if (model != null)
                {
                    model.Status = 1;//ngừng hoạt động
                    model.EndWorking = DateTime.Now;
                    EmployeeBO.Instance.Update(model);

                    //UsersModel user = (UsersModel)UsersBO.Instance.FindByPK(TextUtils.ToInt(model.UserID));
                    UsersModel user = SQLHelper<UsersModel>.FindByID(TextUtils.ToInt(model.UserID));
                    if (user != null)
                    {
                        user.Status = 1;
                        UsersBO.Instance.Update(user);
                    }
                }

                loadGrid();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }



        private void btnQLNhom_Click(object sender, EventArgs e)
        {
            frmStaffGroup frm = new frmStaffGroup();
            frm.Show();
        }


        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmImportExcel frmStaff = new frmImportExcel();
            frmStaff.ShowDialog();
            loadGrid();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colLeader)
            {
                if (TextUtils.ToInt(e.Value) == 1)
                {
                    e.DisplayText = "Leader";
                }
                else
                {
                    e.DisplayText = "Nhân viên";
                }
            }
        }


        private void btnLoginManager_Click(object sender, EventArgs e)
        {
            int IDEmlpoyee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (IDEmlpoyee == 0) return;
            _rownIndex = grvData.FocusedRowHandle;
            frmLoginManager frm = new frmLoginManager();
            frm.ID = IDEmlpoyee;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrid();
            }
        }

        private void btnNguoiThan_Click(object sender, EventArgs e)
        {
            //int IDEmlpoyee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (IDEmlpoyee == 0) return;
            //_rownIndex = grvData.FocusedRowHandle;
            //frmFamily frm = new frmFamily();
            //frm.IDEmlpoyee = IDEmlpoyee;
            //frm.ShowDialog();

        }

        private void quảnLýĐăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int IDEmlpoyee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (IDEmlpoyee == 0) return;
            //_rownIndex = grvData.FocusedRowHandle;
            //frmLoginManager frm = new frmLoginManager();
            //frm.ID = IDEmlpoyee;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadGrid();
            //}
        }
        private void ngườiThânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int IDEmlpoyee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (IDEmlpoyee == 0) return;
            //_rownIndex = grvData.FocusedRowHandle;
            //frmFamily frm = new frmFamily();
            //frm.IDEmlpoyee = IDEmlpoyee;
            //frm.ShowDialog();
        }

        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (id == 0) return;

            //_rownIndex = grvData.FocusedRowHandle;

            ////UsersModel model = (UsersModel)UsersBO.Instance.FindByPK(id);
            //EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);

            //frmShowStaff frm = new frmShowStaff();
            ////ucUserContract UCUserContract = new ucUserContract();
            //frm.Model = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadGrid();
            //}
        }

        private void xoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (id == 0) return;
            //string name = grvData.GetFocusedRowCellValue(colFullName).ToString();
            //DialogResult result = MessageBox.Show("Bạn có thực sự muốn chuyển trạng thái của nhân viên [" + name + "]?", TextUtils.Caption,
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    UsersModel model = (UsersModel)UsersBO.Instance.FindByPK(id);
            //    model.Status = 1;//ngừng hoạt động
            //    UsersBO.Instance.Update(model);
            //    loadGrid();
            //}
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(null, null);
        }

        private void btnManagerLoginItem_Click(object sender, EventArgs e)
        {
            btnLoginManager_Click(null, null);
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void cboEmployeeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void grvData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridView view = sender as GridView;
            //GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            //string caption = info.Column.Caption;
            //if (info.Column.Caption == string.Empty)
            //    caption = info.Column.ToString();
            //info.GroupText = string.Format("{0} : {1} ({2})", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }

        private void grvData_CustomColumnGroup(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            //if (e.Column == colDepartmentName)
            //{
            //    int x = TextUtils.ToInt(e.Value1);
            //    int y = TextUtils.ToInt(e.Value1);
            //    int res = Comparer.Default.Compare(x, y);
            //    if (x > 14 && y > 14) res = 0;
            //    e.Result = res;
            //    e.Handled = true;
            //}
        }

        private void btnSetupIsApprove_Click(object sender, EventArgs e)
        {
            int employeeId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int code = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCode));
            int fullName = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colFullName));
        }

        private void btnEmployeeApprove_Click(object sender, EventArgs e)
        {
            frmEmployeeApprove frm = new frmEmployeeApprove();
            frm.ShowDialog();
        }



        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = System.Drawing.Color.LightGray;
                e.HighPriority = true;
            }

            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
                if (status == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
                    e.Appearance.ForeColor = Color.Black;
                    e.HighPriority = true;
                }
            }
        }

        private void grvData_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            //if (e.RowHandle >= 0)
            //{
            //    if (e.Column == colCode || e.Column == colFullName)
            //    {
            //        int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
            //        if (status == 1)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
            //            e.Appearance.ForeColor = Color.Black;
            //            //e.HighPriority = true;
            //        }
            //    }
            //}
        }

        private void btnEmployeeContract_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;

            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (employeeID <= 0)
            {
                return;
            }

            frmEmployeeContract frm = new frmEmployeeContract();
            frm.employeeID = employeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrid();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    int val1 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentSTT"));
                    int val2 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentSTT"));

                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvData_DoubleClick_1(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
