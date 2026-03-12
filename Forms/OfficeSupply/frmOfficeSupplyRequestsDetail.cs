using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmOfficeSupplyRequestsDetail : _Forms
    {
        #region VARIABLES
        public int departmentID = 0;
        private bool NotLoadFirstTime = false;
        DataTable dtOSRD = new DataTable();
        private int FocusedRowOld = -1;
        bool isFocusedOld = true;

        public OfficeSupplyRequestsModel OSRequestModel = new OfficeSupplyRequestsModel();
        public int OSRequestID = 0;
        DataTable dtEmployeeAll = new DataTable();
        DataTable dtDetailFirstTime = new DataTable();
        List<int> lsDeleteDetail = new List<int>();
        public bool isCopy = false;
        List<OfficeSupplyUnitModel> lsOSUnitModel = new List<OfficeSupplyUnitModel>();
        private int previousDepartmentID = -1;
        #endregion
        public frmOfficeSupplyRequestsDetail()
        {
            InitializeComponent();
        }
        #region LOAD DATA
        private void frmOfficeSupplyRequestsDetail_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime startOfPreviousMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            if (!Global.IsAdmin)
            {
                dtpDateRequest.MinDate = startOfPreviousMonth;
            }

            LoadEmployeeAll();
            LoadOfficeSupplyUnit();
            LoadEmployeeByDepartment();
            LoadDepartment();
            LoadOfficeSupply();
            loadDataDetail(dtDetailFirstTime);
            LoadEmployee();

            LoadData();

        }
        void LoadData()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupplyRequestsDetail",
                new string[] { "@OfficeSupplyRequestsID" },
                new object[] { OSRequestID });
            dt.Columns.Add("STT", typeof(int));
            //copy
            if (isCopy)
            {
                if (dt.Columns.Contains("ID"))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["ID"] = 0;
                    }
                }
                OSRequestID = 0;
                NotLoadFirstTime = false;
                cboDepartment.EditValue = TextUtils.ToInt(dt.Rows[0]["DepartmentID"]);
            }
            //master
            DataTable dtEmpolyee = dt.Clone();
            var addEmployeeIDs = new HashSet<int>();
            foreach (DataRow row in dt.Rows)
            {
                int employeeID = row.Field<int>("EmployeeID");
                if (addEmployeeIDs.Add(employeeID))
                {
                    dtEmpolyee.ImportRow(row);
                }
            }
            grdData.DataSource = dtEmpolyee;
            dtOSRD = dt.Copy();

            //detail
            dtDetailFirstTime = dt;
            grdDataDetail.DataSource = dt.Clone();

            //cboDepartment.EditValue = Global.DepartmentID;

            //if (!Global.userAllsOfficeSupply.Contains(Global.EmployeeID) && !Global.IsAdmin)
            //{
            //    cboDepartment.EditValue = Global.DepartmentID;
            //    //cboDepartment.Enabled = false;
            //}

            //khac
            if (OSRequestModel.ID > 0)
            {
                if (OSRequestModel.IsApproved == true)
                {
                    setAllowEditNotQuantityReceived(grvData);
                    setAllowEditNotQuantityReceived(grvDataDetail);

                    dtpDateRequest.Enabled = cboDepartment.Enabled = cboEmployeeRegister.Enabled = false;
                }
                NotLoadFirstTime = false;
                //cboDepartment.EditValue = TextUtils.ToInt(dt.Rows[0]["DepartmentID"]);
                cboDepartment.EditValue = OSRequestModel.DepartmentID;
                dtpDateRequest.Value = TextUtils.ToDate5(OSRequestModel.DateRequest);
                cboEmployeeRegister.EditValue = OSRequestModel.EmployeeIDRequest;
            }


        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployeeRegister.Properties.ValueMember = "ID";
            cboEmployeeRegister.Properties.DisplayMember = "FullName";
            cboEmployeeRegister.Properties.DataSource = dt;

            cboEmployeeRegister.EditValue = Global.EmployeeID;
        }

        void setAllowEditNotQuantityReceived(GridView grvData)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in grvData.Columns)
            {
                if (column.FieldName != "QuantityReceived")
                {
                    column.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    //check là admin ở đây
                    column.OptionsColumn.AllowEdit = true;
                }
            }
        }

        void loadDataDetail(DataTable dt)
        {
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (employeeID == 0 || dt.Rows.Count == 0)
            {
                grdDataDetail.DataSource = dt.Clone();
                return;
            }
            DataRow[] dtRow = dt.Select($"EmployeeID = {employeeID}");
            DataTable newTable = dt.Clone();
            foreach (DataRow row in dtRow)
            {
                newTable.Rows.Add(row.ItemArray);
            }
            grdDataDetail.DataSource = newTable;
        }

        void LoadEmployeeAll()
        {
            dtEmployeeAll = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
        }

        void LoadEmployeeByDepartment()
        {
            DataTable dt = dtEmployeeAll.Copy();
            // get data employee theo phong ban
            if (departmentID > 0)
            {
                //if (dt.Columns.Contains("ID"))
                //{
                //    dt.Columns["ID"].ColumnName = "EmployeeID";
                //}

                List<DataRow> filteredRowsList = new List<DataRow>();
                DataRow[] filteredRows = dt.Select($"DepartmentID = {departmentID}");
                if (filteredRows.Length > 0)
                {
                    filteredRowsList.AddRange(filteredRows);
                }

                if (filteredRowsList.Count > 0)
                {
                    dt = filteredRowsList.CopyToDataTable();
                }
                else
                {
                    dt = dt.Clone();
                }

                //cboEmployee.ValueMember = "EmployeeID";
                cboEmployee.ValueMember = "ID";
                cboEmployee.DisplayMember = "Code";
                cboEmployee.DataSource = dt;
            }
        }

        //List<int> userAlls = new List<int>() { 354, 156, 331 };
        void LoadDepartment()
        {
            Global.departmentIDs.Add(Global.DepartmentID);
            //int[] departmentIDs = new int[] { 9, 10 };
            List<DepartmentModel> departmentList = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            if (Global.EmployeeID == 331 || Global.EmployeeID == 55) //nếu là NGân và Hà admin
            {
                departmentList = departmentList.Where(x => Global.departmentIDs.Contains(x.ID)).ToList();
            }

            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = departmentList;
            //cboDepartment.EditValue = Global.DepartmentID;

            if (Global.EmployeeID != 331 && Global.EmployeeID != 55)
            {
                if (!Global.userAllsOfficeSupply.Contains(Global.EmployeeID) && !Global.IsAdmin)
                {
                    cboDepartment.EditValue = Global.DepartmentID;
                    cboDepartment.Enabled = false;
                }
            }
        }
        void LoadOfficeSupply()
        {
            List<OfficeSupplyModel> supply = SQLHelper<OfficeSupplyModel>.FindAll();
            cboOfficeSupply.ValueMember = "ID";
            cboOfficeSupply.DisplayMember = "NameNCC";
            cboOfficeSupply.DataSource = supply;
        }
        void LoadOfficeSupplyUnit()
        {
            lsOSUnitModel = SQLHelper<OfficeSupplyUnitModel>.FindAll();
        }
        #endregion

        #region ADD, DELETE, SELECT DANH SÁCH NGƯỜI ĐĂNG KÍ
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
                grvData.FocusedRowHandle = grvData.RowCount - 1;
            }
            if (info.Column == colDelete && e.Y < 40)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa toàn bộ người đăng ký?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    refreshGrdData(grdData);

                    foreach (DataRow item in dtOSRD.Rows)
                    {
                        int id = TextUtils.ToInt(item["ID"]);
                        if (id > 0)
                        {
                            lsDeleteDetail.Add(id);
                        }
                    }
                    dtOSRD.Clear();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvData.GetFocusedRowCellDisplayText(colSTT));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa người đăng ký hàng số [{0}]?", stt), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow[] existingRows = dtOSRD.Select($"EmployeeID = {employeeID}");
                if (existingRows.Length > 0)
                {
                    foreach (DataRow row in existingRows)
                    {
                        int id = TextUtils.ToInt(row["ID"]);
                        row.Delete();
                        if (id > 0)
                        {
                            lsDeleteDetail.Add(id);
                        }
                    }
                    dtOSRD.AcceptChanges();
                }
                grvData.DeleteSelectedRows();
            }
        }
        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            int EmployeeID = 0;
            string name = "";
            string departmentName = "";
            if (dataRow != null)
            {
                if (IsDuplicateRecord(TextUtils.ToInt(lookUpEdit.EditValue)))
                {
                    MessageBox.Show("Người đi này đã tồn tại trong danh sách. Vui lòng chọn người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    EmployeeID = TextUtils.ToInt(lookUpEdit.EditValue);
                    name = dataRow.Row.Field<string>("FullName");
                    departmentName = dataRow.Row.Field<string>("DepartmentName");
                }
            }
            grvData.SetFocusedRowCellValue(colEmployeeID, EmployeeID);
            grvData.SetFocusedRowCellValue(colName, name);
            grvData.SetFocusedRowCellValue(colDepartmentName, departmentName);

            for (int i = 0; i < grvDataDetail.RowCount; i++)
            {
                int employeeIDDetai = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colEmployeeIDDetail));
                int officeSupplyID = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colOfficeSupplyID));

                DataRow existingRow = dtOSRD.AsEnumerable().FirstOrDefault(row =>
                                                row.Field<int>("EmployeeID") == employeeIDDetai &&
                                                row.Field<int>("OfficeSupplyID") == officeSupplyID);
                if (existingRow != null)
                {
                    existingRow["EmployeeID"] = EmployeeID;
                    existingRow["FullName"] = name;
                }

                grvDataDetail.SetRowCellValue(i, colEmployeeIDDetail, EmployeeID);
                grvDataDetail.SetRowCellValue(i, colEmployeeName, name);
            }
        }
        #endregion

        #region ADD, DELETE, SELECT DANH SÁCH VĂN PHÒNG PHẨM
        private void grvDataDetail_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvDataDetail.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTTDetail && e.Y < 40)
            {
                //MyLib.AddNewRow(grdDataDetail, grvDataDetail);
                int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                if (employeeID <= 0) return;
                int STT;
                DataTable dt = (DataTable)grdDataDetail.DataSource;
                if (dt.Rows.Count == 0)
                {
                    STT = 1;
                }
                else
                {
                    STT = TextUtils.ToInt(grvDataDetail.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
                }
                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = STT;
                dtrow["Quantity"] = 1;
                dtrow["QuantityReceived"] = 0;
                dtrow["EmployeeID"] = employeeID;
                dtrow["FullName"] = TextUtils.ToString(grvData.GetFocusedRowCellValue(colName));
                dt.Rows.Add(dtrow);
                grdDataDetail.DataSource = dt;
            }
            if (info.Column == colDeleteDetail && e.Y < 40)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa toàn bộ văn phòng phẩm?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = grvDataDetail.RowCount - 1; i >= 0; i--)
                    {
                        int employeeIDDetai = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colEmployeeIDDetail));
                        int officeSupplyID = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colOfficeSupplyID));
                        int id = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colDetaiID));

                        DataRow existingRow = dtOSRD.AsEnumerable().FirstOrDefault(row =>
                                                row.Field<int>("EmployeeID") == employeeIDDetai &&
                                                row.Field<int>("OfficeSupplyID") == officeSupplyID);
                        if (existingRow != null)
                        {
                            existingRow.Delete();
                            if (id > 0)
                            {
                                lsDeleteDetail.Add(id);
                            }
                        }
                        grvDataDetail.DeleteRow(i);
                    }
                    dtOSRD.AcceptChanges();
                }
            }


        }
        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvDataDetail.GetFocusedRowCellDisplayText(colSTTDetail));
            int employeeIDDetai = TextUtils.ToInt(grvDataDetail.GetFocusedRowCellValue(colEmployeeIDDetail));
            int officeSupplyID = TextUtils.ToInt(grvDataDetail.GetFocusedRowCellValue(colOfficeSupplyID));
            int id = TextUtils.ToInt(grvDataDetail.GetFocusedRowCellValue(colDetaiID));

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa văn phòng phẩm hàng số [{0}]?", stt), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow existingRow = dtOSRD.AsEnumerable().FirstOrDefault(row =>
                                        row.Field<int>("EmployeeID") == employeeIDDetai &&
                                        row.Field<int>("OfficeSupplyID") == officeSupplyID);
                if (existingRow != null)
                {
                    existingRow.Delete();
                    dtOSRD.AcceptChanges();
                    if (id > 0)
                    {
                        lsDeleteDetail.Add(id);
                    }
                }
                grvDataDetail.DeleteSelectedRows();
            }
        }
        private void cboOfficeSupply_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            OfficeSupplyModel supplyModel = (OfficeSupplyModel)lookUpEdit.GetSelectedDataRow();

            int selectedOfficeSupplyID = TextUtils.ToInt(lookUpEdit.EditValue);

            for (int i = 0; i < grvDataDetail.RowCount; i++)
            {
                if (TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colOfficeSupplyID)) == selectedOfficeSupplyID)
                {
                    MessageBox.Show("VPP này đã tồn tại trong danh sách. Vui lòng chọn VPP khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grvDataDetail.SetFocusedRowCellValue(colOfficeSupplyID, 0);
                    return;
                }
            }

            if (supplyModel == null)return;
            grvDataDetail.SetFocusedRowCellValue(colOfficeSupplyID, supplyModel.ID);
            int supplyUnitID = TextUtils.ToInt(supplyModel.SupplyUnitID);
            //OfficeSupplyUnitModel model = SQLHelper<OfficeSupplyUnitModel>.FindByID(supplyUnitID);

            OfficeSupplyUnitModel model = lsOSUnitModel.Find(x => x.ID == supplyUnitID);
            if (model != null)
            {
                grvDataDetail.SetFocusedRowCellValue(colUnit, TextUtils.ToString(model.Name));
            }
        }
        private void ckExceedsLimit_CheckedChanged(object sender, EventArgs e)
        {
            grvDataDetail.SetFocusedRowCellValue(colReason, "");
        }
        #endregion

        #region SAVE DATA

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                dtpDateRequest.Value = DateTime.Now;
                dtOSRD.Clear();
                refreshGrdData(grdData);
                refreshGrdData(grdDataDetail);
            }
        }

        private void refreshGrdData(GridControl grdData)
        {
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt == null) return;
            dt.Clear();
            grdData.RefreshDataSource();
        }

        private bool save()
        {
            grvDataDetail.FocusedRowHandle = -1;
            if (!saveTempData()) return false;
            if (!CheckValidate()) return false;
            // master
            OfficeSupplyRequestsModel oSRModel = new OfficeSupplyRequestsModel();
            if (OSRequestID > 0)
            {
                oSRModel = SQLHelper<OfficeSupplyRequestsModel>.FindByID(OSRequestID);
            }

            oSRModel.DateRequest = dtpDateRequest.Value;
            oSRModel.EmployeeIDRequest = TextUtils.ToInt(cboEmployeeRegister.EditValue);
            oSRModel.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
            if (oSRModel.ID > 0)
            {
                SQLHelper<OfficeSupplyRequestsModel>.Update(oSRModel);
            }
            else
            {

                oSRModel.ID = SQLHelper<OfficeSupplyRequestsModel>.Insert(oSRModel).ID;
            }

            // detail
            foreach (DataRow row in dtOSRD.Rows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                int employeeDetailID = TextUtils.ToInt(row["EmployeeID"]);
                int officeSupplyID = TextUtils.ToInt(row["OfficeSupplyID"]);
                bool exceedsLimit = TextUtils.ToBoolean(row["ExceedsLimit"]);
                string reason = TextUtils.ToString(row["Reason"]);
                OfficeSupplyRequestsDetailModel model = new OfficeSupplyRequestsDetailModel();
                if (id > 0)
                {
                    model = SQLHelper<OfficeSupplyRequestsDetailModel>.FindByID(id);
                }

                if (employeeDetailID <= 0 || officeSupplyID <= 0)
                {
                    continue;
                }
                if (exceedsLimit && reason == "")
                {
                    continue;
                }

                model.OfficeSupplyRequestsID = oSRModel.ID;
                model.EmployeeID = employeeDetailID;
                model.OfficeSupplyID = officeSupplyID;
                model.Quantity = TextUtils.ToInt(row["Quantity"]);
                model.QuantityReceived = TextUtils.ToInt(row["QuantityReceived"]);
                model.ExceedsLimit = exceedsLimit;
                model.Reason = reason;
                model.Note = TextUtils.ToString(row["Note"]);

                if (model.ID > 0)
                {
                    SQLHelper<OfficeSupplyRequestsDetailModel>.Update(model);
                }
                else
                {
                    SQLHelper<OfficeSupplyRequestsDetailModel>.Insert(model);
                }
            }

            // delete
            for (int i = 0; i < lsDeleteDetail.Count; i++)
            {
                int id = TextUtils.ToInt(lsDeleteDetail[i]);
                if (id == 0) continue;
                SQLHelper<OfficeSupplyRequestsDetailModel>.DeleteModelByID(id);
            }

            return true;
        }

        private bool saveTempData()
        {
            for (int i = 0; i < grvDataDetail.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colSTTDetail));
                int employeeIDDetai = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colEmployeeIDDetail));
                int officeSupplyID = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colOfficeSupplyID));
                int qty = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colQuantity));
                int quantityReceived = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colQuantityReceived));
                bool exceedsLimit = TextUtils.ToBoolean(grvDataDetail.GetRowCellValue(i, colExceedsLimit));
                string reason = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colReason));
                string note = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colNote));
                string name = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colEmployeeName));

                if (officeSupplyID <= 0)
                {
                    MessageBox.Show($"Văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (quantityReceived < 0)
                {
                    MessageBox.Show($"Số lượng văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] không được nhỏ hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (qty <= 0)
                {
                    MessageBox.Show($"Số lượng văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (exceedsLimit && reason == "")
                {
                    MessageBox.Show($"Lý do vượt mức dòng số [{stt}] của nhân viên [{name}] không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                DataRow existingRow = dtOSRD.AsEnumerable().FirstOrDefault(row =>
                    row.Field<int>("EmployeeID") == employeeIDDetai &&
                    row.Field<int>("OfficeSupplyID") == officeSupplyID);
                if (existingRow != null)
                {
                    existingRow["STT"] = stt;
                    existingRow["Quantity"] = qty;
                    existingRow["QuantityReceived"] = quantityReceived;
                    existingRow["ExceedsLimit"] = exceedsLimit;
                    existingRow["Reason"] = reason;
                    existingRow["Note"] = note;
                }
                else
                {
                    DataRow newRow = dtOSRD.NewRow();
                    newRow["STT"] = stt;
                    newRow["EmployeeID"] = employeeIDDetai;
                    newRow["OfficeSupplyID"] = officeSupplyID;
                    newRow["Quantity"] = qty;
                    newRow["QuantityReceived"] = quantityReceived;
                    newRow["ExceedsLimit"] = exceedsLimit;
                    newRow["Reason"] = reason;
                    newRow["Note"] = note;

                    dtOSRD.Rows.Add(newRow);
                }
            }
            return true;
        }

        private bool CheckValidate()
        {
            if (dtOSRD.Rows.Count <= 0)
            {
                MessageBox.Show($"Cần ít nhất một người đăng ký văn phòng phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            bool isNull = true;
            foreach (DataRow item in dtOSRD.Rows)
            {
                int osid = TextUtils.ToInt(item["OfficeSupplyID"]);
                if (osid > 0)
                {
                    isNull = false;
                }
            }
            if (isNull)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region PHƯƠNG THỨC KHÁC
        private bool IsDuplicateRecord(int EmployeeID)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID)) == EmployeeID)
                {
                    return true;
                }
            }
            return false;
        }

        void addRowByDepartmentID(int DepartmentID)
        {
            DepartmentModel departmentModel = SQLHelper<DepartmentModel>.FindByID(DepartmentID);

            var ex1 = new Expression("DepartmentID", DepartmentID);
            var ex2 = new Expression("Status", 0);
            List<EmployeeModel> lst = SQLHelper<EmployeeModel>.FindByExpression(ex1.And(ex2));
            foreach (var item in lst)
            {
                int STT;
                DataTable dt = (DataTable)grdData.DataSource;
                if (dt == null) return;
                if (dt == null || dt.Rows.Count == 0)
                {
                    STT = 1;
                }
                else
                {
                    STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
                }
                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = STT;
                dtrow["EmployeeID"] = item.ID;
                dtrow["FullName"] = item.FullName;
                dtrow["DepartmentName"] = TextUtils.ToString(departmentModel.Name);
                dt.Rows.Add(dtrow);
                grdData.DataSource = dt;
            }
        }

        private void cboDepartment_EditValueChanged_1(object sender, EventArgs e)
        {
            int newDepartmentID = TextUtils.ToInt(cboDepartment.EditValue);

            if (newDepartmentID > 0)
            {
                if (NotLoadFirstTime)
                {
                    var result = MessageBox.Show(string.Format("Nội dung đăng kí VPP đã thay đổi. Bạn có muốn lưu không?"), TextUtils.Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        save();

                        refreshGrdData(grdData);

                        addRowByDepartmentID(newDepartmentID);
                    }
                    else if (result == DialogResult.No)
                    {
                        refreshGrdData(grdData);

                        addRowByDepartmentID(newDepartmentID);
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        NotLoadFirstTime = false;
                        cboDepartment.EditValue = previousDepartmentID;
                        return;
                    }
                }
                else
                {
                    refreshGrdData(grdData);
                    addRowByDepartmentID(newDepartmentID);
                }

                previousDepartmentID = newDepartmentID;
                departmentID = newDepartmentID;
                NotLoadFirstTime = true;
                LoadEmployeeByDepartment();
            }
            else
            {
                cboEmployee.DataSource = dtEmployeeAll.Clone();
            }
        }

        private void grvDataDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "Reason")
            {
                bool exceedsLimit = TextUtils.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "ExceedsLimit"));
                if (!exceedsLimit)
                {
                    e.Cancel = true;
                }
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            for (int i = 0; i < grvDataDetail.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colSTTDetail));
                int employeeIDDetai = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colEmployeeIDDetail));
                int officeSupplyID = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colOfficeSupplyID));
                int qty = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colQuantity));
                int quantityReceived = TextUtils.ToInt(grvDataDetail.GetRowCellValue(i, colQuantityReceived));
                bool exceedsLimit = TextUtils.ToBoolean(grvDataDetail.GetRowCellValue(i, colExceedsLimit));
                string reason = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colReason));
                string note = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colNote));
                string name = TextUtils.ToString(grvDataDetail.GetRowCellValue(i, colEmployeeName));

                if (officeSupplyID <= 0)
                {
                    if (isFocusedOld)
                    {
                        MessageBox.Show($"Văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        isFocusedOld = false;
                    }
                    grvData.FocusedRowHandle = FocusedRowOld;
                    isFocusedOld = true;
                    return;
                }
                if (qty <= 0)
                {
                    if (isFocusedOld)
                    {
                        MessageBox.Show($"Số lượng văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        isFocusedOld = false;
                    }
                    grvData.FocusedRowHandle = FocusedRowOld;
                    isFocusedOld = true;
                    return;
                }
                if (quantityReceived < 0)
                {
                    if (isFocusedOld)
                    {
                        MessageBox.Show($"Số lượng văn phòng phẩm dòng số [{stt}] của nhân viên [{name}] không dược nhỏ hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        isFocusedOld = false;
                    }
                    grvData.FocusedRowHandle = FocusedRowOld;
                    isFocusedOld = true;
                    return;
                }
                if (exceedsLimit && reason == "")
                {
                    if (isFocusedOld)
                    {
                        MessageBox.Show($"Lý do vượt mức dòng số [{stt}] của nhân viên [{name}] không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        isFocusedOld = false;
                    }
                    grvData.FocusedRowHandle = FocusedRowOld;
                    isFocusedOld = true;
                    return;
                }

                DataRow existingRow = dtOSRD.AsEnumerable().FirstOrDefault(row =>
                    row.Field<int>("EmployeeID") == employeeIDDetai &&
                    row.Field<int>("OfficeSupplyID") == officeSupplyID);
                if (existingRow != null)
                {
                    existingRow["STT"] = stt;
                    existingRow["Quantity"] = qty;
                    existingRow["QuantityReceived"] = quantityReceived;
                    existingRow["ExceedsLimit"] = exceedsLimit;
                    existingRow["Reason"] = reason;
                    existingRow["Note"] = note;
                }
                else
                {
                    DataRow newRow = dtOSRD.NewRow();
                    newRow["STT"] = stt;
                    newRow["EmployeeID"] = employeeIDDetai;
                    newRow["OfficeSupplyID"] = officeSupplyID;
                    newRow["Quantity"] = qty;
                    newRow["QuantityReceived"] = quantityReceived;
                    newRow["ExceedsLimit"] = exceedsLimit;
                    newRow["Reason"] = reason;
                    newRow["Note"] = note;

                    dtOSRD.Rows.Add(newRow);
                }
            }

            loadDataDetail(dtOSRD);
        }

        private void grdData_Click(object sender, EventArgs e)
        {
            FocusedRowOld = grvData.FocusedRowHandle;
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        private void grvDataDetail_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvDataDetail.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        #endregion

        private void grvDataDetail_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view == null) return;
            //if (e.RowHandle < 0) return;

            //DataRow dataRow = (DataRow)view.GetDataRow(e.RowHandle)["Note"];

            //if (dataRow == null) return;

           

            //if (view.FocusedColumn == colNote)
            //{
            //    e.RowHeight = (int)view.GetDataRow(e.RowHandle)["Note"];
            //}
            //else if (view.FocusedColumn == colReason)
            //{
            //    e.RowHeight = (int)view.GetDataRow(e.RowHandle)["Reason"];
            //}
        }
    }
}