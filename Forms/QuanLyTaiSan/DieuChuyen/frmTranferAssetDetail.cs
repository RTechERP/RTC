using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmTranferAssetDetail : _Forms
    {

        public TSTranferAssetModel transfer = new TSTranferAssetModel();
        public TSAllocationEvictionAssetModel tslocation = new TSAllocationEvictionAssetModel();
        public TSAssetManagementModel asset = new TSAssetManagementModel();

        List<int> listIdDelete = new List<int>();

        public frmTranferAssetDetail()
        {
            InitializeComponent();
        }

        private void frmTranferAssetDetail_Load(object sender, EventArgs e)
        {

            LoadEmployee();
            LoadDepartment();
            LoadchucVu();

            LoadDataTransfer();
            LoadTSAssetManagement();
        }

        //Load chi tiết biên bản
        void LoadDataTransfer()
        {
            if (transfer.ID > 0)
            {
                txtSoChungTu.Text = transfer.CodeReport;
                dtpNgayDieuChuyen.Value = transfer.TranferDate.HasValue == true ? transfer.TranferDate.Value : DateTime.Now;
                cboTuPhongBan.EditValue = transfer.FromDepartmentID;
                cboDenPhongBan.EditValue = transfer.ToDepartmentID;
                cboTuNhanVien.EditValue = transfer.DeliverID;
                cboDenNhanVien.EditValue = transfer.ReceiverID;
                cboTuChucVu.EditValue = transfer.FromChucVuID;
                cboDenChucVu.EditValue = transfer.ToChucVuID;
                txtGhiChu.Text = transfer.Reason;

                if (transfer.IsApprovedPersonalProperty || transfer.IsApproved || transfer.IsApproveAccountant)
                {
                    btnSave.Enabled = btnSaveAndClose.Enabled = false;
                }
            }
            else
            {
                txtSoChungTu.Text = $"SBB{DateTime.Now.ToString("yyyyMMddhhMMss")}";
            }


            DataTable dt = TextUtils.LoadDataFromSP("spGetTranferAssetDetail", "A", new string[] { "@TranferAssetID" }, new object[] { transfer.ID });
            grdData.DataSource = dt;
        }


        //Load danh sách tài sản
        void LoadTSAssetManagement()
        {
            //int employeeID = TextUtils.ToInt(cboTuNhanVien.EditValue);

            //DataTable dtTSAssetManagement = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A", new string[] { "@EmployeeID", "@StatusID" }, new object[] { employeeID, 0 });

            //cboTSAsset.DataSource = dtTSAssetManagement;
            //cboTSAsset.DisplayMember = "TSCodeNCC";
            //cboTSAsset.ValueMember = "ID";

            int employeeID = TextUtils.ToInt(cboTuNhanVien.EditValue);

            DataTable dtTSAssetManagement = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A", new string[] { "@EmployeeID", "@StatusID" }, new object[] { employeeID, 0 });
            if (transfer.ID > 0)
            {
                DataTable dtTSAssetMerge = TextUtils.LoadDataFromSP("spGetTSAssetByEmployeeMerge", "B", new string[] { "@TSAssetID", "@TSAssetType" }, new object[] { transfer.ID, 1 });
                dtTSAssetManagement.Merge(dtTSAssetMerge);
            }

            cboTSAsset.DataSource = dtTSAssetManagement;

            cboTSAsset.DisplayMember = "TSCodeNCC";
            cboTSAsset.ValueMember = "ID";
        }


        //Load danh sách nhân viên
        void LoadEmployee()
        {
            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT * FROM dbo.Employee");

            cboTuNhanVien.Properties.DisplayMember = "FullName";
            cboTuNhanVien.Properties.ValueMember = "ID";
            cboTuNhanVien.Properties.DataSource = listEmployee;

            cboDenNhanVien.Properties.DisplayMember = "FullName";
            cboDenNhanVien.Properties.ValueMember = "ID";
            cboDenNhanVien.Properties.DataSource = listEmployee;

        }

        //Load danh sách phòng ban
        void LoadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM dbo.Department");

            cboTuPhongBan.Properties.DisplayMember = "Name";
            cboTuPhongBan.Properties.ValueMember = "ID";
            cboTuPhongBan.Properties.DataSource = listDepartment;

            cboDenPhongBan.Properties.DisplayMember = "Name";
            cboDenPhongBan.Properties.ValueMember = "ID";
            cboDenPhongBan.Properties.DataSource = listDepartment;

        }

        //Load danh sách chức vụ
        void LoadchucVu()
        {
            List<EmployeeChucVuHDModel> listPosition = SQLHelper<EmployeeChucVuHDModel>.SqlToList("SELECT * FROM dbo.EmployeeChucVuHD");

            cboTuChucVu.Properties.DisplayMember = "Name";
            cboTuChucVu.Properties.ValueMember = "ID";
            cboTuChucVu.Properties.DataSource = listPosition;

            cboDenChucVu.Properties.DisplayMember = "Name";
            cboDenChucVu.Properties.ValueMember = "ID";
            cboDenChucVu.Properties.DataSource = listPosition;

        }

        public bool ValidateForm()
        {
            if (cboTuPhongBan.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào phòng ban !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboDenPhongBan.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào phòng ban !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboTuNhanVien.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboDenNhanVien.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboTuNhanVien.EditValue == cboDenNhanVien.EditValue)
            {
                MessageBox.Show(string.Format("Không thể điều chuyển tài sản cùng 1 nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboTuChucVu.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào chức vụ !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboDenChucVu.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào chức vụ !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtGhiChu.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập lý do !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (grvData.RowCount < 1)
            {
                MessageBox.Show(string.Format("Vui lòng nhập thông tin danh sách tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    DataRow row = grvData.GetDataRow(i);
            //    if (row != null)
            //    {
            //        // Check giá trị ở cột cụ thể

            //        string assetName = row.IsNull("TSAssetName") ? null : Convert.ToString(row["TSAssetName"]);
            //        //string assetCode = row.IsNull("TSAssetCode") ? null : Convert.ToString(row["TSAssetCode"]);
            //        if (string.IsNullOrEmpty(assetName))
            //        {
            //            MessageBox.Show(string.Format("Vui lòng nhập đầy đủ thông tin danh sách tài sản  "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return false;
            //        }
            //    }

            //}
            return true;
        }

        bool saveData()
        {
            if (!ValidateForm()) return false;

            transfer.DeliverID = TextUtils.ToInt(cboTuNhanVien.EditValue);
            transfer.ReceiverID = TextUtils.ToInt(cboDenNhanVien.EditValue);
            transfer.FromChucVuID = TextUtils.ToInt(cboTuChucVu.EditValue);
            transfer.ToChucVuID = TextUtils.ToInt(cboDenChucVu.EditValue);
            transfer.FromDepartmentID = TextUtils.ToInt(cboTuPhongBan.EditValue);
            transfer.ToDepartmentID = TextUtils.ToInt(cboDenPhongBan.EditValue);
            transfer.CodeReport = txtSoChungTu.Text;
            transfer.TranferDate = dtpNgayDieuChuyen.Value;
            transfer.Reason = txtGhiChu.Text;
            transfer.IsApproved = false;

            if (transfer.ID > 0)
            {
                //transfer.UpdatedBy = Global.AppFullName;
                //transfer.UpdatedDate = DateTime.Now;

                TSTranferAssetBO.Instance.Update(transfer);
            }
            else
            {
                //transfer.CreatedBy = Global.AppFullName;
                //transfer.CreatedDate = DateTime.Now;
                //transfer.UpdatedBy = Global.AppFullName;
                //transfer.UpdatedDate = DateTime.Now;

                transfer.ID = (int)TSTranferAssetBO.Instance.Insert(transfer);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                TSTranferAssetDetailModel model = new TSTranferAssetDetailModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                model.AssetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                model.TSTranferAssetID = transfer.ID;
                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.Quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantity));

                if (model.AssetManagementID <= 0)
                {
                    continue;
                }

                if (id > 0)
                {
                    TSTranferAssetDetailBO.Instance.Update(model);
                }
                else
                {
                    TSTranferAssetDetailBO.Instance.Insert(model);
                }


                //Update thông tin tài sản
                var myDict = new Dictionary<string, object>()
                {
                    {"EmployeeID",TextUtils.ToInt(cboDenNhanVien.EditValue) },
                    {"DepartmentID",TextUtils.ToInt(cboDenPhongBan.EditValue) },
                    {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    {"UpdatedBy",Global.LoginName },
                };

                SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, model.AssetManagementID);
                //TSAllocationEvictionAssetModel evictionAsset = TSAllocationEvictionAssetBO.Instance.FindByAttribute("AssetManagementID", model.AssetManagementID).Cast<TSAllocationEvictionAssetModel>().FirstOrDefault();

                //if (evictionAsset != null)
                //{
                //    evictionAsset.EmployeeID = transfer.ReceiverID;
                //    evictionAsset.ChucVuID = transfer.ToChucVuID;
                //    evictionAsset.DepartmentID = transfer.ToDepartmentID;
                //    TSAllocationEvictionAssetBO.Instance.Update(evictionAsset);
                //}

                //DataTable dtlocation = TextUtils.Select($"Select TOP 1 ID from dbo.TSAllocationEvictionAsset where AssetManagementID = '{model.AssetManagementID}' order by ID DESC");
                //if (dtlocation.Rows.Count == 0)
                //{

                //}
                //else
                //{
                //    int ID = TextUtils.ToInt(dtlocation.Rows[0]["ID"]);
                //    tslocation = (TSAllocationEvictionAssetModel)TSAllocationEvictionAssetBO.Instance.FindByPK(ID);

                //    if (tslocation == null)
                //    {

                //    }
                //    else
                //    {
                //        tslocation.Status = "Đã điều chuyển";
                //        DataTable dtemployee1 = TextUtils.Select($"Select TOP 1 FullName from dbo.Employee Where ID = '{TextUtils.ToInt(cboDenNhanVien.EditValue)}'");
                //        tslocation.Note = "Đã điều chuyển cho " + TextUtils.ToString(dtemployee1.Rows[0]["FullName"]);

                //        if (tslocation.ID > 0)
                //        {
                //            TSAllocationEvictionAssetBO.Instance.Update(tslocation);
                //        }
                //        else
                //            tslocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(tslocation);
                //    }
                //}
                //tslocation.AssetManagementID = model.AssetManagementID;
                //tslocation.EmployeeID = TextUtils.ToInt(cboDenNhanVien.EditValue);
                //tslocation.ChucVuID = TextUtils.ToInt(cboDenChucVu.EditValue);
                //tslocation.DepartmentID = TextUtils.ToInt(cboDenPhongBan.EditValue);
                //tslocation.DateAllocation = new DateTime(dtpNgayDieuChuyen.Value.Year, dtpNgayDieuChuyen.Value.Month, dtpNgayDieuChuyen.Value.Day);
                //DataTable dtemployee2 = TextUtils.Select($"Select TOP 1 FullName from dbo.Employee Where ID = '{TextUtils.ToInt(cboTuNhanVien.EditValue)}'");
                //tslocation.Note = "Được điều chuyển từ " + TextUtils.ToString(dtemployee2.Rows[0]["FullName"]);
                //tslocation.Status = "Đang sử dụng";

                //tslocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(tslocation);

                //asset.EmployeeID = tslocation.EmployeeID;
                //asset.DepartmentID = tslocation.DepartmentID;
                //asset.Note = txtGhiChu.Text.Trim();

                //if (model.AssetManagementID > 0)
                //{
                //    TSAssetManagementBO.Instance.Update(asset);
                //}

                //TSAssetManagementModel tSAssetManagement = TSAssetManagementBO.Instance.FindByAttribute("ID", model.AssetManagementID).Cast<TSAssetManagementModel>().FirstOrDefault();
                //if (tSAssetManagement != null)
                //{

                //    tSAssetManagement.EmployeeID = transfer.ReceiverID;
                //    TSAssetManagementBO.Instance.Update(tSAssetManagement);
                //}


                //if (id > 0)
                //{
                //    model.ID = id;
                //    model.UpdatedBy = Global.AppFullName;
                //    model.UpdatedDate = DateTime.Now;
                //    TSTranferAssetDetailBO.Instance.Update(model);
                //}
                //else
                //{
                //    model.CreatedBy = Global.AppFullName;
                //    model.CreatedDate = DateTime.Now;

                //    TSTranferAssetDetailBO.Instance.Insert(model);

                //    dtpNgayDieuChuyen.Value = DateTime.Now;
                //    cboTuPhongBan.EditValue = null;
                //    cboDenPhongBan.EditValue = null;
                //    cboTuNhanVien.EditValue = null;
                //    cboDenNhanVien.EditValue = null;
                //    cboTuChucVu.EditValue = null;
                //    cboDenChucVu.EditValue = null;
                //    txtGhiChu.Text = null;
                //    DataTable dt = TextUtils.LoadDataFromSP("spGetTranferAssetDetail", "A", new string[] { "@TranferAssetID" }, new object[] { 0 });
                //    grdData.DataSource = dt;
                //    //MessageBox.Show(string.Format("Thêm thành công"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}


            }

            //DataTable dtlocation = TextUtils.Select($"Select TOP 1 ID from dbo.TSAllocationEvictionAsset where AssetManagementID = '{asset.ID}' order by ID DESC");
            //if (dtlocation.Rows.Count == 0)
            //{

            //}
            //else
            //{
            //    int ID = TextUtils.ToInt(dtlocation.Rows[0]["ID"]);
            //    tslocation = (TSAllocationEvictionAssetModel)TSAllocationEvictionAssetBO.Instance.FindByPK(ID);

            //    if (tslocation == null)
            //    {

            //    }
            //    else
            //    {
            //        tslocation.Status = "Đã điều chuyển";
            //        DataTable dtemployee1 = TextUtils.Select($"Select TOP 1 FullName from dbo.Employee Where ID = '{TextUtils.ToInt(cboDenNhanVien.EditValue)}'");
            //        tslocation.Note = "Đã điều chuyển cho " + TextUtils.ToString(dtemployee1.Rows[0]["FullName"]);

            //        if (tslocation.ID > 0)
            //        {
            //            TSAllocationEvictionAssetBO.Instance.Update(tslocation);
            //        }
            //        else
            //            tslocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(tslocation);
            //    }
            //}
            //tslocation.AssetManagementID = asset.ID;
            //tslocation.EmployeeID = TextUtils.ToInt(cboDenNhanVien.EditValue);
            //tslocation.ChucVuID = TextUtils.ToInt(cboDenChucVu.EditValue);
            //tslocation.DepartmentID = TextUtils.ToInt(cboDenPhongBan.EditValue);
            //tslocation.DateAllocation = new DateTime(dtpNgayDieuChuyen.Value.Year, dtpNgayDieuChuyen.Value.Month, dtpNgayDieuChuyen.Value.Day);
            //DataTable dtemployee2 = TextUtils.Select($"Select TOP 1 FullName from dbo.Employee Where ID = '{TextUtils.ToInt(cboTuNhanVien.EditValue)}'");
            //tslocation.Note = "Được điều chuyển từ " + TextUtils.ToString(dtemployee2.Rows[0]["FullName"]);
            //tslocation.Status = "Đang sử dụng";

            //tslocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(tslocation);

            //asset.EmployeeID = tslocation.EmployeeID;
            //asset.DepartmentID = tslocation.DepartmentID;
            //asset.Note = txtGhiChu.Text.Trim();

            //if (asset.ID > 0)
            //{
            //    TSAssetManagementBO.Instance.Update(asset);
            //}
            //return true;


            return true;
        }


        void onEditValueEmployee()
        {

            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, DepartmentID, ChuVuID, ChucVuHDID FROM dbo.Employee");


            if (TextUtils.ToInt(cboTuNhanVien.EditValue) > 0)
            {
                EmployeeModel employee = listEmployee.Where(x => x.ID == TextUtils.ToInt(cboTuNhanVien.EditValue)).FirstOrDefault();

                cboTuPhongBan.EditValue = employee.DepartmentID;
                cboTuChucVu.EditValue = employee.ChucVuHDID;
            }

            if (TextUtils.ToInt(cboDenNhanVien.EditValue) > 0)
            {
                EmployeeModel employee = listEmployee.Where(x => x.ID == TextUtils.ToInt(cboDenNhanVien.EditValue)).FirstOrDefault();

                cboDenPhongBan.EditValue = employee.DepartmentID;
                cboDenChucVu.EditValue = employee.ChucVuHDID;
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
            }
        }

        private void cboTuNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            onEditValueEmployee();
            LoadTSAssetManagement();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                foreach (int id in listIdDelete)
                {
                    TSTranferAssetDetailBO.Instance.Delete(id);
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboDenNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            onEditValueEmployee();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int assetId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colAssetManagementID));


            if (assetId > 0)
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có thực sự muốn xoá tài sản [{cboTSAsset.GetDisplayTextByKeyValue(assetId)}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (id > 0)
                    {
                        listIdDelete.Add(id);
                    }

                    grvData.DeleteSelectedRows();
                    listID.Remove(assetId);
                }
            }
            else
            {
                grvData.DeleteSelectedRows();
                listID.Remove(assetId);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                transfer = new TSTranferAssetModel();
                LoadDataTransfer();

                cboTuNhanVien.EditValue = 0;
                cboDenNhanVien.EditValue = 0;

                txtGhiChu.Text = "";
            }
        }

        private void cboTSAsset_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();

            int employeeIDFrom = TextUtils.ToInt(cboTuNhanVien.EditValue);
            int employeeIDTo = TextUtils.ToInt(cboDenNhanVien.EditValue);
            string employeeName = cboTuNhanVien.Text;

            if (employeeIDFrom <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Từ nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grvData.SetFocusedRowCellValue(colAssetManagementID, 0);
                return;
            }

            if (employeeIDTo <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Đến nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grvData.SetFocusedRowCellValue(colAssetManagementID, 0);
                return;
            }

            int assetID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colAssetManagementID));

            if (assetID <= 0)
            {
                return;
            }

            DataRowView dataRow = (DataRowView)cboTSAsset.GetRowByKeyValue(assetID);

            var assetName = dataRow["TSAssetName"];
            var quantity = dataRow["Quantity"];
            int employeeID = TextUtils.ToInt(dataRow["EmployeeID"]);
            int statusID = TextUtils.ToInt(dataRow["StatusID"]);


            string assetCode = TextUtils.ToString(dataRow["TSCodeNCC"]);
            string status = TextUtils.ToString(dataRow["Status"]);

            if (employeeIDFrom != employeeID)
            {
                MessageBox.Show($"Tài sản [{assetCode}] không thuộc sử dụng của nhân viên [{employeeName}].\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grvData.SetFocusedRowCellValue(colAssetManagementID, 0);
                return;
            }
            else
            {
                if (statusID == 1)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (statusID == 3)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (statusID == 4)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (statusID == 6)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    grvData.SetFocusedRowCellValue(colTSAssetName, assetName);
                    grvData.SetFocusedRowCellValue(colQuantity, quantity);
                }
            }


        }


        List<int> listID = new List<int>();
        private void btnSelectTSAsset_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboTuNhanVien.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Từ nhân viên!", "Thông báo");
                return;
            }

            frmTSAssetEmployee frm = new frmTSAssetEmployee();
            frm.type = 2;
            frm.cboEmployee.EditValue = cboTuNhanVien.EditValue;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow dataRow in frm.listRows)
                {
                    int id = TextUtils.ToInt(dataRow["ID"]);
                    if (!listID.Contains(id))
                    {
                        listID.Add(id);
                        if (grvData.RowCount <= 0) MyLib.AddNewRow(grdData, grvData);
                        for (int i = 0; i < grvData.RowCount; i++)
                        {
                            int assetId = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                            if (assetId <= 0)
                            {
                                grvData.SetRowCellValue(i, colSTT, i + 1);
                                grvData.SetRowCellValue(i, colAssetManagementID, id);
                                grvData.SetRowCellValue(i, colTSAssetName, TextUtils.ToString(dataRow["TSAssetName"]));
                                grvData.SetRowCellValue(i, colQuantity, TextUtils.ToInt(dataRow["Quantity"]));
                                //grvData.SetRowCellValue(i, colEmployeeID, TextUtils.ToInt(dataRow["EmployeeID"]));
                                break;
                            }

                            if (i == grvData.RowCount - 1) MyLib.AddNewRow(grdData, grvData);
                        }
                    }
                }

            }
        }
    }
}
