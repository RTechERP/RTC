using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmEmployeeProtectiveGear : _Forms
    {
        public frmEmployeeProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmProductGearEmployee_Load(object sender, EventArgs e)
        {
            this.Text += $" - {this.Tag}";
            DateTime date = DateTime.Now.AddMonths(-1);
            dtpFromDate.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            cboStatus.Properties.Items[1].CheckState = CheckState.Checked;

            LoadEmployee();
            LoadData();
        }

        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeTeamAndDepartment", "A", new string[] { }, new object[] { });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "UserID";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.UserID;
        }

        private void LoadData()
        {
            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            string status = TextUtils.ToString(cboStatus.EditValue);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable dtHistoryProduct = TextUtils.LoadDataFromSP("spGetHistoryProduct_New", "A",
                    new string[] { "@DateStart", "@DateEnd", "@Keyword", "@Status", "@WarehouseID", "@PageNumber", "@PageSize", "@UserID" },
                    new object[] { ds, de, txtFilterText.Text.Trim(), status, 5, 1, 99999999, userID });
                grdData.DataSource = dtHistoryProduct;
                if (TextUtils.ToInt(status) == 2)
                {
                    colNumberBorrow.Caption = "Số lượng mất";
                    colPeople.Caption = "Người làm mất";
                    colDate1.Caption = "Ngày làm mất";
                }
                else
                {
                    if (TextUtils.ToInt(status) == 3)
                    {
                        colNumberBorrow.Caption = "Số lượng hỏng";
                        colPeople.Caption = "Người làm hỏng";
                        colDate1.Caption = "Ngày xác nhận";
                    }
                    else
                    {
                        colNumberBorrow.Caption = "Số lượng mượn";
                        colPeople.Caption = "Người mượn";
                        colDate1.Caption = "Ngày mượn";
                    }
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn gia hạn sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) continue;
                    HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(id);
                    if (model.PeopleID != Global.UserID) continue;
                    bool isValid = model.Status == 1 || model.Status == 4 || model.Status == 7 || model.Status == 8;
                    if (isValid)
                    {
                        model.DateReturnExpected = dtpNgayGiaHan.Value;
                        model.Status = 8;
                        SQLHelper<HistoryProductRTCModel>.Update(model);
                    }
                }
            }
            LoadData();
        }

        private void btnBorrow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHistoryProductRTCDetailProtectiveGear frm = new frmHistoryProductRTCDetailProtectiveGear();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                Int32[] selectedRowHandles = grvData.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                    if (selectedRowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                        if (id == 0) continue;
                        HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(id);
                        if (model.PeopleID != Global.UserID) continue;
                        bool isValid = model.Status == 1 || model.Status == 4 || model.Status == 7 || model.Status == 8;
                        if (isValid)
                        {
                            model.Status = 4;
                            SQLHelper<HistoryProductRTCModel>.Update(model);
                        }
                    }
                }
                LoadData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colDualDate)) == 1)
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }
    }
}
