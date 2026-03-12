using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DocumentFormat.OpenXml.Office2010.Excel;
using Forms.Classes;
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
    public partial class frmHistoryMoney_New : _Forms
    {
        List<int> _lstIDDel = new List<int>();
        DataTable originalTable;
        int _prevFocusedRowHandle = -1;

        public frmHistoryMoney_New()
        {
            InitializeComponent();
        }

        private void frmHistoryMoney_New_Load(object sender, EventArgs e)
        {
            loadBankName();
            loadProduct();
            loadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadProduct();
            loadData();
        }

        public void loadProduct()
        {
            DataTable dt = SQLHelper<POKHDetailModel>.LoadDataFromSP("spGetProductByPOorSHD"
                                                                    , new string[] { "@Filter" }
                                                                    , new object[] { txtFilter.Text.Trim() });

            grdProduct.DataSource = dt;
            //grvProduct.BestFitColumns();
        }

        public void loadData()
        {
            int poKHDetailID = TextUtils.ToInt(grvProduct.GetFocusedRowCellValue("ID"));

            DataTable dt = SQLHelper<HistoryMoneyPOModel>.LoadDataFromSP("spGetHistoryMoneyPONew"
                                                                        , new string[] { "@POKHDetailID" }
                                                                        , new object[] { poKHDetailID });
            originalTable = dt.Copy(); // Lưu bản gốc để so sánh khi xóa
            grdData.DataSource = dt;
            grvData.BestFitColumns();
        }
        void loadBankName()
        {
            List<object> listBanks = new List<object>()
            {
                new {BankName = "Techcombank-CN Ba Đình (19037214270015)"},
                new {BankName = "MB Bank-CN Đông Anh (835333886666)"},
                new {BankName = "TP Bank-CN Hà Nội (007 1361 8001)"},
            };

            cboBankName.ValueMember = "BankName";
            cboBankName.DisplayMember = "BankName";
            cboBankName.DataSource = listBanks;
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colADD && e.Y < 40)
            {
                AddNewRow();
            }
        }

        DataRow AddNewRow()
        {
            DataRow newRow = null;
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt.Rows.Count == 0)
                STT = 1;
            else
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;

            newRow = dt.NewRow();
            newRow["STT"] = STT;
            dt.Rows.Add(newRow);
            //newRowDataSource = dt;

            return newRow;
        }
        private bool HasUnsavedChanges()
        {
            DataTable currentTable = grdData.DataSource as DataTable;
            if (currentTable == null || originalTable == null)
                return false;

            // Kiểm tra có hàng nào đã bị sửa, thêm hoặc xóa
            return currentTable.GetChanges() != null;
        }

        private void grvProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Nếu không có thay đổi trong grdData thì cứ load dữ liệu mới
            if (!HasUnsavedChanges())
            {
                _prevFocusedRowHandle = e.FocusedRowHandle;
                loadData(); // Load dữ liệu mới theo dòng mới
                return;
            }

            // Nếu có thay đổi -> hỏi người dùng
            var result = MessageBox.Show("Dữ liệu đã thay đổi. Bạn có muốn lưu không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.Cancel)
            {
                // Quay lại dòng trước
                grvProduct.FocusedRowChanged -= grvProduct_FocusedRowChanged; // Tạm ngắt sự kiện để tránh lặp vô hạn
                grvProduct.FocusedRowHandle = _prevFocusedRowHandle;
                grvProduct.FocusedRowChanged += grvProduct_FocusedRowChanged;
                return;
            }
            else if (result == DialogResult.Yes)
            {
                SaveData(_prevFocusedRowHandle);
            }

            // Nếu chọn No hoặc Yes thì cứ load dòng mới
            _prevFocusedRowHandle = e.FocusedRowHandle;
            loadData();
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colVat || e.Column == colMoney)
            {
                decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVat));
                decimal money = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colMoney));
                grvData.SetFocusedRowCellValue(colMoneyVAT, money / (vat + 1));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc muốn xóa không !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                if (ID > 0)
                    _lstIDDel.Add(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData(grvProduct.FocusedRowHandle))
            {
                loadData();
            }
        }

        bool SaveData(int focusedIndex)
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                DataTable dtAll = ((DataTable)grdData.DataSource);
                DataTable dt = dtAll.GetChanges();
                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                int pokhDetailID = TextUtils.ToInt(grvProduct.GetRowCellValue(focusedIndex, "ID"));
                int pokhID = TextUtils.ToInt(grvProduct.GetRowCellValue(focusedIndex, "POKHID"));

                foreach (DataRow row in dt.Rows)
                {
                    switch (row.RowState)
                    {
                        case DataRowState.Deleted:
                            {
                                int idDeleted = TextUtils.ToInt(row["ID", DataRowVersion.Original]);
                                SQLHelper<HistoryMoneyPOModel>.DeleteModelByID(idDeleted);
                                break;
                            }
                        case DataRowState.Modified:
                        case DataRowState.Added:
                            {
                                HistoryMoneyPOModel model = new HistoryMoneyPOModel();
                                if (row.RowState == DataRowState.Modified)
                                {
                                    model.ID = TextUtils.ToInt(row[HistoryMoneyPOModel_Enum.ID.ToString()]);
                                    model = SQLHelper<HistoryMoneyPOModel>.FindByID(model.ID);
                                }

                                model.Money = TextUtils.ToDecimal(row[HistoryMoneyPOModel_Enum.Money.ToString()]);
                                model.MoneyVAT = TextUtils.ToDecimal(row[HistoryMoneyPOModel_Enum.MoneyVAT.ToString()]);
                                model.VAT = TextUtils.ToDecimal(row[HistoryMoneyPOModel_Enum.VAT.ToString()]);
                                model.MoneyDate = TextUtils.ToDate4(row[HistoryMoneyPOModel_Enum.MoneyDate.ToString()]);
                                model.BankName = TextUtils.ToString(row[HistoryMoneyPOModel_Enum.BankName.ToString()]);
                                model.InvoiceNo = TextUtils.ToString(row[HistoryMoneyPOModel_Enum.InvoiceNo.ToString()]);
                                model.Note = TextUtils.ToString(row[HistoryMoneyPOModel_Enum.Note.ToString()]);
                                model.ProductID = TextUtils.ToInt(row[HistoryMoneyPOModel_Enum.ProductID.ToString()]);
                                model.IsFilm = TextUtils.ToBoolean(row[HistoryMoneyPOModel_Enum.IsFilm.ToString()]);
                                model.POKHDetailID = pokhDetailID;
                                model.POKHID = pokhID;

                                if (model.MoneyDate.HasValue)
                                {
                                    SQLHelper<POKHDetailModel>.ExcuteNonQuerySQL($"UPDATE POKHDetail SET RecivedMoneyDate = '{model.MoneyDate.Value:yyyy-MM-dd HH:mm:ss}' WHERE ID = {model.POKHDetailID}");
                                }

                                if (model.ID > 0)
                                    SQLHelper<HistoryMoneyPOModel>.Update(model);
                                else
                                    SQLHelper<HistoryMoneyPOModel>.Insert(model);

                                break;
                            }
                    }
                }

                // IsPay: Trạng thái thanh toán
                // IsShip: Trạng thái giao hàng
                // IsBill: Trạng thái xuất hóa đơn
                // 1 chưa thanh toán'
                // 2 đã thanh toán một phần'
                // 3 đã thanh toán'

                POKHModel po = SQLHelper<POKHModel>.FindByID(pokhID);
                List<HistoryMoneyPOModel> lstDetail = SQLHelper<HistoryMoneyPOModel>.FindByExpression(new Utils.Expression("POKHID", pokhID));
                decimal total = lstDetail.Sum(x => x.Money ?? 0);
                po.ReceiveMoney = total;
                if (po.ReceiveMoney > 0)
                {
                    po.IsPay = true;
                    int status = TextUtils.ToInt(po.Status);

                    if (po.IsPay && po.IsExport && po.IsBill)
                        status = 1; // Đã giao đã thanh toán
                    if (!po.IsPay && po.IsExport)
                        status = 3; //Đã giao nhưng chưa thanh toán
                    if (!po.IsPay && !po.IsExport)
                        status = 0;//Chưa giao chưa thanh toán
                    if (po.IsPay && !po.IsExport)
                        status = 2;//Chưa giao đã thanh toán
                    if (po.IsPay && po.IsExport && !po.IsBill)
                        status = 4;//Đã thanh toán nhưng chưa xuất hóa đơn
                    if (po.IsShip && !po.IsExport)
                        status = 5;//Đã giao 1 phần 

                    po.Status = status;
                }
                // Check trạng thái thành toán 
                if (po.IsPay == true)
                {
                    if (po.ReceiveMoney > 0 && po.ReceiveMoney < po.TotalMoneyPO) // Thanh toán một phần
                    {
                        po.PaymentStatus = 2;
                    }
                    else if (po.TotalMoneyPO - po.ReceiveMoney <= 0)
                    {
                        po.PaymentStatus = 3;
                    }
                }
                else
                {
                    po.PaymentStatus = 1;
                }
                po.DeliveryStatus = 1;

                SQLHelper<POKHModel>.Update(po);


                // Cập nhật lại dữ liệu trong lưới
                decimal sum = dtAll.AsEnumerable().Sum(p => p.Field<decimal>("Money"));

                decimal totalMoney = TextUtils.ToDecimal(grvProduct.GetRowCellValue(focusedIndex, "TotalPriceIncludeVAT"));


                grvProduct.SetRowCellValue(focusedIndex, "TotalMoneyRemaining", totalMoney - sum);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void grvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }
    }
}