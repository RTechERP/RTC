using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmTSAssetRecoveryDetail : _Forms
    {
        public TSAssetRecoveryModel recovery = new TSAssetRecoveryModel();
        List<int> listIdDelete = new List<int>();
        public frmTSAssetRecoveryDetail()
        {
            InitializeComponent();
        }

        private void frmTSAssetRecoveryDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadDepartment();
            LoadChucVu();
            loadTSAssetRecovery();
            LoadTSAssetManagement();

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!saveData()) return;
            this.DialogResult = DialogResult.OK;
        }
        void LoadTSAssetManagement()
        {
            //int employeeID = TextUtils.ToInt(cboEmployeeReturn.EditValue);

            //DataTable dtTSAssetManagement = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A", new string[] { "@EmployeeID", "@StatusID" }, new object[] { employeeID, 0 });

            //cboTSAsset.DataSource = dtTSAssetManagement;
            //cboTSAsset.DisplayMember = "TSCodeNCC";
            //cboTSAsset.ValueMember = "ID";

            int employeeID = TextUtils.ToInt(cboEmployeeReturn.EditValue);

            DataTable dtTSAssetManagement = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A", new string[] { "@EmployeeID", "@StatusID" }, new object[] { employeeID, 0 });
            if (recovery.ID > 0)
            {
                DataTable dtTSAssetMerge = TextUtils.LoadDataFromSP("spGetTSAssetByEmployeeMerge", "B", new string[] { "@TSAssetID", "@TSAssetType" }, new object[] { recovery.ID, 2 });
                dtTSAssetManagement.Merge(dtTSAssetMerge);
            }

            cboTSAsset.DataSource = dtTSAssetManagement;

            cboTSAsset.DisplayMember = "TSCodeNCC";
            cboTSAsset.ValueMember = "ID";
        }
        private void loadTSAssetRecovery()
        {
            if (recovery.ID > 0)
            {
                txtCode.Text = recovery.Code;
                txtNote.Text = recovery.Note;
                cboEmployeeReturn.EditValue = recovery.EmployeeReturnID;
                cboEmployeeRecovery.EditValue = recovery.EmployeeRecoveryID;
                dtpDateRecovery.Value = (DateTime)recovery.DateRecovery;
                onEditValueEmployee();

                if (!Global.IsAdmin)
                {
                    if (recovery.IsApprovedPersonalProperty || recovery.Status == 1 || recovery.IsApproveAccountant)
                    {
                        btnSave.Enabled = btnSaveAndClose.Enabled = false;
                    }
                }

            }
            else
            {
                //txtCode.Text = $"SBB{DateTime.Now.ToString("yyyyMMddhhMMss")}";
                loadCodeNumber();
            }
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetTSAssetRecoveryDetail", "A", new string[] { "@TSAssetRecoveryID" }, new object[] { recovery.ID });
        }
        //Load danh sách nhân viên
        void LoadEmployee()
        {
            List<EmployeeModel> listEmployeeFrom = SQLHelper<EmployeeModel>.SqlToList("SELECT ID,Code, FullName FROM dbo.Employee");

            cboEmployeeReturn.Properties.DisplayMember = "FullName";
            cboEmployeeReturn.Properties.ValueMember = "ID";
            cboEmployeeReturn.Properties.DataSource = listEmployeeFrom;


            List<EmployeeModel> listEmployeeTo = SQLHelper<EmployeeModel>.SqlToList("SELECT ID,Code, FullName FROM dbo.Employee WHERE Status <> 1");
            cboEmployeeRecovery.Properties.DisplayMember = "FullName";
            cboEmployeeRecovery.Properties.ValueMember = "ID";
            cboEmployeeRecovery.Properties.DataSource = listEmployeeTo;
            //cboEmployeeRecovery.EditValue = 156;
            cboEmployeeRecovery.EditValue = Global.EmployeeID;

        }

        //Load danh sách phòng ban
        void LoadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM dbo.Department");

            cboDepartmentReturn.Properties.DisplayMember = "Name";
            cboDepartmentReturn.Properties.ValueMember = "ID";
            cboDepartmentReturn.Properties.DataSource = listDepartment;

            cboDepartmentRecovery.Properties.DisplayMember = "Name";
            cboDepartmentRecovery.Properties.ValueMember = "ID";
            cboDepartmentRecovery.Properties.DataSource = listDepartment;

        }

        //Load danh sách chức vụ
        void LoadChucVu()
        {
            List<EmployeeChucVuHDModel> listPosition = SQLHelper<EmployeeChucVuHDModel>.SqlToList("SELECT * FROM dbo.EmployeeChucVuHD");

            cboPossitionReturn.Properties.DisplayMember = "Name";
            cboPossitionReturn.Properties.ValueMember = "ID";
            cboPossitionReturn.Properties.DataSource = listPosition;

            cboPossitionRecovery.Properties.DisplayMember = "Name";
            cboPossitionRecovery.Properties.ValueMember = "ID";
            cboPossitionRecovery.Properties.DataSource = listPosition;

        }
        public bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show(string.Format("Vui lòng nhập Số biên bản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboEmployeeReturn.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboEmployeeRecovery.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (cboEmployeeReturn.EditValue == cboEmployeeRecovery.EditValue)
            if (TextUtils.ToInt(cboEmployeeReturn.EditValue) == TextUtils.ToInt(cboEmployeeRecovery.EditValue))
            {
                MessageBox.Show(string.Format("Không thể thu hồi tài sản cùng 1 nhân viên !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNote.Text))
            {
                MessageBox.Show(string.Format("Vui lòng nhập lý do!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (grvData.RowCount < 1)
            {
                MessageBox.Show(string.Format("Vui lòng nhập thông tin danh sách tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                if (assetManagementID <= 0)
                {
                    MessageBox.Show(string.Format($"Vui lòng chọn tài sản cần thu hồi! (Stt: {stt})"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private bool saveData()
        {
            if (!ValidateForm()) return false;
            grvData.CloseEditor();
            recovery.EmployeeReturnID = TextUtils.ToInt(cboEmployeeReturn.EditValue);
            recovery.EmployeeRecoveryID = TextUtils.ToInt(cboEmployeeRecovery.EditValue);
            recovery.Code = txtCode.Text;
            recovery.DateRecovery = TextUtils.ToDate5(dtpDateRecovery.Value);
            recovery.Note = txtNote.Text;
            //recovery.Status = 0;

            recovery.IsApprovedPersonalProperty = true;//ndnhat update 19/07/2025
            recovery.DateApprovedPersonalProperty = DateTime.Now;//ndnhat update 19/07/2025
            recovery.Status = 0;

            if (recovery.ID > 0)
            {
                TSAssetRecoveryBO.Instance.Update(recovery);
            }
            else
            {
                recovery.ID = (int)TSAssetRecoveryBO.Instance.Insert(recovery);
            }


            //ndnhat update 25/07/2025
            if (listIdDelete.Count > 0)
            {
                foreach (var item in listIdDelete)
                {
                    //var myDict = new Dictionary<string, object>()
                    // {
                    //     {"IsDeleted",1 },
                    // };
                    //SQLHelper<TSAssetRecoveryDetailModel>.UpdateFieldsByID(myDict, item);
                    //TSAssetRecoveryDetailModel tsAssetRecovery = SQLHelper<TSAssetRecoveryDetailModel>.FindByID(item);
                    //tsAssetRecovery.IsDeleted = true;
                    //SQLHelper<TSAssetRecoveryDetailModel>.Update(tsAssetRecovery);

                    TSAssetRecoveryDetailModel delModel = SQLHelper<TSAssetRecoveryDetailModel>.FindByID(item);
                    TSAssetManagementModel tsAssetManagement = SQLHelper<TSAssetManagementModel>.FindByID(delModel.AssetManagementID);
                    delModel.IsDeleted = true;
                    tsAssetManagement.StatusID = delModel.LastTSStatusAssetID;
                    tsAssetManagement.EmployeeID = delModel.LastEmployeeID;
                    SQLHelper<TSAssetRecoveryDetailModel>.Update(delModel);
                    SQLHelper<TSAssetManagementModel>.Update(tsAssetManagement);
                }
            }
            //end ndnhat update 25/07/2025
            for (int i = 0; i < grvData.RowCount; i++)
            {
                TSAssetRecoveryDetailModel model = new TSAssetRecoveryDetailModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                model.AssetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                model.TSAssetRecoveryID = recovery.ID;
                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.Quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantity));
                //ndnhat update 05/08/2025
                TSAssetManagementModel tsAssetManagement = SQLHelper<TSAssetManagementModel>.FindByID(model.AssetManagementID);
                model.LastTSStatusAssetID = tsAssetManagement.StatusID ?? 0;
                model.LastEmployeeID = tsAssetManagement.EmployeeID.Value;
                //end ndnhat update 05/08/2025
                if (model.AssetManagementID <= 0) continue;
                if (id > 0) TSAssetRecoveryDetailBO.Instance.Update(model);
                else
                {
                    TSAssetRecoveryDetailBO.Instance.Insert(model);

                    var myDict = new Dictionary<string, object>()
                    {
                        {"EmployeeID",Global.EmployeeID },
                        {"DepartmentID",Global.DepartmentID },
                        {"StatusID",1 }, //Cu sử dụng
                        {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                        {"UpdatedBy",Global.LoginName },
                    };
                    SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, model.AssetManagementID);
                }

                ////Update thông tin tài sản
                //var myDict = new Dictionary<string, object>()
                //{
                //    {"EmployeeID",TextUtils.ToInt(cboEmployeeRecovery.EditValue) },
                //    {"DepartmentID",TextUtils.ToInt(cboDepartmentRecovery.EditValue) },
                //    {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                //    {"UpdatedBy",Global.LoginName },
                //};

                //SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, model.AssetManagementID);
            }


            return true;
        }
        void onEditValueEmployee()
        {

            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, DepartmentID, ChuVuID, ChucVuHDID FROM dbo.Employee");


            if (TextUtils.ToInt(cboEmployeeReturn.EditValue) > 0)
            {
                EmployeeModel employee = listEmployee.Where(x => x.ID == TextUtils.ToInt(cboEmployeeReturn.EditValue)).FirstOrDefault();

                cboDepartmentReturn.EditValue = employee.DepartmentID;
                cboPossitionReturn.EditValue = employee.ChucVuHDID;
            }

            if (TextUtils.ToInt(cboEmployeeRecovery.EditValue) > 0)
            {
                EmployeeModel employee = listEmployee.Where(x => x.ID == TextUtils.ToInt(cboEmployeeRecovery.EditValue)).FirstOrDefault();

                cboDepartmentRecovery.EditValue = employee.DepartmentID;
                cboPossitionRecovery.EditValue = employee.ChucVuHDID;
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {

            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                if (TextUtils.ToInt(cboEmployeeReturn.EditValue) <= 0)
                {
                    MessageBox.Show(string.Format("Vui lòng chọn nhân viên bị thu hồi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MyLib.AddNewRow(grdData, grvData);
            }
        }

        private void cboTSAsset_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();

            int employeeIDFrom = TextUtils.ToInt(cboEmployeeReturn.EditValue);
            int employeeIDTo = TextUtils.ToInt(cboEmployeeRecovery.EditValue);
            string employeeName = cboEmployeeReturn.Text;

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

            //ndnhat update 25/07/2025
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id > 0)
            {
                Dictionary<string, object> map = new Dictionary<string, object>()
               {
                   {"LastTSStatusAssetID",statusID },
                   {"LastEmployeeID",employeeID },

               };
                SQLHelper<TSAssetRecoveryDetailModel>.UpdateFieldsByID(map, id);
            }
            //End ndnhat update 25/07/2025

            if (employeeIDFrom != employeeID)
            {
                MessageBox.Show($"Tài sản [{assetCode}] không thuộc sử dụng của nhân viên [{employeeName}].\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //grvData.SetFocusedRowCellValue(colAssetManagementID, 0);
                grvData.SetFocusedRowCellValue(colAssetManagementID, 0);//ndnhat update 06/08/2025
                return;
            }
            else
            {
                if (statusID == 1)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể thu hồi !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.SetFocusedRowCellValue(colAssetManagementID, 0);//ndnhat update 06/08/2025
                    return;
                }
                else if (statusID == 3)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể thu hồi !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.SetFocusedRowCellValue(colAssetManagementID, 0);//ndnhat update 06/08/2025
                    return;
                }
                else if (statusID == 4)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể thu hồi !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.SetFocusedRowCellValue(colAssetManagementID, 0);//ndnhat update 06/08/2025
                    return;
                }
                else if (statusID == 6)
                {
                    MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.SetFocusedRowCellValue(colAssetManagementID, 0);//ndnhat update 06/08/2025
                    return;
                }
                else
                {
                    grvData.SetFocusedRowCellValue(colTSAssetName, assetName);
                    grvData.SetFocusedRowCellValue(colQuantity, quantity);
                }
            }
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

        private void cboEmployeeReturn_EditValueChanged(object sender, EventArgs e)
        {
            onEditValueEmployee();
            LoadTSAssetManagement();
        }

        private void cboEmployeeRecovery_EditValueChanged(object sender, EventArgs e)
        {
            onEditValueEmployee();
        }

        void loadCodeNumber()
        {
            if (recovery.ID > 0) return;
            DateTime date = dtpDateRecovery.Value;

            TSAssetRecoveryModel tSAsset = SQLHelper<TSAssetRecoveryModel>.SqlToModel($"SELECT TOP 1 Code FROM dbo.TSAssetRecovery WHERE YEAR(CreatedDate) = {date.Year} AND MONTH(CreatedDate) = {date.Month} AND DAY(CreatedDate) = {date.Day} ORDER BY ID DESC");
            string code = string.IsNullOrEmpty(tSAsset.Code) ? $"TSCP{date.ToString("ddMMyyyy")}00000" : tSAsset.Code;
            int stt = TextUtils.ToInt(code.Substring(code.Length - 5)) + 1;
            string sttText = stt.ToString();

            for (int i = sttText.Length; i < 5; i++)
            {
                sttText = "0" + sttText;
            }

            code = $"TSTH{date.ToString("ddMMyyyy")}" + sttText;

            txtCode.Text = code;
        }

        private void dtpDateRecovery_ValueChanged(object sender, EventArgs e)
        {
            loadCodeNumber();
        }

        List<int> listID = new List<int>();
        private void btnSelectTSAsset_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboEmployeeReturn.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Thu hồi từ!", "Thông báo");
                return;
            }

            frmTSAssetEmployee frm = new frmTSAssetEmployee();
            frm.type = 3;
            frm.cboEmployee.EditValue = cboEmployeeReturn.EditValue;
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