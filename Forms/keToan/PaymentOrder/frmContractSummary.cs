using BMS.Model;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BMS
{
    public partial class frmContractSummary : _Forms
    {
        public frmContractSummary()
        {
            InitializeComponent();
        }

        private void frmContractSummary_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(+1).AddSeconds(-1);
            cboContract.EditValue = 0;
            txtFilterText.Text = "";

            LoadContract();
            LoadData();
        }

        void LoadData()
        {
            try
            {
                DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
                int contractID = TextUtils.ToInt(cboContract.EditValue);
                string filterText = txtFilterText.Text.Trim();

                DataTable dt = TextUtils.LoadDataFromSP("spGetContractWithPaymentOrder", "A",
                                                         new string[] { "@DateStart", "@DateEnd", "@RegisterContractID", "@KeyWords", "@EmployeeID" },
                                                         new object[] { dateStart, dateEnd, contractID, filterText, Global.EmployeeID });
                grdData.DataSource = dt;
            }
            catch (Exception)
            {

            }

        }

        void LoadContract()
        {
            List<RegisterContractModel> contracts = SQLHelper<RegisterContractModel>.FindAll().Where(x => x.IsDeleted == false).ToList();
            cboContract.Properties.DataSource = contracts;
            cboContract.Properties.DisplayMember = "DocumentName";
            cboContract.Properties.ValueMember = "ID";
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
            if (status == 1)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (status == 2)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnApprovedContract_Click(object sender, EventArgs e)
        {
            int[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Count() <= 0)
            {
                MessageBox.Show("Vui lòng chọn hợp đồng cần duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.
                Show($"Bạn có chắc chắn muốn duyệt hợp đồng đã chọn không?", "Thông báo", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

            foreach (int rowHandle in selectedRowHandles)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
                ApproveContract(id, 1, "");
            }

            LoadData();
        }

        private void btnCancleContractApproved_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selectedRowHandles = grvData.GetSelectedRows();
                if (selectedRowHandles.Count() <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần hủy duyệt!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.
                Show($"Bạn có chắc chắn muốn hủy duyệt hợp đồng đã chọn không?", "Thông báo", MessageBoxButtons.YesNo)
                == DialogResult.No) return;

                ToolStripButton item = (ToolStripButton)sender;

                flyoutPanel1.Tag = item.Tag;
                flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual;

                int x = (Screen.PrimaryScreen.Bounds.Width / 2) - (flyoutPanel1.Width / 2);
                int y = (Screen.PrimaryScreen.Bounds.Height / 2) - (flyoutPanel1.Height / 2) - 300;
                flyoutPanel1.Options.Location = new System.Drawing.Point(x, y);
                flyoutPanel1.OwnerControl = this;
                flyoutPanel1.ShowPopup();
            }
            catch (Exception)
            {

            }

        }

        void ApproveContract(int contractId, int status, string reasonCancle)
        {
            RegisterContractModel contract = SQLHelper<RegisterContractModel>.FindByID(contractId);
            if (contract != null)
            {
                contract.Status = status;
                contract.ReasonCancel = reasonCancle;
                SQLHelper<RegisterContractModel>.Update(contract);
                
            }
        }

        private void cboContract_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void flyoutPanel1_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "btnOK":
                    string reasonCancle = TextUtils.ToString(txtReasonCancle.Text.Trim());
                    if (String.IsNullOrWhiteSpace(reasonCancle))
                    {
                        MessageBox.Show($"Vui lòng nhập lý do hủy duyệt!", "Thông báo");
                        break;
                    }
                    int[] selectedRowHandles = grvData.GetSelectedRows();
                    foreach (int rowHandle in selectedRowHandles)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
                        ApproveContract(id, 2, reasonCancle);
                    }
                    txtReasonCancle.Text = "";
                    (sender as FlyoutPanel).HidePopup();
                    LoadData();
                    break;
                case "btnCancel":
                    txtReasonCancle.Text = "";
                    (sender as FlyoutPanel).HidePopup();
                    break;
            }
        }
    }
}
