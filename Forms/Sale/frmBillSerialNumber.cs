using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections;
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
    public partial class frmBillSerialNumber : _Forms
    {
        public BillImportDetailModel billImportDetailModel = new BillImportDetailModel();
        public BillExportDetailModel billExportDetailModel = new BillExportDetailModel();
        ArrayList lstIDDelete = new ArrayList();
        public int Type;
        public int ID;


        List<BillImportDetailSerialNumberModel> listIdImports = new List<BillImportDetailSerialNumberModel>();
        List<BillExportDetailSerialNumberModel> listIdExports = new List<BillExportDetailSerialNumberModel>();
        public frmBillSerialNumber()
        {
            InitializeComponent();
        }

        private void frmBillImportDetailSerialNumber_Load(object sender, EventArgs e)
        {
            //Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            //int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 4;
            //int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            //this.Location = new Point((screen.Width - w) / 2, ((screen.Height - h) / 2) - 63);
            //this.Size = new Size(w, screen.Height);

            LoadData(Type);

            LoadAddRow();
            setFocusCell(getFirstRowSpace());
        }
        #region LOAD DỮ LIỆU
        private void LoadData(int type)
        {
            DataTable dt = new DataTable();
            if (type == 1) //Phiếu Nhập
            {
                dt = TextUtils.LoadDataFromSP("spGetBillImportDetailSerialNumber", "A", new string[] { "@BillImportDetailID" }, new object[] { billImportDetailModel.ID });
                btnAdd.Enabled = false;
            }
            else // Phiếu Xuất
            {
                dt = TextUtils.LoadDataFromSP("spGetBillExportDetailSerialNumber", "A", new string[] { "@BillExportDetailID" }, new object[] { billExportDetailModel.ID });
            }
            grdData.DataSource = dt;
        }

        #endregion

        #region THÊM VÀ XÓA DÒNG
        //Từ động add Row theo số lượng vật tư
        public int Qty;
        private void LoadAddRow()
        {
            if (Type == 1) //Phiếu Nhập
            {
                Qty = TextUtils.ToInt(billImportDetailModel.Qty);
            }
            else //Phiếu Xuất
                Qty = TextUtils.ToInt(billExportDetailModel.Qty);
            int rowCount = grvData.RowCount;
            if (rowCount < Qty)
            {
                var number = Qty - rowCount;
                for (int i = 0; i < number; i++)
                {
                    MyLib.AddNewRow(grdData, grvData);
                }
            }
        }

        //Nút Add dòng
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
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string serialNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colSerialNumber));
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa SerialNumber '{serialNumber}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                //if (id > 0)
                //{
                //    lstIDDelete.Add(id);
                //}

                if (Type == 1)
                {
                    BillImportDetailSerialNumberModel serial = SQLHelper<BillImportDetailSerialNumberModel>.FindByID(id);
                    if (serial != null) listIdImports.Add(serial);
                }
                else
                {
                    BillExportDetailSerialNumberModel serial = SQLHelper<BillExportDetailSerialNumberModel>.FindByID(id);
                    if (serial != null) listIdExports.Add(serial);
                }
            }
        }
        #endregion

        #region SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (Save())
            //    //this.DialogResult = DialogResult.OK;
            //    this.Close();


            if (SaveData()) this.Close();
        }

        private void btnSaveNow_Click(object sender, EventArgs e)
        {
            //if (Save())
            //    this.DialogResult = DialogResult.OK;

        }
        //bool Save()
        //{
        //    ArrayList lstSerialNumber = new ArrayList();
        //    ArrayList lstSerialNumberRTC = new ArrayList();
        //    grvData.CloseEditor();
        //    for (int i = 0; i < grvData.RowCount; i++)
        //    {
        //        if (Type == 1) //Phiếu NHập
        //        {
        //            BillImportDetailSerialNumberModel _model = new BillImportDetailSerialNumberModel();
        //            string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
        //            string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));
        //            int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
        //            if (string.IsNullOrEmpty(serialNumber))
        //            {
        //                MessageBox.Show(String.Format($"Vui lòng nhập Serial Number tại dòng [{STT}]"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return false;
        //            }
        //            else
        //            {
        //                if (lstSerialNumberRTC.Contains(serialNumberRTC))
        //                {
        //                    //MessageBox.Show(String.Format($"Serial Number tại dòng [{STT}] đã tồn tại"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    //colSerialNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        //                    //return false;

        //                    MessageBox.Show(String.Format($"Serial Number RTC tại dòng [{STT}] đã tồn tại"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    // colSerialNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        //                    //LoadData(Type);
        //                    //LoadAddRow();
        //                    return false;

        //                }
        //            }
        //            int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
        //            if (ID > 0)
        //            {
        //                _model = (BillImportDetailSerialNumberModel)BillImportDetailSerialNumberBO.Instance.FindByPK(ID);
        //            }
        //            _model.BillImportDetailID = billImportDetailModel.ID;
        //            _model.SerialNumber = serialNumber;
        //            _model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
        //            _model.SerialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));
        //            if (_model.ID > 0)
        //                BillImportDetailSerialNumberBO.Instance.Update(_model);
        //            else
        //                BillImportDetailSerialNumberBO.Instance.Insert(_model);
        //            lstSerialNumber.Add(serialNumber);
        //            lstSerialNumberRTC.Add(serialNumberRTC);
        //        }
        //        else // Phiếu Xuất
        //        {
        //            BillExportDetailSerialNumberModel _model = new BillExportDetailSerialNumberModel();
        //            string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
        //            string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));
        //            int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

        //            if (string.IsNullOrEmpty(serialNumber))
        //            {
        //                MessageBox.Show(String.Format($"Vui lòng nhập Serial Number tại dòng [{STT}]"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return false;
        //            }
        //            else
        //            {
        //                if (lstSerialNumberRTC.Contains(serialNumberRTC))
        //                {
        //                    MessageBox.Show(String.Format($"Serial Number RTC tại dòng [{STT}] đã tồn tại"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    //colSerialNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        //                    return false;
        //                }
        //            }
        //            int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
        //            if (ID > 0)
        //            {
        //                _model = (BillExportDetailSerialNumberModel)BillExportDetailSerialNumberBO.Instance.FindByPK(ID);
        //            }
        //            _model.BillExportDetailID = billExportDetailModel.ID;
        //            _model.SerialNumber = serialNumber;
        //            _model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
        //            _model.SerialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));

        //            if (_model.ID > 0)
        //            {
        //                BillExportDetailSerialNumberBO.Instance.Update(_model);
        //            }   
        //            else
        //            {
        //                BillExportDetailSerialNumberBO.Instance.Insert(_model);
        //            }    
        //            lstSerialNumber.Add(serialNumber);
        //            lstSerialNumberRTC.Add(serialNumberRTC);
        //        }
        //    }
        //    if (Type == 1)
        //    {
        //        if (lstIDDelete.Count > 0)
        //            BillImportDetailSerialNumberBO.Instance.Delete(lstIDDelete);
        //    }
        //    else
        //    {
        //        if (lstIDDelete.Count > 0)
        //            BillExportDetailSerialNumberBO.Instance.Delete(lstIDDelete);
        //    }
        //    return true;
        //}

        bool Save()
        {
            //ArrayList lstSerialNumber = new ArrayList();
            ArrayList lstSerialNumberRTC = new ArrayList();
            grvData.CloseEditor();
            List<BillImportDetailSerialNumberModel> importModels = new List<BillImportDetailSerialNumberModel>();
            List<BillExportDetailSerialNumberModel> exportModels = new List<BillExportDetailSerialNumberModel>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (Type == 1) //Phiếu NHập
                {
                    BillImportDetailSerialNumberModel _model = new BillImportDetailSerialNumberModel();

                    string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
                    string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));
                    int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                    if (string.IsNullOrEmpty(serialNumber))
                    {
                        MessageBox.Show(String.Format($"Vui lòng nhập Serial Number tại dòng [{STT}]"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        if (lstSerialNumberRTC.Contains(serialNumberRTC))
                        {
                            MessageBox.Show(String.Format($"Serial Number RTC tại dòng [{STT}] đã tồn tại"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // colSerialNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            //  LoadData(Type);
                            // LoadAddRow();
                            return false;
                        }
                    }
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    if (ID > 0)
                    {
                        _model = (BillImportDetailSerialNumberModel)BillImportDetailSerialNumberBO.Instance.FindByPK(ID);
                    }
                    _model.BillImportDetailID = billImportDetailModel.ID;
                    _model.SerialNumber = serialNumber;
                    _model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                    _model.SerialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));

                    importModels.Add(_model);
                    lstSerialNumberRTC.Add(serialNumberRTC);


                }

                else // Phiếu Xuất
                {
                    BillExportDetailSerialNumberModel _model = new BillExportDetailSerialNumberModel();
                    string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
                    string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));
                    int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

                    if (string.IsNullOrEmpty(serialNumber))
                    {
                        MessageBox.Show(String.Format($"Vui lòng nhập Serial Number tại dòng [{STT}]"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        if (lstSerialNumberRTC.Contains(serialNumberRTC))
                        {
                            MessageBox.Show(String.Format($"Serial Number tại dòng [{STT}] đã tồn tại"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //colSerialNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            return false;
                        }
                    }
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    if (ID > 0)
                    {
                        _model = (BillExportDetailSerialNumberModel)BillExportDetailSerialNumberBO.Instance.FindByPK(ID);
                    }
                    _model.BillExportDetailID = billExportDetailModel.ID;
                    _model.SerialNumber = serialNumber;
                    _model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                    _model.SerialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC));

                    exportModels.Add(_model);
                    lstSerialNumberRTC.Add(serialNumberRTC);
                }
            }
            if (Type == 1)
            {
                foreach (var importModel in importModels)
                {
                    //BillImportDetailSerialNumberBO.Instance.Insert(importModel);
                    if (importModel.ID > 0)
                    {
                        BillExportDetailSerialNumberBO.Instance.Update(importModel);
                    }
                    else
                    {
                        BillExportDetailSerialNumberBO.Instance.Insert(importModel);
                    }
                }

            }
            else
            {
                // BillExportDetailSerialNumberBO.Instance.Insert(exportModels);
                foreach (var exportModel in exportModels)
                {
                    if (exportModel.ID > 0)
                    {
                        BillExportDetailSerialNumberBO.Instance.Update(exportModel);
                    }
                    else
                    {
                        BillExportDetailSerialNumberBO.Instance.Insert(exportModel);
                    }
                }
            }
            if (Type == 1)
            {
                if (lstIDDelete.Count > 0)
                    BillImportDetailSerialNumberBO.Instance.Delete(lstIDDelete);
            }
            else
            {
                if (lstIDDelete.Count > 0)
                    BillExportDetailSerialNumberBO.Instance.Delete(lstIDDelete);
            }
            return true;
        }


        bool SaveData()
        {
            grvData.FocusedRowHandle = -1;
            if (!CheckValidate()) return false;

            //MessageBox.Show("ok save nhé!");
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber)).Trim();
                string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC)).Trim();

                if (Type == 1)
                {
                    BillImportDetailSerialNumberModel serial = SQLHelper<BillImportDetailSerialNumberModel>.FindByID(id);
                    if (serial == null) serial = new BillImportDetailSerialNumberModel();

                    serial.BillImportDetailID = billImportDetailModel.ID;
                    serial.STT = stt;
                    serial.SerialNumber = serialNumber;
                    serial.SerialNumberRTC = serialNumberRTC;

                    if (serial.ID > 0)
                    {
                        SQLHelper<BillImportDetailSerialNumberModel>.Update(serial);
                    }
                    else
                    {
                        SQLHelper<BillImportDetailSerialNumberModel>.Insert(serial);
                    }
                }
                else
                {
                    BillExportDetailSerialNumberModel serial = new BillExportDetailSerialNumberModel();
                    if (serial == null) serial = new BillExportDetailSerialNumberModel();

                    serial.BillExportDetailID = billExportDetailModel.ID;
                    serial.STT = stt;
                    serial.SerialNumber = serialNumber;
                    serial.SerialNumberRTC = serialNumberRTC;

                    if (serial.ID > 0)
                    {
                        SQLHelper<BillExportDetailSerialNumberModel>.Update(serial);
                    }
                    else
                    {
                        SQLHelper<BillExportDetailSerialNumberModel>.Insert(serial);
                    }
                }
            }

            if (listIdImports.Count > 0)
            {
                SQLHelper<BillImportDetailSerialNumberModel>.DeleteListModel(listIdImports);
            }

            if (listIdExports.Count > 0)
            {
                SQLHelper<BillExportDetailSerialNumberModel>.DeleteListModel(listIdExports);
            }

            listIdImports.Clear();
            listIdExports.Clear();
            return true;
        }

        bool CheckValidate()
        {
            grvData.FocusedRowHandle = -1;
            List<string> listSerials = new List<string>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumberRTC)).Trim();
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, colSTT)).Trim();

                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                if (!string.IsNullOrEmpty(serialNumberRTC))
                {
                    //Check trùng trên view
                    if (listSerials.Contains(serialNumberRTC))
                    {
                        MessageBox.Show($"Serial Number RTC dòng [{stt}] đã tồn tại.\nVui lòng kiểm tra lại!", "Thông báo");
                        grvData.FocusedColumn = colSerialNumberRTC;
                        grvData.FocusedRowHandle = i;
                        return false;
                    }
                    else
                    {
                        listSerials.Add(serialNumberRTC);
                    }

                    //Check trùng trong data
                    //var exp1 = new Expression("SerialNumberRTC", serialNumberRTC);
                    //var exp2 = new Expression("ID", id, "<>");
                    //if (Type == 1)
                    //{
                    //    var serialNumbers = SQLHelper<BillImportDetailSerialNumberModel>.FindByExpression(exp1.And(exp2));
                    //    if (serialNumbers.Count() > 0)
                    //    {
                    //        MessageBox.Show($"Serial Number RTC dòng [{stt}] đã tồn tại trong danh sách nhập kho.\nVui lòng kiểm tra lại!", "Thông báo");
                    //        grvData.FocusedColumn = colSerialNumberRTC;
                    //        grvData.FocusedRowHandle = i;
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    var serialNumbers = SQLHelper<BillExportDetailSerialNumberModel>.FindByExpression(exp1.And(exp2));
                    //    if (serialNumbers.Count() > 0)
                    //    {
                    //        MessageBox.Show($"Serial Number RTC dòng [{stt}] đã tồn tại trong danh sách xuất kho.\nVui lòng kiểm tra lại!", "Thông báo");
                    //        grvData.FocusedColumn = colSerialNumberRTC;
                    //        grvData.FocusedRowHandle = i;
                    //        return false;
                    //    }
                    //}
                }
            }
            return true;
        }
        #endregion

        #region SỰ KIỆN ENTER XUỐNG DÒNG
        void setFocusCell(int indexRow)
        {
            // grvData.FocusedRowHandle = indexRow;
            // grvData.FocusedColumn = colSerialNumber;

            //// grvData.FocusedColumn = colSerialNumberRTC;
            // grvData.ShowEditor();


            if (grvData.FocusedColumn == colSerialNumber)
            {
                grvData.FocusedRowHandle = indexRow;
                grvData.FocusedColumn = colSerialNumber;
            }
            else
            {
                grvData.FocusedRowHandle = indexRow;
                grvData.FocusedColumn = colSerialNumberRTC;
            }

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
            //====================== lee min khooi update 30/11/2024
            //grvData.CloseEditor();
            //setFocusCell(e.RowHandle + 1);
            //=======================================
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddSerialNumber frm = new frmAddSerialNumber();
            frm.billExportDetailModel = billExportDetailModel;
            frm.billImportDetailModel = billImportDetailModel;
            frm.Type = Type;
            frm.quantity = Qty;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //Gán data vào grid

                DataTable data = (DataTable)grdData.DataSource;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string serial = TextUtils.ToString(data.Rows[i]["SerialNumber"]).Trim();
                    if (!string.IsNullOrEmpty(serial)) continue;
                    if (frm.listSerials.Count <= 0) return;
                    //data.Rows[i]["STT"] = frm.listSerials[i].STT;
                    data.Rows[i]["SerialNumber"] = frm.listSerials[0].SerialNumber;
                    data.Rows[i]["SerialNumberRTC"] = frm.listSerials[0].SerialNumberRTC;

                    frm.listSerials.RemoveAt(0);
                }
            }
        }

        public void ReceiveData(List<BillImportDetailSerialNumberModel> lstSerialNumbers)
        {
            frmAddSerialNumber frm = new frmAddSerialNumber();
            var data = (DataTable)grdData.DataSource;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string serial = TextUtils.ToString(data.Rows[i]["SerialNumber"]).Trim();
                if (!string.IsNullOrEmpty(serial)) continue;
                if (frm.listSerials.Count <= 0) return;
                //data.Rows[i]["STT"] = frm.listSerials[i].STT;
                data.Rows[i]["SerialNumber"] = frm.listSerials[0].SerialNumber;
                data.Rows[i]["SerialNumberRTC"] = frm.listSerials[0].SerialNumberRTC;

                frm.listSerials.RemoveAt(0);
            }
        }

        private void frmBillSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string typeText = Type == 1 ? "PhieuNhap" : "PhieuXuat";
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachSerial_{typeText}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

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


        //====================== lee min khooi update 30/11/2024 ======================================
        private void SaveOneRow(int rowHandle)
        {
            grvData.CloseEditor();
            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
            int stt = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colSTT));
            string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colSerialNumber));
            string serialNumberRTC = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colSerialNumberRTC));
            //if (string.IsNullOrWhiteSpace(serialNumber)) return;

            int serialID = 0;
            if (Type == 1)
            {
                BillImportDetailSerialNumberModel serial = SQLHelper<BillImportDetailSerialNumberModel>.FindByID(id);
                if (serial == null) serial = new BillImportDetailSerialNumberModel();

                serial.BillImportDetailID = billImportDetailModel.ID;
                serial.STT = stt;
                serial.SerialNumber = serialNumber;
                serial.SerialNumberRTC = serialNumberRTC;

                if (serial.ID > 0)
                {
                    SQLHelper<BillImportDetailSerialNumberModel>.Update(serial);
                }
                else
                {
                    serial.ID = SQLHelper<BillImportDetailSerialNumberModel>.Insert(serial).ID;
                }

                serialID = serial.ID;
            }
            else
            {
                BillExportDetailSerialNumberModel serial = new BillExportDetailSerialNumberModel();
                if (serial == null) serial = new BillExportDetailSerialNumberModel();

                serial.BillExportDetailID = billExportDetailModel.ID;
                serial.STT = stt;
                serial.SerialNumber = serialNumber;
                serial.SerialNumberRTC = serialNumberRTC;

                if (serial.ID > 0)
                {
                    SQLHelper<BillExportDetailSerialNumberModel>.Update(serial);
                }
                else
                {
                    serial.ID = SQLHelper<BillExportDetailSerialNumberModel>.Insert(serial).ID;
                }
                serialID = serial.ID;
            }

            grvData.SetRowCellValue(rowHandle, colID, serialID);
        }
        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (grvData.RowCount == 0) return;
                int rowHandle = grvData.FocusedRowHandle;
                SaveOneRow(rowHandle);
                setFocusCell(rowHandle + 1);
            }
        }
    }
}
