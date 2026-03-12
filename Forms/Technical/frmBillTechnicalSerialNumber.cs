using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.Fields;
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
    public partial class frmBillTechnicalSerialNumber : _Forms
    {
        public BillImportDetailTechnicalModel modelImportDetail = new BillImportDetailTechnicalModel();
        public BillExportDetailTechnicalModel modelExportDetail = new BillExportDetailTechnicalModel();
        public int Type;
        public int quantity;//ndnhat 01/04/2025
        ArrayList lstIDDelete = new ArrayList();
        public List<string> lstSerialNumberid;//ndnhat 01/04/2025
        public List<BillImportTechDetailSerialModel> lstSerialNumberImport = new List<BillImportTechDetailSerialModel>();
        public List<BillExportTechDetailSerialModel> lstSerialNumberExport = new List<BillExportTechDetailSerialModel>();
        int warehouseID;
        public frmBillTechnicalSerialNumber(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmBillTechnicalSerialNumber_Load(object sender, EventArgs e)
        {
            LoadModulaLocation();
            LoadData(Type);
            LoadAddRow();
            setFocusCell(getFirstRowSpace());
        }


        void LoadModulaLocation()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetModulaLocation","A",new string[] { "@ModulaLocationID", "@Keyword" },new object[] {0,"" });
            cboModulaLocationDetail.ValueMember = "ModulaLocationDetailID";
            cboModulaLocationDetail.DisplayMember = "LocationName";
            cboModulaLocationDetail.DataSource = dt;
        }
        private void LoadData(int Type)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("ID");
            dt.Columns.Add("SerialNumber");
            dt.Columns.Add("ModulaLocationDetailID");

            if (Type == 1) //Nhập phiếu
            {
                if (modelImportDetail.ID <= 0)
                {
                    if (lstSerialNumberImport != null && lstSerialNumberImport.Count > 0)
                    {
                    
                        int stt = 1;
                        foreach (var serial in lstSerialNumberImport)
                        {
                            DataRow row = dt.NewRow();
                            row["STT"] = stt++;
                            row["ID"] = serial.ID;
                            row["SerialNumber"] = serial.SerialNumber;
                            row["ModulaLocationDetailID"] = 0;
                            dt.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    dt = TextUtils.LoadDataFromSP("spGetBillImportTechDetailSerial", "A", new string[] { "@BillImportTechDetail", "@WarehouseID" }, new object[] { modelImportDetail.ID, warehouseID });
                }
            }
            else //Xuất phiếu
            {
                if (modelExportDetail.ID <= 0)
                {
                    if (lstSerialNumberExport != null && lstSerialNumberExport.Count > 0)
                    {
                        int stt = 1;
                        foreach (var serial in lstSerialNumberExport)
                        {
                            DataRow row = dt.NewRow();
                            row["STT"] = stt++;
                            row["ID"] = serial.ID;
                            row["SerialNumber"] = serial.SerialNumber;
                            row["ModulaLocationDetailID"] = 0;
                            dt.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetailSerial", "A", new string[] { "@BillExportTechDetailID", "@WarehouseID" }, new object[] { modelExportDetail.ID, warehouseID });

                }
            }

            grdData.DataSource = dt;
        }
        #region THÊM VÀ XÓA DÒNG
        //Từ động add Row theo số lượng vật tư
        int Qty;
        private void LoadAddRow()
        {
            if (Type == 1) //Phiếu Nhập
            {
                //ndnhat 01/04/2025
                if (modelImportDetail.ID > 0)
                {
                    Qty = quantity;
                }
                else
                {
                    Qty = TextUtils.ToInt(modelImportDetail.Quantity);
                }
            }
            else //Phiếu Xuất
            {
                //ndnhat 01/04/2025
                if (modelExportDetail.ID > 0)
                {
                    Qty = quantity;
                }
                else
                {
                    Qty = TextUtils.ToInt(modelExportDetail.Quantity);
                }
            }    
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
                    if (grvData.RowCount >= TextUtils.ToInt(modelImportDetail.Quantity)) return;
                }
                else  //Phiếu Xuất
                    if (grvData.RowCount >= TextUtils.ToInt(modelExportDetail.Quantity)) return;
                MyLib.AddNewRow(grdData, grvData);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string serialNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colSerialNumber));
            int rowIndex = grvData.GetSelectedRows()[0];
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa SerialNumber '{serialNumber}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (lstSerialNumberImport.Count > 0)
                {
                    lstSerialNumberImport.RemoveAt(rowIndex);

                    for (int i = 0; i < lstSerialNumberImport.Count; i++)
                    {
                        lstSerialNumberImport[i].STT = i + 1; // cập nhật lại giá trị STT
                    }
                }
                if (lstSerialNumberExport.Count > 0)
                {
                    lstSerialNumberExport.RemoveAt(rowIndex);

                    for (int i = 0; i < lstSerialNumberExport.Count; i++)
                    {
                        lstSerialNumberExport[i].STT = i + 1; // cập nhật lại giá trị STT
                    }
                }
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }

            }
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

        bool Save()
        {
            ArrayList checkSerialNumber = new ArrayList();
            // Reset danh sách mỗi lần save
            grvData.CloseEditor();
           

            try
            {
                int id = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    string serialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
                    int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

                    // Kiểm tra serial number rỗng
                    if (string.IsNullOrEmpty(serialNumber))
                    {
                        MessageBox.Show($"Vui lòng nhập Serial Number tại dòng [{STT}]");
                        return false;
                    }

                    // Kiểm tra serial number trùng lặp
                    if (checkSerialNumber.Contains(serialNumber))
                    {
                        MessageBox.Show($"Serial Number tại dòng [{STT}] đã tồn tại");
                        return false;
                    }
                    checkSerialNumber.Add(serialNumber);
                    //ndnhat 01/04/2025
                    if (Type == 1) // Nhập kho
                    {
                        BillImportTechDetailSerialModel serialModel = new BillImportTechDetailSerialModel
                        {
                            STT = STT,
                            SerialNumber = serialNumber,
                            WarehouseID = warehouseID,
                            BillImportTechDetailID = modelImportDetail.ID 
                        };

                        int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                        if (ID > 0)
                        {
                            serialModel.ID = ID;
                            id = SQLHelper<BillImportTechDetailSerialModel>.Update(serialModel).ID;
                            id = serialModel.ID;
                        }
                        else
                        {
                            id = SQLHelper<BillImportTechDetailSerialModel>.Insert(serialModel).ID;
                            lstSerialNumberid.Add(id.ToString());
                        }


                        //UpdateImportExportSerialModula(id, i,Type);
                    }
                    else if (Type == 2) // Xuất kho
                    {

                        BillExportTechDetailSerialModel serialModel = new BillExportTechDetailSerialModel
                        {
                            STT = STT,
                            SerialNumber = serialNumber,
                            WarehouseID = warehouseID,
                            BillExportTechDetailID = modelExportDetail.ID
                        };

                        int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                        if (ID > 0)
                        {
                            serialModel.ID = ID;
                            id = SQLHelper<BillExportTechDetailSerialModel>.Update(serialModel).ID;
                            id = serialModel.ID;
                        }
                        else
                        {
                            id = SQLHelper<BillExportTechDetailSerialModel>.Insert(serialModel).ID;
                            lstSerialNumberid.Add(id.ToString());
                        }
                    }
                    UpdateImportExportSerialModula(id, i, Type);
                }

                // Xử lý xóa nếu có
                if (lstIDDelete.Count > 0)
                {
                    foreach (int a in lstIDDelete)
                    {
                        if (Type == 1)
                        {
                            SQLHelper<BillImportTechDetailSerialModel>.DeleteModelByID(a);
                        }
                        else if (Type == 2)
                        {
                            SQLHelper<BillExportTechDetailSerialModel>.DeleteModelByID(a);
                        }
                        lstSerialNumberid.Remove(a.ToString());
                    }
                }
                //end ndnhat 01/04/2025
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu serial: " + ex.Message);
                return false;
            }
        }


        void UpdateImportExportSerialModula(int serialID,int rowHandle,int type)
        {
            int locationID = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colModulaLocationDetailID));
            var exp1 = new Expression(BillImportDetailSerialNumberModulaLocationModel_Enum.ModulaLocationDetailID, locationID);
            
            if (type == 1)
            {
                var exp2 = new Expression(BillImportDetailSerialNumberModulaLocationModel_Enum.BillImportTechDetailSerialID, serialID);

                var locations = SQLHelper<BillImportDetailSerialNumberModulaLocationModel>.FindByExpression(exp1.And(exp2));
                if (locations.Count > 0) return;

                BillImportDetailSerialNumberModulaLocationModel location = new BillImportDetailSerialNumberModulaLocationModel();

                location.ModulaLocationDetailID = locationID;
                location.Quantity = 1;
                location.BillImportTechDetailSerialID = serialID;


                SQLHelper<BillImportDetailSerialNumberModulaLocationModel>.Insert(location);
            }
            else if (type == 2)
            {
                var exp2 = new Expression(BillExportDetailSerialNumberModulaLocationModel_Enum.BillExportTechDetailSerialID, serialID);

                var locations = SQLHelper<BillExportDetailSerialNumberModulaLocationModel>.FindByExpression(exp1.And(exp2));
                if (locations.Count > 0) return;

                BillExportDetailSerialNumberModulaLocationModel location = new BillExportDetailSerialNumberModulaLocationModel();

                location.ModulaLocationDetailID = locationID;
                location.Quantity = 1;
                location.BillExportTechDetailSerialID = serialID;

                SQLHelper<BillExportDetailSerialNumberModulaLocationModel>.Insert(location);
            }
        }

        private void frmBillTechnicalSerialNumber_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            lstSerialNumberImport.Clear();
            lstSerialNumberExport.Clear();
        }
    }
}
