using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator;

namespace BMS
{
    public partial class frmAddQRCode : _Forms
    {
        private int warehouseID;
        public frmAddQRCode(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        //public frmAddQRCode()
        //{
        //	InitializeComponent();
        //}

        private void frmAddQRCode_Load(object sender, EventArgs e)
        {
            LoadModulaLocation();
            loadGrdData();
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");

            grdData.ContextMenuStrip = contextMenuStrip1;
        }

        void LoadModulaLocation()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetModulaLocation", "A", new string[] { "@ModulaLocationID", "@Keyword", "@IsDeleted" }, new object[] { 0, "", 0 });
            cboModulaLocationDetail.Properties.ValueMember = "ModulaLocationDetailID";
            cboModulaLocationDetail.Properties.DisplayMember = "LocationName";
            cboModulaLocationDetail.Properties.DataSource = dt;
        }

        void loadGrdData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductAndQrCode", "A",
                                                    new string[] { "@WarehouseID", "@FilterText" },
                                                    new object[] { warehouseID, txtKeyword.Text.Trim()});
            grdData.DataSource = dt;
        }

        int _RowIndex;
        private void btnNew_Click(object sender, EventArgs e)
        {
            _RowIndex = grvData.FocusedRowHandle;
            frmAddQRCodeDetail frm = new frmAddQRCodeDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrdData();
            }
            grvData.FocusedRowHandle = _RowIndex;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            _RowIndex = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colId));
            if (id <= 0) return;
            ProductRTCQRCodeModel modelrtc = (ProductRTCQRCodeModel)ProductRTCQRCodeBO.Instance.FindByPK(id);
            frmAddQRCodeDetail frm = new frmAddQRCodeDetail(warehouseID);
            frm.modelrtc = modelrtc;
            frm.Edit = true;

            frm.ShowDialog();
            loadGrdData();
            grvData.FocusedRowHandle = _RowIndex;
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colId));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã Qrcode của sản phẩm  [{0}] : [{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductRTCQRCodeBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
            }
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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {


        }

        private void BntNhapExcel_Click(object sender, EventArgs e)
        {
            frmImportQRCode frm = new frmImportQRCode();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrdData();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrdData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn set vị trí [{cboModulaLocationDetail.Text}] cho tất cả các thiết bị được chọn không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (var item in rowSelected)
            {
                int productID = TextUtils.ToInt(grvData.GetRowCellValue(item, colId));
                var product = SQLHelper<ProductRTCQRCodeModel>.FindByID(productID);
                product.ModulaLocationDetailID = TextUtils.ToInt(cboModulaLocationDetail.EditValue);
                SQLHelper<ProductRTCQRCodeModel>.Update(product);
            }

            loadGrdData();
        }

        List<string> qrCodes = new List<string>();
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                //int[] selectedRows = grvData.GetSelectedRows();

                //foreach (int row in selectedRows)
                //{
                //    string qrCode = TextUtils.ToString(grvData.GetRowCellValue(row, colQrcode));
                //    if (!qrCodes.Contains(qrCode)) qrCodes.Add(qrCode);
                //}

                var frm = new frmProductHistoryBorrowDetailNew(warehouseID, qrCodes);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadModulaLocation();
        }

        private void chkIsSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit checkEdit = (CheckEdit)sender;
            string qrCode = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colQrcode));

            if (checkEdit.Checked && !qrCodes.Contains(qrCode)) qrCodes.Add(qrCode);
            else if (!checkEdit.Checked) qrCodes.Remove(qrCode);

            //MessageBox.Show(string.Join(";", qrCodes));
        }

        private void btnSelectedAll_Click(object sender, EventArgs e)
        {
            SelectedAll(true);
        }

        private void btnUnSelectedAll_Click(object sender, EventArgs e)
        {
            SelectedAll(false);
        }


        void SelectedAll(bool isSelected)
        {

            //if (isSelected)
            //{
            //    MessageBox.Show("Những sản phẩm Đang mượn hoặc Đã xuất kho sẽ được tự động bỏ qua!", "Thông báo");
            //}
            for (int i = 0; i < grvData.RowCount; i++)
            {
                //int status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus));
                //if (status != 1) continue;
                grvData.SetRowCellValue(i, colIsSelected, isSelected);

                string qrCode = TextUtils.ToString(grvData.GetRowCellValue(i, colQrcode));

                if (isSelected && !qrCodes.Contains(qrCode)) qrCodes.Add(qrCode);
                else if (!isSelected) qrCodes.Remove(qrCode);
            }

            //MessageBox.Show(string.Join(";", qrCodes));
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //int status = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colStatus));
            //if (status != 1)
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Bạn chỉ được chọn những sản phẩm Trong kho!";
            //}
        }
    }
}
