using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
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
    public partial class frmAddSerialNumber : _Forms
    {
        public BillImportDetailModel billImportDetailModel = new BillImportDetailModel();
        public BillExportDetailModel billExportDetailModel = new BillExportDetailModel();
        ArrayList lstIDDelete = new ArrayList();
        public int quantity;
        public int Type = 0;


        public List<BillImportDetailSerialNumberModel> listSerials = new List<BillImportDetailSerialNumberModel>();

        public frmAddSerialNumber()
        {
            InitializeComponent();
        }

        private void frmAddSerialNumber_Load(object sender, EventArgs e)
        {
            LoadData();

            //LoadAddRow();
            //setFocusCell(getFirstRowSpace());
        }

        #region LOAD DỮ LIỆU
        private void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetSerialNumberBillImportByProductID", "A", new string[] { "@ProductID" }, new object[] { billExportDetailModel.ProductID });
            grdData.DataSource = dt;
        }
        #endregion
        #region THÊM VÀ XÓA DÒNG
        //Từ động add Row theo số lượng vật tư
        int Qty;
        private void LoadAddRow()
        {
            Qty = billExportDetailModel.ProductID;

            int rowCount = grvData.RowCount;
            MyLib.AddNewRow(grdData, grvData);
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            grvData.CloseEditor();
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                if (Type == 1)  //Phiếu Nhập
                {
                    if (grvData.RowCount >= TextUtils.ToInt(billImportDetailModel.Qty)) return;
                }
                else  //Phiếu Xuất
                    if (grvData.RowCount >= TextUtils.ToInt(billExportDetailModel.Qty)) return;
                MyLib.AddNewRow(grdData, grvData);
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string serialNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colSerialNumber));
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa SerialNumber '{serialNumber}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }
            }
        }
        #endregion
        #region SAVE

        bool Save()
        {
            int selected = grvData.GetSelectedRows().Length;
            int[] selectedRowHandles = grvData.GetSelectedRows();

            foreach (int item in selectedRowHandles)
            {
                if (selected <= quantity)
                {
                    BillImportDetailSerialNumberModel serialNumberModel = new BillImportDetailSerialNumberModel();

                    // Lấy dữ liệu từ GridControl
                    serialNumberModel.SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(item, colSerialNumber));
                    serialNumberModel.SerialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(item, colSerialNumberRTC));
                    serialNumberModel.STT = TextUtils.ToInt(grvData.GetRowCellValue(item, colSTT));

                    listSerials.Add(serialNumberModel);
                }
                else
                {
                    MessageBox.Show(String.Format($"Chọn quá số lượng hàng so với phiếu xuất"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region SỰ KIỆN ENTER XUỐNG DÒNG
        void setFocusCell(int indexRow)
        {
            grvData.FocusedRowHandle = indexRow;
            grvData.FocusedColumn = colSerialNumber;
            grvData.ShowEditor();
        }

        //Focus dòng đầu tiên k có dữ liệu cần nhập
        int indexRow;
        int getFirstRowSpace()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
                if (string.IsNullOrEmpty(serialNumber))
                {
                    indexRow = i;
                    return indexRow;
                }
            }
            return indexRow;
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvData.CloseEditor();
            setFocusCell(e.RowHandle + 1);
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void frmAddSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
