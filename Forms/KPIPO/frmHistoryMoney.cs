using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
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

namespace Forms.KPI_PO
{
    public partial class frmHistoryMoney : _Forms
    {
        public string project;
        public int ID;

        public double totalMoneyPO;
        public double priceRemain;

        public decimal totalQuantityRequest = 0;
        public decimal totalQuantityReturn = 0;

        public frmHistoryMoney()
        {
            InitializeComponent();
        }

        private void frmHistoryMoney_Load(object sender, EventArgs e)
        {
            lbProject.Text = project;
            loadProject();
            loadProduct();
            checkMoneyType();
            LoadBankName();

        }
        void checkMoneyType()
        {
            DataTable dt = TextUtils.Select($"Select * from HistoryMoneyPO where POKHID={ID}");
            if (dt.Rows.Count > 0)
            {
                int type = TextUtils.ToInt(dt.Rows[0]["Type"]);
                cbType.SelectedIndex = type;
                //cbType.Enabled = false;
            }
        }
        DataSet ds = new DataSet();
        void loadGrvUser()
        {
            try
            {
                ds = TextUtils.LoadDataSetFromSP("spGetHistoryMoneyPO", new string[] { "@POKHID", "@Type" }, new object[] { ID, cbType.SelectedIndex });
                grdData.DataSource = ds.Tables[0];
                grdDataProduct.DataSource = ds.Tables[1];

                bool merge = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colMerge));
                Check(merge);

                #region VtNam update trạng thái thanh toán
                var summarys = grvData.Columns[colMoneyDate.FieldName].Summary;
                if (summarys.Count > 0)
                {
                    grvData.Columns[colMoneyDate.FieldName].Summary.Clear();
                }

                var summarys1 = grvData.Columns[colVat.FieldName].Summary;
                if (summarys1.Count > 0)
                {
                    grvData.Columns[colVat.FieldName].Summary.Clear();
                }

                //double totalMoney = TextUtils.ToDouble(TotalMoneyPO);
                grvData.Columns[colMoneyDate.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colMoneyDate.FieldName, $"Tổng tiền PO = {totalMoneyPO.ToString("#,##0.00")}"));

                priceRemain = (totalMoneyPO - TextUtils.ToDouble(grvData.Columns[$"{colMoney.FieldName}"].SummaryItem.SummaryValue));
                grvData.Columns[colVat.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colVat.FieldName, $"Số tiền còn lại = {priceRemain.ToString("#,##0.00")}"));
                #endregion

                //if (merge)
                //{
                //    grvData.OptionsView.AllowCellMerge = true;
                //    colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                //    colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                //    colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                //    //colGroup.GroupIndex = 0;
                //    //grvData.ExpandAllGroups();
                //    grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                //}
                //else
                //{
                //    //colGroup.GroupIndex = -1;
                //    grvData.OptionsView.AllowCellMerge = false;
                //    grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                //    colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                //    colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                //    colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DataTable dtProject;
        DataTable dtProduct;
        void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,UserID,ContactID,CustomerID From Project");
            cbProject.DisplayMember = "ProjectCode";
            cbProject.ValueMember = "ID";
            cbProject.DataSource = dtProject;
        }
        void loadProduct()
        {
            // if (cboGroup.Text == "") return;
            dtProduct = TextUtils.Select("SELECT ID,ProductCode,ProductName,ItemType,Unit,Maker FROM ProductSale");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
        }


        void LoadBankName()
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
        void SaveData()
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                if (lstIDDelete.Count > 0)
                    HistoryMoneyPOBO.Instance.Delete(lstIDDelete);
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    HistoryMoneyPOModel model = new HistoryMoneyPOModel();
                    int IDMoney = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    if (IDMoney > 0)
                        model = (HistoryMoneyPOModel)HistoryMoneyPOBO.Instance.FindByPK(IDMoney);
                    model.Money = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMoney));
                    model.MoneyVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMoneyVAT));
                    model.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVat));
                    model.MoneyDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colMoneyDate));
                    model.BankName = TextUtils.ToString(grvData.GetRowCellValue(i, colBankName));
                    model.InvoiceNo = TextUtils.ToString(grvData.GetRowCellValue(i, colInvoiceNo));
                    model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                    model.POKHDetailID = TextUtils.ToInt(ds.Tables[2].Rows[i]["ID"]);
                    model.POKHID = TextUtils.ToInt(ID);
                    model.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    model.Type = TextUtils.ToInt(cbType.SelectedIndex);
                    model.IsFilm = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsFilm));
                    model.IsMergePO = ckbMerge.Checked;
                    if (cbType.SelectedIndex == 1)
                    {
                        DateTime? moneyDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colMoneyDate));
                        if (moneyDate.HasValue)
                        {
                            TextUtils.ExcuteSQL($"Update POKHDetail set RecivedMoneyDate = '{moneyDate.Value.ToString("yyyy-MM-dd HH:mm:ss")}' where ID = {model.POKHDetailID}");
                        }
                    }
                    else
                    {
                        DateTime? moneyDate = TextUtils.ToDate2(grvData.GetRowCellValue(0, colMoneyDate));
                        if (moneyDate.HasValue)
                        {
                            TextUtils.ExcuteSQL($"Update POKHDetail set RecivedMoneyDate = '{moneyDate.Value.ToString("yyyy-MM-dd HH:mm:ss")}' where ID = {model.POKHID}");
                        }
                    }
                    if (model.ID > 0)
                    {
                        HistoryMoneyPOBO.Instance.Update(model);
                    }
                    else
                    {
                        HistoryMoneyPOBO.Instance.Insert(model);
                    }
                }

                TextUtils.ExcuteSQL($"Update POKH set ReceiveMoney = {colMoney.SummaryItem.SummaryValue} where ID = {ID}");
                DataTable dt = TextUtils.Select($"Select * From POKH where ID={ID}");
                if (dt.Select("ReceiveMoney>0").Length > 0)
                {
                    TextUtils.ExcuteSQL($"Update POKH set IsPay = 1  where ID = {ID}");
                    cPOStatus.AutoUpdateStatus(ID);
                }
                for (int i = 0; i < grvDataProduct.RowCount; i++)
                {
                    int iduser = TextUtils.ToInt(grvDataProduct.GetRowCellValue(i, colIDPro));
                    POKHDetailMoneyModel detailuser = new POKHDetailMoneyModel();
                    if (iduser > 0)
                    {
                        detailuser = (POKHDetailMoneyModel)(POKHDetailMoneyBO.Instance.FindByPK(iduser));
                    }
                    detailuser.ReceiveMoney = TextUtils.ToDecimal(grvDataProduct.GetRowCellValue(i, colReceiveMoneyPro));
                    detailuser.Month = TextUtils.ToDate5(grvData.GetRowCellValue(0, colMoneyDate)).Month;
                    if (detailuser.ID > 0)
                        POKHDetailMoneyBO.Instance.Update(detailuser);
                    else
                        POKHDetailMoneyBO.Instance.Insert(detailuser);
                }

                #region VtNam update trạng thái thanh toán
                //if (ID > 0)
                //{
                //    if (priceRemain < 0)
                //    {
                //        MessageBox.Show("Số tiền về đang lớn hơn tổng tiền nhận PO", "Thông báo", MessageBoxButtons.OK);
                //        return;
                //    }
                //}
                UpdateStatusPO();
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        void UpdateStatusPO()
        {
            grvData.FocusedRowHandle = -1;
            #region VtNam update trạng thái thanh toán
            POKHModel POKH = SQLHelper<POKHModel>.FindByID(ID);

            // IsPay: Trạng thái thanh toán
            // IsShip: Trạng thái giao hàng
            // IsBill: Trạng thái xuất hóa đơn
            // 1 chưa thanh toán'
            // 2 đã thanh toán một phần'
            // 3 đã thanh toán'

            // Check trạng thái thành toán 
            if (POKH.IsPay == true)
            {
                if (priceRemain > 0 && priceRemain < totalMoneyPO) // Thanh toán một phần
                {
                    POKH.PaymentStatus = 2;
                }
                else if (priceRemain <= 0)
                {
                    POKH.PaymentStatus = 3;
                }
            }
            else
            {
                POKH.PaymentStatus = 1;
            }

            //Qty = QtyReturn = 0;
            //ProcessNodes(TreeData.Nodes);

            if (totalQuantityReturn == 0) POKH.DeliveryStatus = 1;
            else if (totalQuantityRequest > totalQuantityReturn) POKH.DeliveryStatus = 2;
            else POKH.DeliveryStatus = 3;

            SQLHelper<POKHModel>.Update(POKH);
            #endregion
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            grvData.AddNewRow();
        }
        ArrayList lstIDDelete = new ArrayList();

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colVat || e.Column == colMoney)
            {
                decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVat));
                decimal money = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colMoney));
                grvData.SetFocusedRowCellValue(colMoneyVAT, money / (vat + 1));
            }
            if (e.Column == colMoneyVAT)
                tinhMoneyUser();
        }
        void tinhMoneyUser()
        {
            if (cbType.SelectedIndex == 0)
            {
                grvData.FocusedRowHandle = -1;
                decimal total = TextUtils.ToDecimal(colMoney.SummaryItem.SummaryValue);
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    decimal moneyPro = total * TextUtils.ToDecimal(ds.Tables[2].Rows[i]["Ratio"]);
                    for (int j = 0; j < grvDataProduct.RowCount; j++)
                    {
                        int ID = TextUtils.ToInt(grvDataProduct.GetRowCellValue(j, colPOKHDetailID));
                        if (ID == TextUtils.ToInt(ds.Tables[2].Rows[i]["ID"]))
                        {
                            decimal per = TextUtils.ToDecimal(grvDataProduct.GetRowCellValue(j, colPercentUser));
                            grvDataProduct.SetRowCellValue(j, colReceiveMoneyPro, moneyPro * per);
                        }
                    }
                }
            }
            else if (cbType.SelectedIndex == 1)
            {
                grvData.FocusedRowHandle = -1;
                for (int i = 0; i < grvData.RowCount; i++)
                {


                    int product = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    decimal total = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMoney));
                    for (int j = 0; j < grvDataProduct.RowCount; j++)
                    {
                        int productID = TextUtils.ToInt(grvDataProduct.GetRowCellValue(j, colProductPro));
                        if (product == productID)
                        {
                            decimal per = TextUtils.ToDecimal(grvDataProduct.GetRowCellValue(j, colPercentUser));
                            grvDataProduct.SetRowCellValue(j, colReceiveMoneyPro, total * per);
                        }
                    }
                }
            }
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrvUser();
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colADDD && e.Y < 40)
            {
                grvData.AddNewRow();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc muốn xóa không !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                lstIDDelete.Add(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void frmHistoryMoney_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvData.FocusedRowHandle++;
            }
            if (e.KeyCode == Keys.C && e.Control)
            {
                grvData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                if (grvData.FocusedColumn == colProductID)
                {
                    try
                    {
                        cGlobVar.LockEvents = true;
                        string[] arChar = { "\r\n" };
                        List<string> lstCode = Clipboard.GetText().Split(arChar, StringSplitOptions.None).ToList();
                        if (!lstCode.Any()) return;
                        if (lstCode[lstCode.Count - 1] == "")
                            lstCode.RemoveAt(lstCode.Count - 1);
                        DataTable dt = (DataTable)cbProduct.DataSource;
                        for (int i = 0; i < lstCode.Count; i++)
                        {
                            int rowCount = grvData.RowCount - (grvData.FocusedRowHandle + 1);
                            if (lstCode.Count > rowCount + 1)
                                AddNewRow();
                            DataRow[] dtr = dt.Select($"ProductID='{lstCode[i]}'");
                            if (dtr.Length == 0) return;
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colMoney, dtr[0]["Money"]);
                            //grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductName, dtr[0]["ProductName"]);
                            //grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductCode, dtr[0]["ProductCode"]);
                            //grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colUnit, dtr[0]["Unit"]);
                            //grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colMaker, dtr[0]["Maker"]);
                        }
                    }
                    finally
                    {
                        e.Handled = true;
                        cGlobVar.LockEvents = false;
                    }
                }
                else
                {

                    int[] selectedRow = grvData.GetSelectedRows();
                    GridCell[] selectedColumn = grvData.GetSelectedCells();

                    List<GridColumn> listCol = new List<GridColumn>();

                    for (int i = 0; i < selectedColumn.Length; i++)
                    {
                        GridColumn colSelect = selectedColumn[i].Column;
                        listCol.Add(colSelect);
                    }

                    string[] separator = { "\r\n" };
                    var data = Clipboard.GetText();

                    List<string> listDataClipboard = Clipboard.GetText().Split(separator, StringSplitOptions.None).ToList();
                    foreach (string item in listDataClipboard.ToList())
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            listDataClipboard.Remove(item);
                        }
                    }

                    if (listDataClipboard.Count <= 1)
                    {
                        for (int i = 0; i < selectedRow.Length; i++)
                        {
                            for (int j = 0; j < selectedColumn.Length; j++)
                            {
                                grvData.SetRowCellValue(i, selectedColumn[j].Column, listDataClipboard[0]);
                            }

                        }
                    }
                    else
                    {
                        grvData.FocusedColumn = selectedColumn[0].Column;
                        grvData.FocusedRowHandle = selectedRow[0];

                        grvData.PasteFromClipboard();
                    }
                }
            }
        }

        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            Check(ckbMerge.Checked);
            //if (ckbMerge.Checked)
            //{
            //    colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //    grvData.OptionsView.AllowCellMerge = true;
            //    colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //    colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //    //colGroup.GroupIndex = 0;
            //    grvData.ExpandAllGroups();
            //    grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //}
            //else
            //{
            //    //colGroup.GroupIndex = -1;
            //    grvData.OptionsView.AllowCellMerge = false;
            //    grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //    colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //    colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //    colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //}
        }
        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column == colVat || e.Column == colMoneyVAT || e.Column == colMoney)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = ((value2 == 0 || value1 == value2) && value1 != 0);
                e.Handled = true;
                return;
            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = ((string.IsNullOrEmpty(value2) || value2 == "0") && value1 != "");
                e.Handled = true;
                return;
            }
        }
        private bool Check(bool data)
        {
            if (data)
            {
                colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                grvData.OptionsView.AllowCellMerge = true;
                colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                //colGroup.GroupIndex = 0;
                grvData.ExpandAllGroups();
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                return true;
            }
            {
                //colGroup.GroupIndex = -1;
                grvData.OptionsView.AllowCellMerge = false;
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                colMoneyVAT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                colMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                colGuestCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                return false;
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

        private void grvData_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == $"{colVat.FieldName}")
            {
                priceRemain = (totalMoneyPO - TextUtils.ToDouble(grvData.Columns[$"{colMoney.FieldName}"].SummaryItem.SummaryValue));
                e.Info.DisplayText = $"Số tiền còn lại = {priceRemain.ToString("#,##0.00")}";
            }
        }
    }
}
