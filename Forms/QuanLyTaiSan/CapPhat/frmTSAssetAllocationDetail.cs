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
    public partial class frmTSAssetAllocationDetail : _Forms
    {
        public TSAssetAllocationModel allocationModel = new TSAssetAllocationModel();

        public frmTSAssetAllocationDetail()
        {
            InitializeComponent();
        }

        private void frmTSAssetAllocationDetail_Load(object sender, EventArgs e)
        {
            loadEmployee();
            LoadDepartment();
            LoadChucVu();
            loadTSAssetAllocation();
            loadTSAsset();

        }
        //Load danh sách nhân viên
        void loadEmployee()
        {
            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, Code, FullName FROM dbo.Employee WHERE Status <> 1");
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = listEmployee;

        }

        private void loadTSAsset()
        {
            //cboTSAsset.DataSource = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "B", new string[] { "@EmployeeID", "@StatusID" }, new object[] { 0, 1 });
            //cboTSAsset.ValueMember = "ID";
            //cboTSAsset.DisplayMember = "TSCodeNCC";

            DataTable dtTSAssetManagement = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A", new string[] { "@EmployeeID", "@StatusID" }, new object[] { 0, 1 });
            if (allocationModel.ID > 0)
            {
                DataTable dtTSAssetMerge = TextUtils.LoadDataFromSP("spGetTSAssetByEmployeeMerge", "B", new string[] { "@TSAssetID", "@TSAssetType" }, new object[] { allocationModel.ID, 0 });
                dtTSAssetManagement.Merge(dtTSAssetMerge);
            }

            cboTSAsset.DataSource = dtTSAssetManagement;

            cboTSAsset.ValueMember = "ID";
            cboTSAsset.DisplayMember = "TSCodeNCC";
        }
        private void loadTSAssetAllocation()
        {
            if (allocationModel.ID > 0)
            {
                txtCode.Text = allocationModel.Code;
                txtNote.Text = allocationModel.Note;
                cboEmployee.EditValue = allocationModel.EmployeeID;
                dtpDateAllocation.Value = TextUtils.ToDate5(allocationModel.DateAllocation);
                if (!Global.IsAdmin)
                {
                    if (allocationModel.IsApprovedPersonalProperty || allocationModel.Status == 1 || allocationModel.IsApproveAccountant)
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
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetTSAssetAllocationDetail", "A", new string[] { "@TSAssetAllocationID" }, new object[] { allocationModel.ID });
        }
        void LoadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM dbo.Department");

            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = listDepartment;

        }
        //Load danh sách chức vụ
        void LoadChucVu()
        {
            List<EmployeeChucVuHDModel> listPosition = SQLHelper<EmployeeChucVuHDModel>.SqlToList("SELECT * FROM dbo.EmployeeChucVuHD");

            cboPossition.Properties.DisplayMember = "Name";
            cboPossition.Properties.ValueMember = "ID";
            cboPossition.Properties.DataSource = listPosition;

        }
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            this.DialogResult = DialogResult.OK;
        }
        private bool save()
        {
            if (!validate()) return false;
            allocationModel.Code = txtCode.Text.Trim();
            allocationModel.DateAllocation = dtpDateAllocation.Value;
            allocationModel.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            allocationModel.Note = txtNote.Text.Trim();
            allocationModel.Status = 0;
            if (allocationModel.ID > 0)
            {
                TSAssetAllocationBO.Instance.Update(allocationModel);
            }
            else
            {
                allocationModel.ID = (int)TSAssetAllocationBO.Instance.Insert(allocationModel);
            }


            for (int i = 0; i < grvData.RowCount; i++)
            {
                TSAssetAllocationDetailModel model = new TSAssetAllocationDetailModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                model.AssetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                model.TSAssetAllocationID = allocationModel.ID;
                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.Quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantity));
                model.EmployeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));
                if (model.AssetManagementID <= 0)
                {
                    continue;
                }

                if (id > 0)
                {
                    TSAssetAllocationDetailBO.Instance.Update(model);
                }
                else
                {
                    TSAssetAllocationDetailBO.Instance.Insert(model);
                }

                //Update thông tin tài sản
                var myDict = new Dictionary<string, object>()
                {
                    {"EmployeeID",TextUtils.ToInt(cboEmployee.EditValue) },
                    {"DepartmentID",TextUtils.ToInt(cboDepartment.EditValue) },
                    {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    {"UpdatedBy",Global.LoginName },
                };
                SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, model.AssetManagementID);
            }
            return true;
        }
        private bool validate()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã cấp phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cboEmployee.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn Người nhận cấp phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpDateAllocation.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày cấp phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
            }
        }

        private void cboTSAsset_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();

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


            if (statusID == 2)
            {
                MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (statusID == 3)
            {
                MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (statusID == 4)
            {
                MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (statusID == 6)
            {
                MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                grvData.SetFocusedRowCellValue(colTSAssetName, assetName);
                grvData.SetFocusedRowCellValue(colQuantity, quantity);
                grvData.SetFocusedRowCellValue(colEmployeeID, employeeID);
            }

        }
        void onEditValueEmployee()
        {

            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, DepartmentID, ChuVuID, ChucVuHDID FROM dbo.Employee");


            if (TextUtils.ToInt(cboEmployee.EditValue) > 0)
            {
                EmployeeModel employee = listEmployee.Where(x => x.ID == TextUtils.ToInt(cboEmployee.EditValue)).FirstOrDefault();

                cboDepartment.EditValue = employee.DepartmentID;
                cboPossition.EditValue = employee.ChucVuHDID;
            }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            onEditValueEmployee();
        }

        void loadCodeNumber()
        {
            if (allocationModel.ID > 0) return;
            DateTime date = dtpDateAllocation.Value;

            TSAssetAllocationModel tSAsset = SQLHelper<TSAssetAllocationModel>.SqlToModel($"SELECT TOP 1 Code FROM dbo.TSAssetAllocation WHERE YEAR(CreatedDate) = {date.Year} AND MONTH(CreatedDate) = {date.Month} AND DAY(CreatedDate) = {date.Day} ORDER BY ID DESC");
            string code = string.IsNullOrEmpty(tSAsset.Code) ? $"TSCP{date.ToString("ddMMyyyy")}00000" : tSAsset.Code;
            int stt = TextUtils.ToInt(code.Substring(code.Length - 5)) + 1;
            string sttText = stt.ToString();

            for (int i = sttText.Length; i < 5; i++)
            {
                sttText = "0" + sttText;
            }

            code = $"TSCP{date.ToString("ddMMyyyy")}" + sttText;

            txtCode.Text = code;
        }

        private void dtpDateAllocation_ValueChanged(object sender, EventArgs e)
        {
            loadCodeNumber();
        }


        List<int> listID = new List<int>();
        private void btnSelectTSAsset_Click(object sender, EventArgs e)
        {
            frmTSAssetEmployee frm = new frmTSAssetEmployee();
            frm.type = 1;
            frm.cboEmployee.Enabled = true;

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
                                grvData.SetRowCellValue(i, colEmployeeID, TextUtils.ToInt(dataRow["EmployeeID"]));
                                break;
                            }

                            if (i == grvData.RowCount - 1) MyLib.AddNewRow(grdData, grvData);
                        }
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}