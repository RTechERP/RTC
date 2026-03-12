using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmEmployeeBussinessDetail : _Forms
    {
        //public EmployeeBussinessModel employee = new EmployeeBussinessModel();
        //public EmployeeTypeBussinessModel employeeType = (EmployeeTypeBussinessModel)EmployeeTypeBussinessBO.Instance.FindByPK(1);


        public DataTable dtBussiness = new DataTable();
        ArrayList listId = new ArrayList();
        public frmEmployeeBussinessDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeBussinessDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadOvernightType();
            LoadTypeBussiness();
            LoadData();
        }

        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboEmployee.Properties.DataSource = dataSet.Tables[0];
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployeeApprove.Properties.DataSource = dataSet.Tables[1];
            cboEmployeeApprove.Properties.DisplayMember = "FullName";
            cboEmployeeApprove.Properties.ValueMember = "EmployeeID";
        }

        void LoadOvernightType()
        {
            List<object> list = new List<object>()
            {
                new {ID = 0,Name = "--Chọn loại--"},
                new {ID = 1,Name = "Từ sau 20H"},
                new {ID = 2,Name = "Theo loại CT"},
            }.ToList();

            cboOvernightType.ValueMember = "ID";
            cboOvernightType.DisplayMember = "Name";
            cboOvernightType.DataSource = list;
        }

        void LoadTypeBussiness()
        {
            List<EmployeeTypeBussinessModel> list = SQLHelper<EmployeeTypeBussinessModel>.FindAll();
            cboTypeBussiness.DisplayMember = "TypeName";
            cboTypeBussiness.ValueMember = "ID";
            cboTypeBussiness.DataSource = list;
            cboTypeBussiness.CreateEditor();
        }

        void LoadData()
        {
            if (dtBussiness.Rows.Count > 0)
            {
                cboEmployee.EditValue = dtBussiness.Rows[0]["EmployeeID"];
                cboEmployeeApprove.EditValue = dtBussiness.Rows[0]["ApprovedID"];
                dtpDayBussiness.Value = TextUtils.ToDate5(dtBussiness.Rows[0]["DayBussiness"]);
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDayBussiness.Value = DateTime.Now; ;
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(dtBussiness.Rows.Count > 0);
            grdData.DataSource = dtBussiness;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                dtBussiness = new DataTable();
                listId.Clear();
                LoadData();
            }
        }

        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Họ tên! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeApprove.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Người duyệt! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                string location = TextUtils.ToString(grvData.GetRowCellValue(i, colLocation)).Trim();
                int typeBussiness = TextUtils.ToInt(grvData.GetRowCellValue(i, colTypeBusiness));
                string vehicleName = TextUtils.ToString(grvData.GetRowCellValue(i, colVehicleName)).Trim();
                string reasonEdit = TextUtils.ToString(grvData.GetRowCellValue(i, colReasonHREdit)).Trim();

                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

                if (string.IsNullOrEmpty(location))
                {
                    MessageBox.Show($"Vui lòng nhập Địa điểm [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (typeBussiness <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Loại [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (id > 0)
                {
                    if (string.IsNullOrEmpty(vehicleName))
                    {
                        MessageBox.Show($"Vui lòng nhập Phương tiện [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (string.IsNullOrEmpty(reasonEdit) && !Global.IsAdmin)
                    {
                        MessageBox.Show($"Vui lòng nhập Lý do sửa [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }


            return true;
        }

        bool SaveData()
        {
            grvData.CloseEditor();
            dtBussiness.AcceptChanges();
            if (!CheckValidate())
            {
                return false;
            }

            foreach (DataRow row in dtBussiness.Rows)
            {
                EmployeeBussinessModel bussiness = new EmployeeBussinessModel();
                int id = TextUtils.ToInt(row["ID"]);
                if (id > 0)
                {
                    bussiness = SQLHelper<EmployeeBussinessModel>.FindByID(id);
                }
                bussiness.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                bussiness.ApprovedID = TextUtils.ToInt(cboEmployeeApprove.EditValue);
                bussiness.DayBussiness = dtpDayBussiness.Value.Date;
                bussiness.Location = TextUtils.ToString(row["Location"]);
                bussiness.TypeBusiness = TextUtils.ToInt(row["TypeBusiness"]);
                bussiness.WorkEarly = TextUtils.ToBoolean(row["WorkEarly"]);
                bussiness.OvernightType = TextUtils.ToInt(row["OvernightType"]);
                bussiness.NotChekIn = TextUtils.ToBoolean(row["NotChekIn"]);
                bussiness.ReasonHREdit = TextUtils.ToString(row["ReasonHREdit"]);
                bussiness.Note = TextUtils.ToString(row["Note"]);

                EmployeeTypeBussinessModel typeBussiness = (EmployeeTypeBussinessModel)cboTypeBussiness.GetRowByKeyValue(bussiness.TypeBusiness);
                bussiness.CostBussiness = typeBussiness.Cost;
                bussiness.Overnight = (bussiness.OvernightType > 0);
                bussiness.CostOvernight = bussiness.OvernightType > 0 ? 35000 : 0;
                bussiness.CostWorkEarly = bussiness.WorkEarly ? 50000 : 0;
                decimal costVehicle = TextUtils.ToDecimal(row["TotalCostVehicle"]);
                bussiness.TotalMoney = bussiness.CostBussiness + bussiness.CostOvernight + bussiness.CostWorkEarly + costVehicle;

                if (bussiness.ID > 0)
                {
                    bussiness.IsApproved = bussiness.IsApprovedHR = false;
                    SQLHelper<EmployeeBussinessModel>.Update(bussiness);
                }
                else
                {
                    bussiness.DecilineApprove = 1;
                    SQLHelper<EmployeeBussinessModel>.Insert(bussiness);
                }
            }

            if (listId.Count > 0)
            {
                EmployeeBussinessBO.Instance.Delete(listId);
            }

            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string typeBussinessName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colTypeBusiness));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá công tác [{typeBussinessName}] ngày {dtpDayBussiness.Value.ToString("dd/MM/yyyy")} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                listId.Add(id);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                MessageBox.Show($"Do chưa có cách để thêm Chi phí phương tiện khi tạo công tác.\n" +
                                 $"Vui lòng Lưu lại khai báo công tác trước sau đó cập nhập Chi phí phương tiện.", "Thông báo");
                return;
            }
            frmEmployeeBussinessVehicle frm = new frmEmployeeBussinessVehicle();
            frm.bussiness = new
            {
                ID = id,
                FullName = cboEmployee.Text,
                TypeBussiness = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colTypeBusiness)),
                Location = TextUtils.ToString(grvData.GetFocusedRowCellValue(colLocation)),
                DayBussiness = dtpDayBussiness.Value
            };
            if (frm.ShowDialog() ==  DialogResult.OK)
            {
                var vehicle = frm.vehicleInfo;
                var type = vehicle.GetType();
                grvData.SetFocusedRowCellValue(colVehicleName, TextUtils.ToString(type.GetProperty("VehicleName").GetValue(vehicle)));
                grvData.SetFocusedRowCellValue(colTotalCostVehicle, TextUtils.ToInt(type.GetProperty("TotalCost").GetValue(vehicle)));
                SetTotalCost(grvData.FocusedRowHandle);
            }
        }

        private void cboTypeBussiness_EditValueChanged(object sender, EventArgs e)
        {
            SetTotalCost(grvData.FocusedRowHandle);
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            SetTotalCost(grvData.FocusedRowHandle);
        }

        private void cboOvernightType_EditValueChanged(object sender, EventArgs e)
        {
            SetTotalCost(grvData.FocusedRowHandle);
        }

        void SetTotalCost(int rowHandle)
        {
            grvData.CloseEditor();
            int typeBussinessID = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colTypeBusiness));
            EmployeeTypeBussinessModel typeBussiness = (EmployeeTypeBussinessModel)cboTypeBussiness.GetRowByKeyValue(typeBussinessID);
            decimal costType = typeBussiness == null ? 0 : typeBussiness.Cost;
            decimal costWorkEarly = TextUtils.ToBoolean(grvData.GetRowCellValue(rowHandle, colWorkEarly)) ? 50000 : 0;
            decimal costOvernight = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colOvernightType)) > 0 ? 35000 : 0;
            decimal costVehicle = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colTotalCostVehicle));

            decimal totalCost = costType + costWorkEarly + costOvernight + costVehicle;
            grvData.SetRowCellValue(rowHandle, colTotalCost, totalCost);
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        private void frmEmployeeBussinessDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            DataTable dataChange = dtBussiness.GetChanges();

            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (SaveData())
                    {
                        e.Cancel = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmEmployeeBussinessDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                grvData.FocusedRowHandle = -1;
                DataTable dataChange = dtBussiness.GetChanges();

                if (dataChange != null)
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        btnSave_Click(null, null);

                    }
                    else if (dialog == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    
                }
            }
        }
    }
}
