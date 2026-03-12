using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Technical
{
    public partial class frmBillExportTechDetail_New : _Forms
    {
        public bool IsEdit;
        public BillExportTechnicalModel billExport = new BillExportTechnicalModel();
        ArrayList lstIDDelete = new ArrayList();
        string statusOld = "";
        public int IDDetail;

        DataTable dtProduct = new DataTable();
        DataTable dtAll = new DataTable();
        public int warehouseID;
        List<BillExportTechDetailSerialModel> lstSerial = new List<BillExportTechDetailSerialModel>();//ndnhat 02/04/2025
        #region Khai báo thêm dữ liệu mượn ncc VTNam 16/08/2024
        public bool openFrmSummary;
        public DataTable dtLoad = new DataTable();
        public int customerID, deliverID, supplierID;
        public string BillCode;
        bool isSave = false;//ndnhat 02/04/2025
        List<string> oldTempSerial = new List<string>();//ndnhat 02/04/2025
        List<string> tempSerial = new List<string>();//ndnhat 02/04/2025
        #endregion

        public frmBillExportTechDetail_New()
        {
            InitializeComponent();

        }
        public frmBillExportTechDetail_New(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmBillExportTechDetail_Load(object sender, EventArgs e)
        {
            //if (IsEdit)
            //{
            //    btnSave.Enabled = btnAddProduct.Enabled = btnSaveNew.Enabled = btnAddProject.Enabled = false;
            //}

            if (billExport.Status == 1 && !Global.IsAdmin)
            {
                btnSave.Enabled = btnAddProduct.Enabled = btnSaveNew.Enabled = btnAddProject.Enabled = false;
            }
            loadCboApprover();
            loadNCC();
            LoadCustomer();
            loadUser();
            loadBillType();
            loadBillExportDetail();
            loadProduct();
            LoadProject();
        }
        void loadCboApprover()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboApprover.Properties.ValueMember = "ID";
            cboApprover.Properties.DisplayMember = "FullName";
            cboApprover.Properties.DataSource = dt;

            cboApprover.EditValue = 54;
        }
        private void loadNCC()
        {
            cboNCC.Properties.DataSource = SQLHelper<SupplierSaleModel>.FindAll();
            cboNCC.Properties.ValueMember = "ID";
            cboNCC.Properties.DisplayMember = "NameNCC";
        }
        void LoadProject()
        {
            List<ProjectModel> lst = SQLHelper<ProjectModel>.FindAll();
            cboProjectID.DataSource = lst;
            cboProjectID.DisplayMember = "ProjectCode";
            cboProjectID.ValueMember = "ID";
        }
        void LoadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(x => x.ID).ToList();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;
        }

        void loadBillType()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Application.StartupPath, "billtype.txt"));
            cboBillType.Items.AddRange(lines);
            cboBillType.SelectedIndex = 0;
        }
        void loadUser()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select("Select * from Users");
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });

            cbUser.Properties.DataSource = dt;
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DisplayMember = "FullName";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            isSave = true;
            //loadBilllNumber();
            if (saveData())
            { this.DialogResult = DialogResult.OK; }
        }

        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        //bool saveData()
        //{
        //    //RecheckQty();
        //    if (!ValidateForm()) return false;
        //    // focus: trỏ đến -> lưu và cất đi luôn
        //    grvDetailTechExport.Focus();
        //    txtCode.Focus();
        //    billExport.Status = 0;
        //    billExport.Code = txtCode.Text.Trim();
        //    //billExport.Addres = txtAddress.Text.Trim();
        //    //billExport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
        //    billExport.SupplierName = TextUtils.ToString(txtNCC.Text);
        //    billExport.Deliver = TextUtils.ToString(txtLienHe.Text);
        //    billExport.ExpectedDate = dtpExpectedDate.Value;


        //    //billExport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
        //    billExport.Receiver = txtNguoiNhan.Text;
        //    billExport.ReceiverID = TextUtils.ToInt(cbUser.EditValue);
        //    billExport.CreatedDate = dtpCreatedDate.Value;
        //    billExport.CheckAddHistoryProductRTC = CkbAddHistoryProductRTC.Checked;
        //    billExport.ProjectName = txtProject.Text;
        //    billExport.WarehouseID = warehouseID;

        //    // billExport.Image = picSign.ImageLocation;
        //    // billExport.CustomerName = TextUtils.ToString(txtCustomer.Text);
        //    billExport.BillType = cboBillType.SelectedIndex;

        //    //if (cboBillType.SelectedIndex == 0)
        //    //{
        //    //    billExport.BillType = false;
        //    //}
        //    //else if (cboBillType.SelectedIndex == 1)
        //    //{
        //    //    billExport.BillType = true;
        //    //}

        //    billExport.WarehouseType = txtWarehouseType.Text.Trim();
        //    billExport.SupplierSaleID = TextUtils.ToInt(cboNCC.EditValue);

        //    if (billExport.ID > 0)
        //    {
        //        BillExportTechnicalBO.Instance.Update(billExport);
        //    }
        //    else
        //    {
        //        billExport.ID = (int)BillExportTechnicalBO.Instance.Insert(billExport);
        //    }

        //    //// Cất Detail
        //    //for (int i = 0; i < grvDetailTechExport.RowCount; i++)
        //    //{
        //    //    BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
        //    //    billExportDetail.ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colID));
        //    //    billExportDetail.BillExportTechID = billExport.ID;//Liên kết bảng Nhập Xuất
        //    //    billExportDetail.ProductID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));//ID Sản phẩm
        //    //    billExportDetail.Quantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
        //    //    billExportDetail.Note = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colNote));
        //    //    billExportDetail.STT = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colSTT));
        //    //    billExportDetail.TotalQuantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colTotalQuantity));
        //    //    billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
        //    //    billExportDetail.UnitID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colUnitName));
        //    //    billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
        //    //    billExportDetail.HistoryProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
        //    //    billExportDetail.ProductRTCQRCodeID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
        //    //    //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));
        //    //    billExportDetail.WarehouseID = warehouseID;

        //    //    if (billExportDetail.ID > 0)
        //    //    {
        //    //        BillExportDetailTechnicalBO.Instance.Update(billExportDetail);
        //    //    }
        //    //    else
        //    //    {
        //    //        billExportDetail.ID = (int)BillExportDetailTechnicalBO.Instance.Insert(billExportDetail);
        //    //        if (lstSerialNumbers.Count > i)
        //    //        {
        //    //            foreach (var x in lstSerialNumbers[i])
        //    //            {
        //    //                x.BillExportTechDetailID = billExportDetail.ID;
        //    //                BillExportDetailTechnicalBO.Instance.Insert(x);
        //    //            }
        //    //        }
        //    //    }
        //    //    //31102022  //update status trong bảng ProductRTCQRCode status=3(Xuất kho)
        //    //    TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{billExportDetail.ProductRTCQRCodeID}','{3}'");
        //    //}

        //    //Khánh update thêm lịch sử khi save
        //    List<int> listHistoryID = new List<int>();

        //    // Cất Detail
        //    for (int i = 0; i < grvDetailTechExport.RowCount; i++)
        //    {
        //        int productRTCId = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
        //        if (productRTCId <= 0)
        //        {
        //            continue;
        //        }
        //        BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
        //        billExportDetail.ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colID));
        //        billExportDetail.BillExportTechID = billExport.ID;//Liên kết bảng Nhập Xuất
        //        billExportDetail.ProductID = productRTCId;//ID Sản phẩm
        //        billExportDetail.Quantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
        //        billExportDetail.Note = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colNote));
        //        billExportDetail.STT = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colSTT));
        //        billExportDetail.TotalQuantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colTotalQuantity));
        //        billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
        //        billExportDetail.UnitID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colUnitName));
        //        billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
        //        billExportDetail.HistoryProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
        //        billExportDetail.ProductRTCQRCodeID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
        //        //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));
        //        billExportDetail.WarehouseID = warehouseID;

        //        if (billExportDetail.ID > 0)
        //        {
        //            BillExportDetailTechnicalBO.Instance.Update(billExportDetail);
        //        }
        //        else
        //        {
        //            billExportDetail.ID = (int)BillExportDetailTechnicalBO.Instance.Insert(billExportDetail);
        //            if (lstSerialNumbers.Count > i)
        //            {
        //                foreach (var x in lstSerialNumbers[i])
        //                {
        //                    x.BillExportTechDetailID = billExportDetail.ID;
        //                    BillExportDetailTechnicalBO.Instance.Insert(x);
        //                }
        //            }
        //        }

        //        //Lt.Anh update add danh sách id lịch sử mượn vào list
        //        int historyID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
        //        if (historyID > 0)
        //        {
        //            listHistoryID.Add(historyID);
        //        }
        //        int ProductRTCQRCodeID = 0;// TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductRTCQRCodeID));
        //        int detailID = billExportDetail.ID;


        //        if (billExport.CheckAddHistoryProductRTC)
        //        {

        //            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetailSerial", "A", new string[] { "@BillExportTechDetailID", "@WarehouseID" }, new object[] { detailID, warehouseID });
        //            var exp1 = new Expression("ProductRTCID", TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID)));
        //            var exp2 = new Expression("BillExportTechnicalID", billExport.ID);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int j = 0; j < dt.Rows.Count; j++)
        //                {
        //                    HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        //                    var exp3 = new Expression("ProductRTCQRCode", TextUtils.ToString(dt.Rows[j]["SerialNumber"]));
        //                    var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
        //                    if (his != null)
        //                    {
        //                        oHistoryModel = his;
        //                        if (oHistoryModel.Status == 0) return false;
        //                    }
        //                    oHistoryModel.ProductRTCQRCode = TextUtils.ToString(dt.Rows[j]["SerialNumber"]);
        //                    oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
        //                    oHistoryModel.DateBorrow = DateTime.Now;
        //                    oHistoryModel.DateReturnExpected = dtpExpectedDate.Value;
        //                    oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
        //                    oHistoryModel.Note += "Phiếu xuất " + billExport.Code + " - " + billExport.Note;
        //                    oHistoryModel.Project = txtProject.Text;
        //                    oHistoryModel.Status = 1;
        //                    oHistoryModel.BillExportTechnicalID = billExport.ID;
        //                    oHistoryModel.NumberBorrow = 1;// TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
        //                    oHistoryModel.WarehouseID = warehouseID;
        //                    if (oHistoryModel.ID > 0)
        //                    {
        //                        HistoryProductRTCBO.Instance.Update(oHistoryModel);
        //                    }
        //                    else
        //                    {
        //                        HistoryProductRTCBO.Instance.Insert(oHistoryModel);
        //                    }

        //                    //update status trong bảng ProductRTCQRCode status=3(Xuất Kho)
        //                    if (ProductRTCQRCodeID > 0)
        //                    {
        //                        TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode,
        //                            new string[] { "@ProductRTCQRCodeID", "@Status", "@ProductRTCQRCode" },
        //                                new object[] { ProductRTCQRCodeID, 3, oHistoryModel.ProductRTCQRCode });
        //                        //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{oHistoryModel.ProductRTCQRCodeID}','{3}'");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //Duyệt + thêm phiếu mượn
        //                HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        //                var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
        //                if (his != null)
        //                {
        //                    oHistoryModel = his;
        //                    if (oHistoryModel.Status == 0) return false;
        //                }
        //                oHistoryModel.ProductRTCQRCodeID = 0;
        //                oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
        //                oHistoryModel.DateBorrow = DateTime.Now;
        //                oHistoryModel.DateReturnExpected = dtpExpectedDate.Value;
        //                oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
        //                oHistoryModel.Note += "Phiếu xuất " + billExport.Code + " - " + billExport.Note;
        //                oHistoryModel.Project = txtProject.Text;
        //                oHistoryModel.Status = 1;
        //                oHistoryModel.BillExportTechnicalID = billExport.ID; ;
        //                oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
        //                oHistoryModel.WarehouseID = warehouseID;
        //                if (oHistoryModel.ID > 0)
        //                {
        //                    HistoryProductRTCBO.Instance.Update(oHistoryModel);
        //                }
        //                else
        //                {
        //                    HistoryProductRTCBO.Instance.Insert(oHistoryModel);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", billExportDetail.ID);
        //        }
        //        //31102022  //update status trong bảng ProductRTCQRCode status=3(Xuất kho)
        //        TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{billExportDetail.ProductRTCQRCodeID}','{3}'");
        //    }
        //    //if (lstIDDelete.Count > 0)
        //    //    BillExportDetailTechnicalBO.Instance.Delete(lstIDDelete);
        //    if (lstIDDelete.Count > 0)
        //    {
        //        foreach (var id in lstIDDelete)
        //        {
        //            var detail = (BillExportDetailTechnicalModel)BillExportDetailTechnicalBO.Instance.FindByPK(TextUtils.ToInt(id));
        //            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetailSerial", "A", new string[] { "@BillExportTechDetailID", "@WarehouseID" }, new object[] { detail.ID, warehouseID });
        //            var exp1 = new Expression("ProductRTCID", detail.ProductID);
        //            var exp2 = new Expression("BillExportTechnicalID", billExport.ID);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int j = 0; j < dt.Rows.Count; j++)
        //                {
        //                    HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        //                    var exp3 = new Expression("ProductRTCQRCode", TextUtils.ToString(dt.Rows[j]["SerialNumber"]));
        //                    var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
        //                    if (his != null)
        //                    {
        //                        oHistoryModel = his;
        //                        if (oHistoryModel.Status == 0) continue;
        //                    }
        //                    else
        //                    {
        //                        continue;
        //                    }
        //                    oHistoryModel.NumberBorrow = 0;// TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
        //                    oHistoryModel.WarehouseID = warehouseID;
        //                    oHistoryModel.IsDelete = true;
        //                    if (oHistoryModel.ID > 0)
        //                    {
        //                        HistoryProductRTCBO.Instance.Update(oHistoryModel);
        //                    }
        //                    //else
        //                    //{
        //                    //    HistoryProductRTCBO.Instance.Insert(oHistoryModel);
        //                    //}
        //                    //update status trong bảng ProductRTCQRCode status=3(Xuất Kho)
        //                    //if (ProductRTCQRCodeID > 0)
        //                    //{
        //                    //    TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode,
        //                    //        new string[] { "@ProductRTCQRCodeID", "@Status", "@ProductRTCQRCode" },
        //                    //            new object[] { ProductRTCQRCodeID, 3, oHistoryModel.ProductRTCQRCode });
        //                    //    //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{oHistoryModel.ProductRTCQRCodeID}','{3}'");
        //                    //}
        //                }
        //            }
        //            else
        //            {
        //                //Duyệt + thêm phiếu mượn
        //                HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        //                var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
        //                if (his != null)
        //                {
        //                    oHistoryModel = his;
        //                    if (oHistoryModel.Status == 0) continue;
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //                oHistoryModel.NumberBorrow = 0;
        //                //oHistoryModel.WarehouseID = warehouseID;
        //                oHistoryModel.IsDelete = true;
        //                if (oHistoryModel.ID > 0)
        //                {
        //                    HistoryProductRTCBO.Instance.Update(oHistoryModel);
        //                }
        //                //else
        //                //{
        //                //    HistoryProductRTCBO.Instance.Insert(oHistoryModel);
        //                //}
        //            }
        //        }
        //        BillExportDetailTechnicalBO.Instance.Delete(lstIDDelete);
        //    }
        //    return true;
        //}

        bool saveData()
        {
            try
            {

                grvDetailTechExport.CloseEditor();
                //RecheckQty();
                // focus: trỏ đến -> lưu và cất đi luôn
                grvDetailTechExport.Focus();
                txtCode.Focus();
                if (!ValidateForm()) return false;
                billExport.Status = 0;
                billExport.Code = txtCode.Text.Trim();
                //billExport.Addres = txtAddress.Text.Trim();
                //billExport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
                billExport.SupplierName = TextUtils.ToString(txtNCC.Text);
                billExport.Deliver = TextUtils.ToString(txtLienHe.Text);
                billExport.ExpectedDate = dtpExpectedDate.Value;


                //billExport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
                billExport.Receiver = txtNguoiNhan.Text;
                billExport.ReceiverID = TextUtils.ToInt(cbUser.EditValue);
                billExport.CreatedDate = dtpCreatedDate.Value;
                //billExport.CheckAddHistoryProductRTC = CkbAddHistoryProductRTC.Checked;
                billExport.CheckAddHistoryProductRTC = cboBillType.SelectedIndex == 1;
                billExport.ProjectName = txtProject.Text;
                billExport.WarehouseID = warehouseID;

                // billExport.Image = picSign.ImageLocation;
                // billExport.CustomerName = TextUtils.ToString(txtCustomer.Text);
                billExport.BillType = cboBillType.SelectedIndex;

                //if (cboBillType.SelectedIndex == 0)
                //{
                //    billExport.BillType = false;
                //}
                //else if (cboBillType.SelectedIndex == 1)
                //{
                //    billExport.BillType = true;
                //}

                billExport.WarehouseType = txtWarehouseType.Text.Trim();
                billExport.SupplierSaleID = TextUtils.ToInt(cboNCC.EditValue);
                billExport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                billExport.ApproverID = TextUtils.ToInt(cboApprover.EditValue);

                if (billExport.ID > 0)
                {
                    SQLHelper<BillExportTechnicalModel>.Update(billExport);
                }
                else
                {
                    //billExport.ID = (int)BillExportTechnicalBO.Instance.Insert(billExport);

                    billExport.BillDocumentExportType = 2;

                    billExport.ID = SQLHelper<BillExportTechnicalModel>.Insert(billExport).ID; // cũ


                    //add ho so chứng từ
                    List<DocumentExportModel> listID = SQLHelper<DocumentExportModel>.SqlToList("SELECT ID FROM DocumentExport WHERE IsDeleted <> 1");
                    if (listID.Count > 0)
                    {
                        for (int i = 0; i < listID.Count; i++)
                        {
                            int DocumentExportID = listID[i].ID;

                            BillDocumentExportTechnicalModel techModel = new BillDocumentExportTechnicalModel();
                            techModel.BillExportTechnicalID = billExport.ID;
                            techModel.DocumentExportID = DocumentExportID;
                            SQLHelper<BillDocumentExportTechnicalModel>.Insert(techModel);
                        }
                    }
                }

                //// Cất Detail
                //for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                //{
                //    BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
                //    billExportDetail.ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colID));
                //    billExportDetail.BillExportTechID = billExport.ID;//Liên kết bảng Nhập Xuất
                //    billExportDetail.ProductID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));//ID Sản phẩm
                //    billExportDetail.Quantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                //    billExportDetail.Note = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colNote));
                //    billExportDetail.STT = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colSTT));
                //    billExportDetail.TotalQuantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colTotalQuantity));
                //    billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
                //    billExportDetail.UnitID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colUnitName));
                //    billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
                //    billExportDetail.HistoryProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
                //    billExportDetail.ProductRTCQRCodeID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
                //    //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));
                //    billExportDetail.WarehouseID = warehouseID;

                //    if (billExportDetail.ID > 0)
                //    {
                //        BillExportDetailTechnicalBO.Instance.Update(billExportDetail);
                //    }
                //    else
                //    {
                //        billExportDetail.ID = (int)BillExportDetailTechnicalBO.Instance.Insert(billExportDetail);
                //        if (lstSerialNumbers.Count > i)
                //        {
                //            foreach (var x in lstSerialNumbers[i])
                //            {
                //                x.BillExportTechDetailID = billExportDetail.ID;
                //                BillExportDetailTechnicalBO.Instance.Insert(x);
                //            }
                //        }
                //    }
                //    //31102022  //update status trong bảng ProductRTCQRCode status=3(Xuất kho)
                //    TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{billExportDetail.ProductRTCQRCodeID}','{3}'");
                //}

                //Khánh update thêm lịch sử khi save
                List<int> listHistoryID = new List<int>();
                string sql = $"UPDATE HistoryProductRTC SET IsDelete = 1,UpdatedBy = '{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE BillExportTechnicalID = {billExport.ID}";
                TextUtils.ExcuteSQL(sql);

                // Cất Detail
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    int productRTCId = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                    if (productRTCId <= 0)
                    {
                        continue;
                    }
                    BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
                    billExportDetail.ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colID));
                    billExportDetail.BillExportTechID = billExport.ID;//Liên kết bảng Nhập Xuất
                    billExportDetail.ProductID = productRTCId;//ID Sản phẩm
                    billExportDetail.Quantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                    billExportDetail.Note = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colNote));
                    billExportDetail.STT = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colSTT));
                    billExportDetail.TotalQuantity = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colTotalQuantity));
                    billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProjectID));
                    billExportDetail.UnitID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colUnitName));
                    billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
                    billExportDetail.HistoryProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
                    billExportDetail.ProductRTCQRCodeID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
                    //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));
                    billExportDetail.WarehouseID = warehouseID;

                    // nam update
                    billExportDetail.BillImportDetailTechnicalID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colBillImportDetailTechnicalID));

                    if (billExportDetail.ID > 0)
                    {
                        SQLHelper<BillExportDetailTechnicalModel>.Update(billExportDetail);
                        string[] serialss = grvDetailTechExport.GetRowCellDisplayText(i, colJoinedIDs).Split(',');
                        foreach (string item in serialss)
                        {
                            int id = TextUtils.ToInt(item);
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"BillExportTechDetailID", billExportDetail.ID}
                            };
                            SQLHelper<BillExportTechDetailSerialModel>.UpdateFieldsByID(data, id);
                        }

                    }
                    else
                    {
                        billExportDetail.ID = SQLHelper<BillExportDetailTechnicalModel>.Insert(billExportDetail).ID;
                        string[] serialss = grvDetailTechExport.GetRowCellDisplayText(i, colJoinedIDs).Split(',');
                        foreach (string item in serialss)
                        {
                            int id = TextUtils.ToInt(item);
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"BillExportTechDetailID", billExportDetail.ID}
                            };
                            SQLHelper<BillExportTechDetailSerialModel>.UpdateFieldsByID(data, id);
                        }
                    }

                    //Khánh update thêm inventory  18/01/2024
                    var exp11 = new Expression("ProductRTCID", billExportDetail.ProductID);
                    var exp12 = new Expression("WarehouseID", warehouseID);
                    var check = InventoryDemoBO.Instance.FindByExpression(exp11.And(exp12));
                    if (check.Count <= 0)
                    {
                        InventoryDemoModel inventory = new InventoryDemoModel();
                        inventory.ProductRTCID = billExportDetail.ProductID;
                        inventory.WarehouseID = warehouseID;
                        InventoryDemoBO.Instance.Insert(inventory);
                    }

                    //Lt.Anh update add danh sách id lịch sử mượn vào list
                    int historyID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
                    if (historyID > 0)
                    {
                        listHistoryID.Add(historyID);
                    }
                    int ProductRTCQRCodeID = 0;// TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductRTCQRCodeID));
                    int detailID = billExportDetail.ID;


                    if (billExport.CheckAddHistoryProductRTC == true)
                    {

                        DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetailSerial", "A", new string[] { "@BillExportTechDetailID", "@WarehouseID" }, new object[] { detailID, warehouseID });
                        var exp1 = new Expression("ProductRTCID", TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID)));
                        var exp2 = new Expression("BillExportTechnicalID", billExport.ID);

                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
                                var exp3 = new Expression("ProductRTCQRCode", TextUtils.ToString(dt.Rows[j]["SerialNumber"]));
                                var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
                                if (his != null)
                                {
                                    oHistoryModel = his;
                                    if (oHistoryModel.Status == 0) return false;
                                }
                                oHistoryModel.ProductRTCQRCode = TextUtils.ToString(dt.Rows[j]["SerialNumber"]);
                                oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                                oHistoryModel.DateBorrow = billExport.CreatedDate;
                                oHistoryModel.DateReturnExpected = dtpExpectedDate.Value;
                                oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
                                oHistoryModel.Note = "Phiếu xuất " + billExport.Code + (string.IsNullOrWhiteSpace(billExportDetail.Note) ? "" : ":\n" + billExportDetail.Note);
                                oHistoryModel.Project = txtProject.Text;
                                oHistoryModel.Status = 1;
                                oHistoryModel.BillExportTechnicalID = billExport.ID;
                                oHistoryModel.NumberBorrow = 1;// TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
                                oHistoryModel.WarehouseID = warehouseID;
                                oHistoryModel.IsDelete = false;
                                if (oHistoryModel.ID > 0)
                                {
                                    SQLHelper<HistoryProductRTCModel>.Update(oHistoryModel);
                                }
                                else
                                {
                                    SQLHelper<HistoryProductRTCModel>.Insert(oHistoryModel);
                                }

                                //update status trong bảng ProductRTCQRCode status=3(Xuất Kho)
                                if (ProductRTCQRCodeID > 0)
                                {
                                    TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode,
                                                                new string[] { "@ProductRTCQRCodeID", "@Status", "@ProductRTCQRCode" },
                                                                new object[] { ProductRTCQRCodeID, 3, oHistoryModel.ProductRTCQRCode });
                                    //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{oHistoryModel.ProductRTCQRCodeID}','{3}'");
                                }
                            }
                        }
                        else
                        {
                            //Duyệt + thêm phiếu mượn
                            HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
                            var his = SQLHelper<HistoryProductRTCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                            if (his != null)
                            {
                                oHistoryModel = his;
                                if (oHistoryModel.Status == 0) return false;
                            }
                            oHistoryModel.ProductRTCQRCodeID = 0;
                            oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                            oHistoryModel.DateBorrow = billExport.CreatedDate;
                            oHistoryModel.DateReturnExpected = dtpExpectedDate.Value;
                            oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
                            oHistoryModel.Note = "Phiếu xuất " + billExport.Code + (string.IsNullOrWhiteSpace(billExportDetail.Note) ? "" : ":\n" + billExportDetail.Note);
                            oHistoryModel.Project = txtProject.Text;
                            oHistoryModel.Status = 1;
                            oHistoryModel.BillExportTechnicalID = billExport.ID; ;
                            oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                            oHistoryModel.WarehouseID = warehouseID;
                            oHistoryModel.IsDelete = false;
                            if (oHistoryModel.ID > 0)
                            {
                                SQLHelper<HistoryProductRTCModel>.Update(oHistoryModel);
                            }
                            else
                            {
                                SQLHelper<HistoryProductRTCModel>.Insert(oHistoryModel);
                            }
                        }
                    }
                    //else
                    //{
                    //    //HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", billExportDetail.ID);

                    //    string sqlUpdate = $"UPDATE HistoryProductRTC SET IsDelete = 1,UpdatedBy = '{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE BillExportTechnicalID = {billExport.ID}";
                    //    TextUtils.ExcuteSQL(sqlUpdate);
                    //}

                    //31102022  //update status trong bảng ProductRTCQRCode status=3(Xuất kho)
                    //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{billExportDetail.ProductRTCQRCodeID}','{3}'");

                    //TextUtils.ExcuteProcedure("spUpdateStatusProductRTCQRCode", new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { billExportDetail.ProductRTCQRCodeID, 3 });

                    foreach (var item in lstSerial)
                    {
                        TextUtils.ExcuteProcedure("spUpdateStatusProductRTCQRCode", new string[] { "@ProductRTCQRCode", "@Status" }, new object[] { item.SerialNumber, 3 });
                    }

                }
                if (lstIDDelete.Count > 0)
                {
                    foreach (var item in lstIDDelete)
                    {
                        int id = TextUtils.ToInt(item);
                        SQLHelper<BillExportDetailTechnicalModel>.DeleteModelByID(id);
                    }

                }

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
                return false;
            }
        }

        /// <summary>
        /// hàm kiểm tra thông tin nhập trước khi save
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            string Billcode = txtCode.Text.Trim();
            DataTable dt = TextUtils.Select($"select top 1 ID from BillExportTechnical where Code = '{Billcode}' and ID <> {billExport.ID} and WarehouseID = {warehouseID}");
            if (billExport.ID > 0)
            {
                //if (Billcode.Contains("PM"))
                //{
                //    Billcode = Billcode.Substring(2);
                //}
                //else if (Billcode.Contains("PXK") || Billcode.Contains("PCT"))
                //{
                //    Billcode = Billcode.Substring(3);
                //}
                ////int strID = billExport.ID;
                ////dt = TextUtils.Select($"select top 1 ID from BillExport where Code LIKE '%{Billcode}%' and ID <> {strID}");
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("Số phiếu này đã tồn tại.\nVui lòng Load lại Số phiếu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            else
            {
                //dt = TextUtils.Select("select top 1 ID from BillExport where Code = '" + txtCode.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    loadBilllNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else if (txtNCC.Text.Trim() == "")
            //{
            //    MessageBox.Show("Xin hãy điền nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //else if (cboCustomer.Text.Trim() == "" && cboCustomer.Enabled == true)
            //{
            //    MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (TextUtils.ToInt(cboNCC.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            if (txtLienHe.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn người liên hệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (txtNguoiNhan.Text == "")
            {
                MessageBox.Show("Chưa có thông tin người nhận.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cboBillType.Text == "")
            {
                MessageBox.Show("Xin hãy chọn loại phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cboNCC.EditValue) <= 0 && TextUtils.ToInt(cboCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp hoặc Khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (TextUtils.ToInt(cboApprover.EditValue) <= 0)
            {
                MessageBox.Show("Xin hãy chọn Người duyệt.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            //grvDetailTechExport.CloseEditor();
            //for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            //{
            //    int quantity = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colQuantity));

            //    int productID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colProductID));

            //    var productSelected = (DataRowView)cboProduct.GetRowByKeyValue(productID);
            //    int inventoryReal = 0;
            //    if (productSelected != null) inventoryReal = TextUtils.ToInt(productSelected["InventoryReal"]);

            //    if (quantity > inventoryReal)
            //    {

            //        MessageBox.Show($"Số lượng tồn kho chỉ còn {inventoryReal}!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            return true;
        }

        /// <summary>
        /// hàm dùng load số phiếu
        /// </summary>
        //void loadBilllNumber()
        //{

        //    int so = 0;
        //    //string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
        //    //string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
        //    //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);

        //    string month = TextUtils.ToString(dtpCreatedDate.Value.Month);
        //    if (TextUtils.ToInt(month) < 10)
        //    {
        //        month = "0" + month;
        //    }

        //    string day = TextUtils.ToString(dtpCreatedDate.Value.Day);
        //    if (TextUtils.ToInt(day) < 10)
        //    {
        //        day = "0" + day;
        //    }

        //    string year = TextUtils.ToString(dtpCreatedDate.Value.Year).Substring(2);

        //    string date = year + month + day;

        //    string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExportTechnical Where WarehouseID = {warehouseID} AND Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} and w ORDER BY ID DESC"));


        //    if (Billcode.Contains("PMD"))
        //    {
        //        Billcode = Billcode.Substring(3);
        //    }
        //    else if (Billcode.Contains("PXKD"))
        //    {
        //        Billcode = Billcode.Substring(4);
        //    }


        //    if (billExport.ID == 0)
        //    {
        //        if (Billcode == "")
        //        {
        //            txtCode.Text = "PXKD" + date + "001";
        //            return;
        //        }
        //        else
        //            so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3));

        //        if (so == 0)
        //        {
        //            if (cboBillType.SelectedIndex == 0)
        //            {
        //                txtCode.Text = "PXKD" + date + "001";
        //            }
        //            else
        //                txtCode.Text = "PMD" + date + "001";
        //            return;
        //        }
        //        else
        //        {
        //            string dem = TextUtils.ToString(so + 1);
        //            for (int i = 0; dem.Length < 3; i++)
        //            {
        //                dem = "0" + dem;
        //            }
        //            if (cboBillType.SelectedIndex == 0)
        //            {
        //                txtCode.Text = "PXKD" + date + TextUtils.ToString(dem);
        //            }
        //            else
        //            {
        //                txtCode.Text = "PMD" + date + TextUtils.ToString(dem);
        //            }
        //        }
        //    }

        //}

        void loadBilllNumber()  //Khánh update 17/04/23
        {

            if (billExport.ID > 0 && !Global.IsAdmin) return;
            int billtype = cboBillType.SelectedIndex;
            txtCode.Text = TextUtils.GetBillCode("BillExportTechnical", billtype);
            return;

            int so = 0;
            //string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            //string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);

            string month = TextUtils.ToString(dtpCreatedDate.Value.Month);
            if (TextUtils.ToInt(month) < 10)
            {
                month = "0" + month;
            }

            string day = TextUtils.ToString(dtpCreatedDate.Value.Day);
            if (TextUtils.ToInt(day) < 10)
            {
                day = "0" + day;
            }

            string year = TextUtils.ToString(dtpCreatedDate.Value.Year).Substring(2);

            string date = year + month + day;

            //string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExportTechnical Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} and WarehouseID = {warehouseID} ORDER BY ID DESC"));
            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExportTechnical Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));


            if (Billcode.Contains("PMD"))
            {
                Billcode = Billcode.Substring(3);
            }
            else if (Billcode.Contains("PXKD"))
            {
                Billcode = Billcode.Substring(4);
            }


            if (billExport.ID == 0)
            {
                if (Billcode == "")
                {
                    txtCode.Text = "PXKD" + date + "001";
                    return;
                }
                else
                    so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3));

                if (so == 0)
                {
                    if (cboBillType.SelectedIndex == 0)
                    {
                        txtCode.Text = "PXKD" + date + "001";
                    }
                    else
                        txtCode.Text = "PXKD" + date + "001";
                    return;
                }
                else
                {
                    string dem = TextUtils.ToString(so + 1);
                    for (int i = 0; dem.Length < 3; i++)
                    {
                        dem = "0" + dem;
                    }
                    if (cboBillType.SelectedIndex == 0)
                    {
                        txtCode.Text = "PXKD" + date + TextUtils.ToString(dem);
                    }
                    else
                    {
                        txtCode.Text = "PXKD" + date + TextUtils.ToString(dem);
                    }
                }
            }

        }



        /// <summary>
        /// load bill Export Detail
        /// </summary>
        private void loadBillExportDetail()
        {
            //grvDetailTechExport.CloseEditor();

            txtLienHe.Text = Global.AppFullName;
            if (warehouseID == 2) txtLienHe.Text = "Nguyễn Thị Phương Thủy";

            if (billExport.ID > 0)
            {
                txtCode.Text = billExport.Code;

                txtNCC.Text = billExport.SupplierName;
                txtProject.Text = billExport.ProjectName;
                txtLienHe.Text = billExport.Deliver;
                txtNguoiNhan.Text = billExport.Receiver;
                CkbAddHistoryProductRTC.Checked = billExport.CheckAddHistoryProductRTC == true;
                cbUser.EditValue = billExport.ReceiverID;
                dtpExpectedDate.Value = TextUtils.ToDate5(billExport.ExpectedDate);
                dtpCreatedDate.Value = TextUtils.ToDate5(billExport.CreatedDate);
                cboBillType.SelectedIndex = TextUtils.ToInt(billExport.BillType);

                cboNCC.EditValue = billExport.SupplierSaleID;
                cboCustomer.EditValue = billExport.CustomerID;
                cboApprover.EditValue = billExport.ApproverID;

                //if (!billExport.BillType)
                //{
                //    cboBillType.SelectedIndex = 0;
                //}
                //else if (billExport.BillType)
                //{
                //    cboBillType.SelectedIndex = 1;
                //}
            }
            else
            {
                loadBilllNumber();
            }


            dtAll = TextUtils.GetDataTableFromSP(StoreProcedures.spGetBillExportTechDetail_New, new string[] { "@ID" }, new object[] { billExport.ID });//spGetBillExportTechDetail
            grdData.DataSource = dtAll;
            //if (dtAll.Rows.Count == 0) return;
            dtpCreatedDate.Text = TextUtils.ToString(billExport.CreatedDate);


            if (openFrmSummary == true)
            {
                grdData.DataSource = dtLoad;
                cbUser.EditValue = deliverID;
                cboNCC.EditValue = supplierID;
                //txtCode.Text = BillCode;
                cboCustomer.EditValue = customerID;


            }
        }


        /// <summary>
        /// giá trị cột TotalQty thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQuantity || e.Column == colProductCode)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(e.RowHandle, colProductRTCQRCodeID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
            if (e.Column == colMaker)
            {
                int projectID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colMaker));
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
                    if (item == 0)
                        grvDetailTechExport.SetRowCellValue(i, colMaker, projectID);
                }
            }
        }
        void RecheckQty()
        {
            for (int k = 0; k < grvDetailTechExport.RowCount; k++)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(k, colProductRTCQRCodeID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
        }


        /// <summary>
        /// hàm dùng để chọn sản phẩm
        /// </summary>
        private void loadProduct()
        {
            if (billExport.Status == 1)
            {
                //dtProduct = TextUtils.Select($"SELECT ID AS ProductRTCID ,p.ProductCode,p.ProductName,p.UnitCountID,p.Maker,p.Number,p.NumberInStore,p.ProductCodeRTC, '' AS ProductQRCode, 0 AS ProductRTCQRCodeID FROM dbo.ProductGroupRTC WHERE WarehouseID = {warehouseID} ORDER BY NumberOrder");

                dtProduct = TextUtils.GetDataTableFromSP("spGetProductRTC",
                            new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" }
                            , new object[] { 0, "", 1, warehouseID });

                cboProduct.DisplayMember = "ProductCode";
                cboProduct.ValueMember = "ID";
                cboProduct.DataSource = dtProduct;
            }
            else if (warehouseID == 1)
            {
                dtProduct = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductRTCQRCode, "A", new string[] { "@WarehouseID" }, new object[] { warehouseID });

                cboProduct.DisplayMember = "ProductCode";
                cboProduct.ValueMember = "ProductRTCID";
                cboProduct.DataSource = dtProduct;
            }
            else
            {
                dtProduct = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
                                        new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                        new object[] { 0, "", 0, warehouseID });

                cboProduct.DisplayMember = "ProductCode";
                cboProduct.ValueMember = "ProductRTCID";
                cboProduct.DataSource = dtProduct;
            }




            //colProductCode.ColumnEdit = cboProduct;

            ////DataTable dt = new DataTable();
            ////dtProduct = TextUtils.Select("select * from ProductRTC");
            //dtProduct = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductRTCQRCode, "A", new string[] { }, new object[] { });
            //cboProduct.DisplayMember = "ProductCode";
            //cboProduct.ValueMember = "ProductRTCID";
            //cboProduct.DataSource = dtProduct;
            //colProductCode.ColumnEdit = cboProduct;
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            //if (Global.EmployeeID == 55) //Nếu là c Ngân đù
            //{
            //    int billtype = cboBillType.SelectedIndex;
            //    txtCode.Text = TextUtils.GetBillCode("BillExportTechnical", billtype);
            //}
            //else
            {
                loadBilllNumber();
            }
        }

        private void grvDetailTechExport_MouseDown(object sender, MouseEventArgs e)
        {
            if (billExport.Status == 1) return;
            GridHitInfo info = grvDetailTechExport.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvDetailTechExport);
            }
        }

        private void grvDetailTechExport_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQuantity || e.Column == colProductCode)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(e.RowHandle, colProductRTCQRCodeID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductRTCQRCodeID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
            //if (e.Column == colMaker)
            //{
            //    int projectID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colMaker));
            //    for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            //    {
            //        int item = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
            //        if (item == 0)
            //            grvDetailTechExport.SetRowCellValue(i, colMaker, projectID);
            //    }
            //}
        }

        private void cboBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadBilllNumber();
            //if (cboBillType.SelectedIndex == 0)
            //{
            //    cboSupplier.Enabled = true;
            //    cboCustomer.Enabled = false;
            //    cboCustomer.Text = "";
            //}
            //else
            //{
            //    cboCustomer.Enabled = true;
            //    cboSupplier.Enabled = false;
            //    cboSupplier.EditValue = "";
            //}
        }

        private void cboBillType_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu xuất mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                statusOld = "";
                //txtAddress.Clear();
                cboBillType.Text = "";
                txtNguoiNhan.Text = "";
                //txtLienHe.Text = "";
                txtNCC.Text = "";

                for (int i = grvDetailTechExport.RowCount - 1; i >= 0; i--)
                {
                    grvDetailTechExport.DeleteRow(i);
                }
                billExport = new BillExportTechnicalModel();
                loadBilllNumber();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailRTC frm = new frmProductDetailRTC(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }
        DataTable dtXuat = new DataTable();
        private void btnAddProductHistory_Click(object sender, EventArgs e)
        {
            List<DataRow> listRow = new List<DataRow>();
            frmProductHistory frm = new frmProductHistory();
            frm.warehouseID = 1;
            frm.IsbtnExport = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listRow = frm.listRow;
            }
            if (listRow.Count > 0)
            {
                int row = grvDetailTechExport.RowCount;
                for (int i = 0; i < listRow.Count; i++)
                {
                    MyLib.AddNewRow(grdData, grvDetailTechExport);
                    SetValueCol(listRow[i], i + row);
                }
            }
        }

        void SetValueCol(DataRow Row, int rowNumber)//int ProductRTCID, int rowNumber, int NumberBorrow, int HistoryProductRTCID
        {

            if (Row != null)
            {
                int ProductRTCQRCodeID = TextUtils.ToInt(Row["ProductRTCQRCodeID"]);
                int ProductID = TextUtils.ToInt(Row["ProductRTCID"]);
                int HistoryProductRTCID = TextUtils.ToInt(Row["ID"]);
                int NumberBorrow = TextUtils.ToInt(Row["NumberBorrow"]);
                string productName = TextUtils.ToString(Row["ProductName"]);
                string productCode = TextUtils.ToString(Row["ProductCode"]);
                string productCodeRTC = TextUtils.ToString(Row["ProductCodeRTC"]);
                string ProductQRCode = TextUtils.ToString(Row["ProductQRCode"]);
                string maker = TextUtils.ToString(Row["Maker"]);
                int idUnitCount = TextUtils.ToInt(Row["UnitCountID"]);
                string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));



                grvDetailTechExport.SetRowCellValue(rowNumber, colProductName, productName);
                //grvDetailTechExport.SetRowCellValue(rowNumber, colProductCode, productCode);
                grvDetailTechExport.SetRowCellValue(rowNumber, colProductCode, ProductRTCQRCodeID);
                grvDetailTechExport.SetRowCellValue(rowNumber, colUnitName, unitName);
                grvDetailTechExport.SetRowCellValue(rowNumber, colInternalCode, productCodeRTC);
                grvDetailTechExport.SetRowCellValue(rowNumber, colMaker, maker);
                grvDetailTechExport.SetRowCellValue(rowNumber, colUnitID, idUnitCount);
                grvDetailTechExport.SetRowCellValue(rowNumber, colQuantity, NumberBorrow);
                grvDetailTechExport.SetRowCellValue(rowNumber, colHistoryProductRTCID, HistoryProductRTCID);
                grvDetailTechExport.SetRowCellValue(rowNumber, colProductQRCode, ProductQRCode);
                grvDetailTechExport.SetRowCellValue(rowNumber, colProductRTCQRCodeID, ProductRTCQRCodeID);
                grvDetailTechExport.SetRowCellValue(rowNumber, colProductID, ProductID);
                grvDetailTechExport.SetRowCellValue(rowNumber, colQuantity, 1);
                grvDetailTechExport.SetRowCellValue(rowNumber, colProductCodeView, productCode);
                // grvDetailTechExport.FocusedColumn = colQuantity;

                grvDetailTechExport.FocusedRowHandle = -1;
            }
        }

        private void btnDelete1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvDetailTechExport.GetFocusedRowCellDisplayText(colProductCode));
            string ProductQRCode = TextUtils.ToString(grvDetailTechExport.GetFocusedRowCellDisplayText(colProductQRCode)).Trim();
            int rowIndex = grvDetailTechExport.GetSelectedRows().Length > 0 ? grvDetailTechExport.GetSelectedRows()[0] : 0;
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvDetailTechExport.DeleteSelectedRows();


                lstIDDelete.Add(ID);
                Listqrcode.Remove(ProductQRCode);
                if (lstSerialNumbers.Count > rowIndex)
                {
                    lstSerialNumbers.RemoveAt(rowIndex);
                }

            }
        }

        private void dtpCreatedDate_ValueChanged(object sender, EventArgs e)
        {
            //loadBilllNumber();
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            //txtCode.Focus();
            //grvDetailTechExport.Focus();

            //SearchLookUpEdit editor = (SearchLookUpEdit)sender;
            //DataRowView dvRow = editor.GetSelectedDataRow() as DataRowView;
            //DataRow row = dvRow.Row;

            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            int rowHandle = edit.Properties.GetIndexByKeyValue(edit.EditValue);
            object row = edit.Properties.View.GetRow(rowHandle);

            //var dataRowView = (DataRowView)edit.Properties.GetRowByKeyValue(edit.EditValue);


            int ProductRTCQRCodeID = TextUtils.ToInt((row as DataRowView).Row["ProductRTCQRCodeID"]);
            int ProductID = TextUtils.ToInt((row as DataRowView).Row["ProductRTCID"]);
            int NumberInStore = TextUtils.ToInt((row as DataRowView).Row["NumberInStore"]);
            int Number = TextUtils.ToInt((row as DataRowView).Row["Number"]);
            string productName = TextUtils.ToString((row as DataRowView).Row["ProductName"]);
            string productCodeRTC = TextUtils.ToString((row as DataRowView).Row["ProductCodeRTC"]);
            string ProductQRCode = TextUtils.ToString((row as DataRowView).Row["ProductQRCode"]);
            string maker = TextUtils.ToString((row as DataRowView).Row["Maker"]);
            int idUnitCount = TextUtils.ToInt((row as DataRowView).Row["UnitCountID"]);
            string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));

            grvDetailTechExport.SetFocusedRowCellValue(colProductName, productName);
            grvDetailTechExport.SetFocusedRowCellValue(colUnitName, unitName);
            grvDetailTechExport.SetFocusedRowCellValue(colInternalCode, productCodeRTC);
            grvDetailTechExport.SetFocusedRowCellValue(colMaker, maker);
            grvDetailTechExport.SetFocusedRowCellValue(colUnitID, idUnitCount);
            grvDetailTechExport.SetFocusedRowCellValue(colNumberInStore, NumberInStore);
            grvDetailTechExport.SetFocusedRowCellValue(colNumber, Number);
            grvDetailTechExport.SetFocusedRowCellValue(colProductQRCode, ProductQRCode);
            grvDetailTechExport.SetFocusedRowCellValue(colQuantity, 1);
            grvDetailTechExport.SetFocusedRowCellValue(colProductID, ProductID);
            grvDetailTechExport.SetFocusedRowCellValue(colProductRTCQRCodeID, ProductRTCQRCodeID);
            grvDetailTechExport.FocusedColumn = colQuantity;

            int index = grvDetailTechExport.FocusedRowHandle;
            for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            {
                if (i != index)
                {
                    if (TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductRTCQRCodeID)) == ProductRTCQRCodeID)
                    {
                        return;
                    }
                }
            }

        }





        private void grvDetailTechExport_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //GridView view = sender as GridView;
            //int Number = TextUtils.ToInt(view.GetFocusedRowCellValue(colNumber));
            //int NumberInStore = TextUtils.ToInt(view.GetFocusedRowCellValue(colNumberInStore));
            //if (view.FocusedColumn.FieldName == "Quantity")
            //{
            //    if (TextUtils.ToInt(e.Value) > NumberInStore)
            //    {
            //        e.Valid = false;
            //        e.ErrorText = $"Số lượng tồn kho chỉ còn {NumberInStore} !";
            //        Show();
            //    }
            //    //}
            //}


            //if (grvDetailTechExport.FocusedColumn == colQuantity)
            //{
            //    //int quantity = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colQuantity));
            //    int quantity = TextUtils.ToInt(e.Value);

            //    int productID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colProductID));

            //    var productSelected = (DataRowView)cboProduct.GetRowByKeyValue(productID);
            //    int inventoryReal = 0;
            //    if (productSelected != null) inventoryReal = TextUtils.ToInt(productSelected["InventoryReal"]);

            //    if (quantity > inventoryReal)
            //    {
            //        e.Valid = false;
            //        e.ErrorText = $"Số lượng tồn kho chỉ còn {inventoryReal}!";
            //    }
            //}
        }

        List<string> Listqrcode = new List<string>();
        private void txtQRCode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductRTCByQrCode", "A", new string[] { "@ProductRTCQRCode", "@WarehouseID" }, new object[] { txtQrCode.Text.Trim(), warehouseID });
            if (dt.Rows.Count == 0 || dt == null)
            {
                //txtQrCode.SelectAll();
                return;
            }

            grvDetailTechExport.BeginDataUpdate();
            DataRow dr = dtAll.NewRow();
            dr.BeginEdit();
            dr["STT"] = grvDetailTechExport.RowCount + 1;
            dr["ProductRTCQRCodeID"] = dt.Rows[0]["ID"];//ID của ProductRTCQRCode
            dr["ProductID"] = dt.Rows[0]["ProductRTCID"];
            dr["ProductQRCode"] = dt.Rows[0]["ProductQRCode"];
            dr["ProductCode"] = dt.Rows[0]["ProductCode"];
            dr["ProductName"] = dt.Rows[0]["ProductName"];
            dr["ProductCodeRTC"] = dt.Rows[0]["ProductCodeRTC"];
            dr["Maker"] = dt.Rows[0]["Maker"];
            //dr["AddressBox"] = dt.Rows[0]["AddressBox"];
            //dr["Note"] = dt.Rows[0]["Note"];
            dr["Quantity"] = 1;
            int idUnitCount = TextUtils.ToInt(dt.Rows[0]["UnitCountID"]);
            string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));
            dr["UnitName"] = unitName;
            dr.EndEdit();
            if (Listqrcode.Contains(TextUtils.ToString(dt.Rows[0]["ProductQRCode"])))
            {
                grdData.DataSource = dtAll;
                grvDetailTechExport.EndDataUpdate();

            }
            else
            {
                dtAll.Rows.Add(dr);
                grdData.DataSource = dtAll;
                grvDetailTechExport.EndDataUpdate();
            }
            Listqrcode.Add(TextUtils.ToString(dt.Rows[0]["ProductQRCode"]));

            txtQrCode.SelectAll();
        }
        List<List<BillExportTechDetailSerialModel>> lstSerialNumbers = new List<List<BillExportTechDetailSerialModel>>();

        private void grvDetailTechExport_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column == colAddSerialNumber)
            //{
            //    if (grdData.DataSource == null) return;
            //    int ID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colID));
            //    //if (ID == 0) return;
            //    int rowIndex = e.RowHandle;
            //    int Quantity = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colQuantity));
            //    while (lstSerialNumbers.Count <= rowIndex)
            //    {
            //        lstSerialNumbers.Add(new List<BillExportTechDetailSerialModel>());
            //    }
            //    BillExportDetailTechnicalModel model = new BillExportDetailTechnicalModel() { ID = ID, Quantity = Quantity };
            //    frmBillTechnicalSerialNumber frm = new frmBillTechnicalSerialNumber(warehouseID);
            //    frm.modelExportDetail = model;
            //    frm.Type = 2;
            //    frm.lstSerialNumberExport = lstSerialNumbers[rowIndex];
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        List<BillExportTechDetailSerialModel> lstSerialNumber = frm.lstSerialNumberExport;
            //        if (lstSerialNumbers.Count <= rowIndex)
            //        {
            //            lstSerialNumbers.Add(lstSerialNumber);
            //        }
            //        else
            //        {
            //            lstSerialNumbers[rowIndex] = lstSerialNumber;
            //        }
            //    }
            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemSearchLookUpEdit2View_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //    if (e.RowHandle < 0)
            //        return;

            //    SearchLookUpEdit editor = (SearchLookUpEdit)sender;
            //    DataRowView dvRow = editor.GetSelectedDataRow() as DataRowView;
            //    DataRow row = dvRow.Row;

            //    //DataRow row = repositoryItemSearchLookUpEdit2View.GetDataRow(e.RowHandle);

            //    int ProductRTCQRCodeID = TextUtils.ToInt(row["ProductRTCQRCodeID"]);
            //    int ProductID = TextUtils.ToInt(row["ProductRTCID"]);
            //    int NumberInStore = TextUtils.ToInt(row["NumberInStore"]);
            //    int Number = TextUtils.ToInt(row["Number"]);
            //    string productName = TextUtils.ToString(row["ProductName"]);
            //    string productCodeRTC = TextUtils.ToString(row["ProductCodeRTC"]);
            //    string ProductQRCode = TextUtils.ToString(row["ProductQRCode"]);
            //    string maker = TextUtils.ToString(row["Maker"]);
            //    int idUnitCount = TextUtils.ToInt(row["UnitCountID"]);
            //    string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));

            //    grvDetailTechExport.SetFocusedRowCellValue(colProductName, productName);
            //    grvDetailTechExport.SetFocusedRowCellValue(colUnitName, unitName);
            //    grvDetailTechExport.SetFocusedRowCellValue(colInternalCode, productCodeRTC);
            //    grvDetailTechExport.SetFocusedRowCellValue(colMaker, maker);
            //    grvDetailTechExport.SetFocusedRowCellValue(colUnitID, idUnitCount);
            //    grvDetailTechExport.SetFocusedRowCellValue(colNumberInStore, NumberInStore);
            //    grvDetailTechExport.SetFocusedRowCellValue(colNumber, Number);
            //    grvDetailTechExport.SetFocusedRowCellValue(colProductQRCode, ProductQRCode);
            //    grvDetailTechExport.SetFocusedRowCellValue(colQuantity, 1);
            //    grvDetailTechExport.SetFocusedRowCellValue(colProductID, ProductID);
            //    grvDetailTechExport.SetFocusedRowCellValue(colProductRTCQRCodeID, ProductRTCQRCodeID);
            //    grvDetailTechExport.FocusedColumn = colQuantity;

        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;

            // Lấy thông tin từ dòng đang focus
            int Quantity = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colQuantity));
            if (Quantity == 0)
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            int rowHandle = grvDetailTechExport.FocusedRowHandle;


            // Tạo model với ID = 0 (vì chưa lưu vào database)

            BillExportDetailTechnicalModel model = new BillExportDetailTechnicalModel()
            {
                ID = 0,
                Quantity = Quantity,

            };
            // Lưu oldtempSerial ban đầu (chỉ lần đầu)
            if (oldTempSerial.Count == 0)
            {
                oldTempSerial = grvDetailTechExport.GetRowCellDisplayText(rowHandle, colJoinedIDs)
                              .Split(',')
                              .Where(x => !string.IsNullOrEmpty(x))
                              .ToList();
            }
            List<string> lst = grvDetailTechExport.GetRowCellDisplayText(rowHandle, colJoinedIDs)
                              .Split(',')
                              .Where(x => !string.IsNullOrEmpty(x))
                              .ToList();
            lstSerial.Clear();
            foreach (string i in lst)
            {
                int id = TextUtils.ToInt(i);
                BillExportTechDetailSerialModel serialmodel = SQLHelper<BillExportTechDetailSerialModel>.FindByID(id);
                if (serialmodel != null)
                {
                    lstSerial.Add(serialmodel);
                }
            }
            using (frmBillTechnicalSerialNumber frm = new frmBillTechnicalSerialNumber(warehouseID))
            {
                int id = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colID));
                if (id <= 0)
                {
                    frm.modelExportDetail = model;
                }
                else
                {
                    frm.modelExportDetail = SQLHelper<BillExportDetailTechnicalModel>.FindByID(id);
                    frm.quantity = Quantity;
                }


                frm.Type = 2;
                frm.lstSerialNumberExport = lstSerial;
                frm.lstSerialNumberid = lst;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tempSerial = frm.lstSerialNumberid;
                    string joinedID = string.Join(",", tempSerial);
                    grvDetailTechExport.SetRowCellValue(rowHandle, colJoinedIDs, joinedID);
                }
            }
        }

        private void frmBillExportTechDetail_New_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!isSave)
            //{
            //    DialogResult rs = MessageBox.Show("Bạn có muốn lưu thay đổi trước khi đóng không?", "Thông báo", MessageBoxButtons.OKCancel);
            //    if (rs == DialogResult.OK)
            //    {
            //        btnSave_Click(sender, e);
            //    }
            //    else
            //    {
            //        // Chỉ xóa các serial mới thêm (không có trong oldtempSerial ban đầu)
            //        List<BillExportTechDetailSerialModel> lst = SQLHelper<BillExportTechDetailSerialModel>.FindAll().Where(p => p.BillExportTechDetailID == 0).ToList();
            //        if (lst.Count > 0 && lst != null)
            //        {
            //            SQLHelper<BillExportTechDetailSerialModel>.DeleteListModel(lst);
            //        }
            //        var newSerials = tempSerial.Except(oldTempSerial).ToList();
            //        foreach (string item in newSerials)
            //        {
            //            int id = TextUtils.ToInt(item);
            //            SQLHelper<BillExportTechDetailSerialModel>.DeleteModelByID(id);
            //        }
            //        this.DialogResult = DialogResult.OK;
            //    }

            //}
        }

        private void cboProjectID_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            ProjectModel project = (ProjectModel)edit.GetSelectedDataRow();
            if (project == null) return;
            int rowhandle = grvDetailTechExport.FocusedRowHandle;
            grvDetailTechExport.SetRowCellValue(rowhandle, colProjectName, project.ProjectName);
            grvDetailTechExport.SetRowCellValue(rowhandle, colProjectID, project.ID);
        }

        private void CkbAddHistoryProductRTC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            frm.Tag = "demo";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadNCC();
            }
        }
    }
}
