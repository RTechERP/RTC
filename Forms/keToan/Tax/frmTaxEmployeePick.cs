using System;
using System.Collections.Generic;
using System.ComponentModel;
using BMS;
using BMS.Utils;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using System.Reflection;
using DevExpress.Utils;
using BMS.Business;

namespace BMS
{
    public partial class frmTaxEmployeePick : _Forms
    {
        public frmTaxEmployeePick()
        {
            InitializeComponent();
        }

        private void frmTaxPickEmployee_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            List<EmployeeModel> listEmployeeModel = SQLHelper<EmployeeModel>.SqlToList("SELECT e.ID, e.Code, e.FullName FROM dbo.Employee e LEFT JOIN dbo.TaxEmployee te ON te.EmployeeID = e.ID WHERE te.EmployeeID IS NULL;");
            grdData.DataSource = listEmployeeModel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool save()
        {
            grvData.FocusedRowHandle = -1;

            if (!CheckValidate()) return false;

            Int32[] selectedRowHandles = grvData.GetSelectedRows();

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectRowHandles = selectedRowHandles[i];
                int id = TextUtils.ToInt(grvData.GetRowCellValue(selectRowHandles, colID));
                if (id <= 0) continue;

                TaxEmployeeModel taxEModel = SQLHelper<TaxEmployeeModel>.FindByAttribute("EmployeeID", id).FirstOrDefault();
                if (taxEModel != null) continue;

                EmployeeModel eModel = SQLHelper<EmployeeModel>.FindByID(id);
                if (eModel == null) continue;

                TaxEmployeeModel model = new TaxEmployeeModel();
                PropertyInfo[] eModelProperties = eModel.GetType().GetProperties();
                PropertyInfo[] modelProperties = model.GetType().GetProperties();

                for (int k = 0; k < eModelProperties.Length; k++)
                {
                    if (eModelProperties[k].Name == "ID") continue;
                    for (int j = 0; j < modelProperties.Length; j++)
                    {
                        if (eModelProperties[k].Name == modelProperties[j].Name)
                        {
                            object value = eModelProperties[k].GetValue(eModel);
                            modelProperties[j].SetValue(model, value);
                            break;
                        }
                    }
                }

                model.EmployeeID = eModel.ID;

                // nếu chức vụ nhân viên thuế chưa có thì thêm mới từ bảng chức vụ
                if (eModel.ChucVuHDID > 0)
                {
                    EmployeeChucVuHDModel cvhdModel = SQLHelper<EmployeeChucVuHDModel>.FindByID(TextUtils.ToInt(eModel.ChucVuHDID));
                    if (cvhdModel != null && cvhdModel.Code != null)
                    {
                        TaxEmployeePositionModel taxEPModel = SQLHelper<TaxEmployeePositionModel>.FindByAttribute("Code", cvhdModel.Code).FirstOrDefault();
                        if (taxEPModel == null)
                        {
                            TaxEmployeePositionModel tempModel = new TaxEmployeePositionModel();
                            tempModel.Code = cvhdModel.Code;
                            tempModel.Name = cvhdModel.Name;

                            tempModel.ID = (int)TaxEmployeePositionBO.Instance.Insert(tempModel);
                            if (tempModel.ID > 0)
                            {

                                model.TaxEmployeePositionID = tempModel.ID;
                            }
                        }
                        else
                        {
                            model.TaxEmployeePositionID = taxEPModel.ID;
                        }
                    }
                }
                // model.TaxCompanyID // chọn sau
                // thêm phòng ban thuế
                if (eModel.DepartmentID > 0)
                {
                    DepartmentModel dModel = SQLHelper<DepartmentModel>.FindByID(TextUtils.ToInt(eModel.DepartmentID));
                    if (dModel != null && eModel.Code != null)
                    {
                        TaxDepartmentModel tdModel = SQLHelper<TaxDepartmentModel>.FindByAttribute("Code", dModel.Code).FirstOrDefault();
                        if (tdModel == null)
                        {
                            TaxDepartmentModel taxDmodel = new TaxDepartmentModel();
                            taxDmodel.Code = dModel.Code;
                            taxDmodel.Name = dModel.Name;
                            taxDmodel.Description = dModel.Description;
                            taxDmodel.Status = dModel.Status;
                            taxDmodel.Email = dModel.Email;
                            taxDmodel.IsShowHotline = dModel.IsShowHotline;
                            taxDmodel.HeadofDepartment = dModel.HeadofDepartment;
                            taxDmodel.PId = dModel.PId;

                            object maxSTT = TextUtils.ExcuteScalar("SELECT MAX(STT) AS MaxSTT FROM TaxDepartment");
                            taxDmodel.STT = maxSTT != DBNull.Value ? TextUtils.ToInt(maxSTT) + 1 : 1;

                            taxDmodel.ID = (int)TaxDepartmentBO.Instance.Insert(taxDmodel);
                            if (taxDmodel.ID > 0)
                            {
                                model.TaxDepartmentID = taxDmodel.ID;
                            }
                        }
                        else
                        {
                            model.TaxDepartmentID = tdModel.ID;
                        }
                    }
                }

                model.ID = (int)TaxEmployeeBO.Instance.Insert(model);

                // insert bảng TaxEmployeeContractModel( hợp dồng lao dộng phòng thuế )
                if (eModel.ID > 0)
                {
                    List<EmployeeContractModel> lsContracts = SQLHelper<EmployeeContractModel>.FindByAttribute("EmployeeID", eModel.ID);
                    if (lsContracts.Count > 0)
                    {
                        for (int b = 0; b < lsContracts.Count; b++)
                        {
                            var contract = lsContracts[b];

                            TaxEmployeeContractModel taxECModel = new TaxEmployeeContractModel();
                            taxECModel.TaxEmployeeID = model.ID;
                            taxECModel.EmployeeLoaiHDLDID = contract.EmployeeLoaiHDLDID;
                            taxECModel.DateStart = contract.DateStart;
                            taxECModel.DateEnd = contract.DateEnd;
                            taxECModel.ContractNumber = contract.ContractNumber;
                            taxECModel.StatusSign = contract.StatusSign;
                            taxECModel.DateSign = contract.DateSign;
                            taxECModel.IsDelete = contract.IsDelete;
                            taxECModel.STT = contract.STT;
                            taxECModel.EmployeeID = contract.EmployeeID;

                            SQLHelper<TaxEmployeeContractModel>.Insert(taxECModel);
                        }
                    }
                }
            }

            return true;
        }

        private bool CheckValidate()
        {
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 nhân viên để thêm nhân viên thuế!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            if (save())
            {
                loadData();
                grvData.FocusedRowHandle = rowhandle;
            }
        }

        private void frmTaxEmployeePick_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
