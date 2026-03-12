using BMS.Business;
using BMS.Model;
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
    public partial class frmTSAssetManagementHistoryDetail : _Forms
    {
        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        public frmTSAssetManagementHistoryDetail()
        {
            InitializeComponent();
        }

        private void frmTSAssetManagementHistoryDetail_Load(object sender, EventArgs e)
        {
            LoadDataBorrow();
            LoadEmployee();
            LoadListAsset();
        }
        private void LoadListAsset()
        {
            _dtList = TextUtils.LoadDataFromSP("spGetTSAssetManagement", "A",
                                new string[] { "@Keyword" },
                                new object[] { txtKeyword.Text.Trim() });
            grdData.DataSource = _dtList;
        }
        private void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployee", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = dataSet.Tables[0];
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }
        private void LoadDataBorrow()
        {
            _dtB = TextUtils.Select("SELECT top 1 * FROM TSAssetManagement where id = 0");
            _dtB.Columns.Add("Quantity");
            grdData2.DataSource = _dtB;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadListAsset();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int quantity = 1;
            int[] lstIndex = grvData.GetSelectedRows();

            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], colID));
                if (id == 0) continue;
                for (int y = 0; y < grvData2.RowCount; y++)
                {
                    string code = TextUtils.ToString(grvData2.GetRowCellValue(y, colACode));
                    int id2 = TextUtils.ToInt(grvData2.GetRowCellValue(y, colID2));
                    if (id == id2)
                    {
                        quantity++;
                        MessageBox.Show($"Tài sản với mã [{code}] đã được thêm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (quantity == 1)
                {
                    DataRow[] rs = _dtList.Select("ID = " + id);
                    rs[0]["Quantity"] = quantity;
                    _dtB.ImportRow(rs[0]);
                    //grvData.DeleteSelectedRows();
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int[] lstIndex = grvData2.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(lstIndex[i], colID2));
                if (id == 0) continue;
                DataRow[] rs = _dtB.Select("ID = " + id);
                _dtList.ImportRow(rs[0]);
            }
            grvData2.DeleteSelectedRows();
        }

        private void grvData2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvData2.FocusedColumn == colNumberBorrow)
            {
                int id = TextUtils.ToInt(grvData2.GetFocusedRowCellValue(colID2));
                string code = TextUtils.ToString(grvData2.GetFocusedRowCellValue(colACode));

                DataRow[] rs = _dtList.Select("ID = " + id);
                var q1 = TextUtils.ToDecimal(rs[0]["InventoryReal"]);
                var q2 = TextUtils.ToDecimal(e.Value.ToString());
                if (q1 < q2)
                {
                    grvData2.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = $"Số lượng mượn của tài sản với mã [{code}] không được vượt quá số lượng hiện có là [{q1}]!";
                    grvData2.EndUpdate();
                }
            }
        }
        bool ValidateForm()
        {
            if (grvData2.RowCount <= 0)
            {
                MessageBox.Show("Chưa chọn tài sản");
                return false;
            }
            if (cboEmployee.Text.Trim() == "")
            {
                MessageBox.Show("Chọn tên người mượn");
                return false;
            }
            if (dtpReturn.Value.Date < DateTime.Now.Date || dtpReturn.Value.Date < dtpBorrowDate.Value.Date)
            {
                MessageBox.Show("Ngày dự kiến trả không phù hợp! Ngày dự kiến trả phải lớn hơn ngày mượn hoặc thời gian hiện tại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                if (!ValidateForm()) return;

                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn thêm ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialog == DialogResult.Cancel)
                {
                    return;
                }

                for (int i = 0; i < grvData2.RowCount; i++)
                {                    
                    TSAssetManagementHistoryModel historyModel = new TSAssetManagementHistoryModel();
                    historyModel.TSAssetManagementID = TextUtils.ToInt(grvData2.GetRowCellValue(i, colID2));
                    historyModel.DateBorrow = TextUtils.ToDate(dtpBorrowDate.Value.ToString("yyyy/MM/dd HH:mm"));
                    historyModel.DateExpectedReturn = TextUtils.ToDate(dtpReturn.Value.ToString("yyyy/MM/dd HH:mm"));
                    historyModel.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                    historyModel.Reason = TextUtils.ToString(txtReason.Text);
                    historyModel.Note = TextUtils.ToString(txtNote.Text);
                    historyModel.Status = 1;
                    historyModel.IsApproved = true;
                    historyModel.QuantityBorrow = TextUtils.ToDecimal(grvData2.GetRowCellValue(i, colNumberBorrow));
                    TSAssetManagementHistoryBO.Instance.Insert(historyModel);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch
            {

            }
        }
    }
}