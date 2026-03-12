using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTSAssetManagementHistory : _Forms
    {
        public frmTSAssetManagementHistory()
        {
            InitializeComponent();
        }

        private void frmTSAssetManagementHistory_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            dtpFromDate.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            cboStatus.Properties.Items[0].CheckState = CheckState.Checked;

            LoadEmployee();
            LoadData();
        }
        private void LoadData()
        {
            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            string status = TextUtils.ToString(cboStatus.EditValue);

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            //Update lại trạng thái
            foreach (var item in SQLHelper<TSAssetManagementHistoryModel>.FindAll())
            {
                if ((item.Status == 1 || item.Status == 6) && item.DateExpectedReturn <= DateTime.Now && item.DateExpectedReturn >= DateTime.Now.AddDays(-1))
                {
                    item.Status = 5;//Sắp hết hạn
                    TSAssetManagementHistoryBO.Instance.Update(item);
                }
                else if ((item.Status == 1 || item.Status == 5) && item.DateExpectedReturn < DateTime.Now.AddDays(-1))
                {
                    item.Status = 6;//Quá hạn
                    TSAssetManagementHistoryBO.Instance.Update(item);
                }
                else if ((item.Status == 5 || item.Status == 6) && item.DateExpectedReturn > DateTime.Now)
                {
                    item.Status = 1;//Đang mượn
                    TSAssetManagementHistoryBO.Instance.Update(item);
                }
            }

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetManagementHistory", "A",
                    new string[] { "@DateStart", "@DateEnd", "@Keyword", "@EmployeeID", "@Status", "@PageNumber", "@PageSize" },
                    new object[] { ds, de, txtFilterText.Text.Trim(), employeeID, status, pageNumber, pageSize });

                grdData.DataSource = dt;
                if (TextUtils.ToInt(status) == 3)
                {
                    colNumberBorrow.Caption = "Số lượng mất";
                    colEmployee.Caption = "Người làm mất";
                    colReason.Caption = "Lý do mất";
                }
                else
                {
                    if (TextUtils.ToInt(status) == 4)
                    {
                        colNumberBorrow.Caption = "Số lượng hỏng";
                        colEmployee.Caption = "Người làm hỏng";
                        colReason.Caption = "Lý do hỏng";
                    }
                    else
                    {
                        colNumberBorrow.Caption = "Số lượng mượn";
                        colEmployee.Caption = "Người mượn";
                        colReason.Caption = "Lý do mượn";
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    txtTotalPage.Text = (dt.Rows[0]["TotalPage"]).ToString();
                }               
            }
        }
        private void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboEmployee.Properties.DataSource = dataSet.Tables[0];
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmTSAssetManagementHistoryDetail frm = new frmTSAssetManagementHistoryDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả tài sản", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    TSAssetManagementHistoryModel model = SQLHelper<TSAssetManagementHistoryModel>.FindByID(id);
                    model.Status = 2;
                    model.DateActualReturn = DateTime.Now;
                    TSAssetManagementHistoryBO.Instance.Update(model);
                }
            }
            LoadData();
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn duyệt", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    TSAssetManagementHistoryModel model = SQLHelper<TSAssetManagementHistoryModel>.FindByID(id);
                    model.IsApproved = true;
                    TSAssetManagementHistoryBO.Instance.Update(model);
                }
            }
            LoadData();
        }

        private void btnLose_Click(object sender, EventArgs e)
        {
            if (txtReason.Text.Trim() == "")
            {
                MessageBox.Show("Nhập lý do mất tài sản");
                return;
            }
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn báo mất tài sản", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    TSAssetManagementHistoryModel model = SQLHelper<TSAssetManagementHistoryModel>.FindByID(id);
                    model.Status = 3;
                    model.Reason = txtReason.Text;
                    model.IsApproved = false;
                    TSAssetManagementHistoryBO.Instance.Update(model);

                    string code = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colTSAssetCode));
                    var lst = SQLHelper<TSAssetManagementModel>.FindByAttribute("TSAssetCode", code).OrderByDescending(p => p.ID).ToList();
                    int numberBorrow = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberBorrow));
                    for (int y = 0; y < numberBorrow; y++)
                    {
                        TSAssetManagementModel tsAssetManagement = lst[y];
                        tsAssetManagement.StatusID = 4;
                        TSAssetManagementBO.Instance.Update(tsAssetManagement);
                    }
                }
                txtReason.Text = "";
                LoadData();
            }
        }

        private void btnBroken_Click(object sender, EventArgs e)
        {
            if (txtReason.Text.Trim() == "")
            {
                MessageBox.Show("Nhập lý do hỏng tài sản");
                return;
            }
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn báo hỏng tài sản", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    TSAssetManagementHistoryModel model = SQLHelper<TSAssetManagementHistoryModel>.FindByID(id);
                    model.Status = 4;
                    model.Reason = txtReason.Text;
                    model.IsApproved = false;
                    TSAssetManagementHistoryBO.Instance.Update(model);

                    string code = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colTSAssetCode));
                    var lst = SQLHelper<TSAssetManagementModel>.FindByAttribute("TSAssetCode", code).OrderByDescending(p => p.ID).ToList();
                    int numberBorrow = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberBorrow));
                    for (int y = 0; y < numberBorrow; y++)
                    {
                        TSAssetManagementModel tsAssetManagement = lst[y];
                        tsAssetManagement.StatusID = 5;
                        TSAssetManagementBO.Instance.Update(tsAssetManagement);
                    }
                }
            }
            txtReason.Text = "";
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            List<TSAssetManagementHistoryModel> list = new List<TSAssetManagementHistoryModel>();
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    string date = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colDateReturn));
                    if (id == 0 || date.Trim().Length > 0) continue;
                    TSAssetManagementHistoryModel model = (TSAssetManagementHistoryModel)TSAssetManagementHistoryBO.Instance.FindByPK(id);
                    list.Add(model);
                }
            }
            if (list.Count <= 0) return;
            frmEditEmployee frm = new frmEditEmployee();
            frm.TSAssetCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTSAssetCode));
            frm.TSAssetName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTSAssetName));
            frm.EmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            frm.lst = list;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnHistoryLog_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }
            frmTSAssetManagementHistoryLog frm = new frmTSAssetManagementHistoryLog();
            frm.historyID = id;
            frm.Show();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    TSAssetManagementHistoryModel model = (TSAssetManagementHistoryModel)TSAssetManagementHistoryBO.Instance.FindByPK(id);
                    model.DateExpectedReturn = dtpNgayGiaHan.Value;
                    TSAssetManagementHistoryBO.Instance.Update(model);

                    //Update lịch sử gia hạn
                    TSAssetManagementHistoryLogModel logModel = new TSAssetManagementHistoryLogModel();
                    logModel.TSAssetManagementHistoryID = id;
                    logModel.DateExpectedReturn = dtpNgayGiaHan.Value;
                    SQLHelper<TSAssetManagementHistoryLogModel>.Insert(logModel);
                }
            }
            LoadData();
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));

            if (status == 1 || status == 2) return;

            if (e.Column == colStatusText)
            {
                if (status == 3)//Mất
                {
                    e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
                    e.Appearance.ForeColor = Color.White;
                }

                if (status == 4)//Hỏng
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
                    e.Appearance.ForeColor = Color.Black;
                }
            }

            if (e.Column == colEmployee)
            {
                if (status == 5)// Sắp hết hạn
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
                    e.Appearance.ForeColor = Color.Black;
                }

                if (status == 6)// Quá hạn
                {
                    e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
    }
}