using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPIPositionEmployeeDetails : _Forms
    {
        List<int> lstAdd = new List<int>();
        List<int> lstInsert = new List<int>();
        List<int> lstDel = new List<int>();
        public int KPIPostionID = 0;

        int _departmentID = 0; //TN.Binh update 
        int _kpiSessionID = 0; //TN.Binh update 
        public frmKPIPositionEmployeeDetails(int departmentID, int kpiSessionID)
        {
            InitializeComponent();
            _departmentID = departmentID;
            _kpiSessionID = kpiSessionID;
        }

        private void frmKPIPositionEmployeeDetails_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void LoadEmployee()
        {
            //DataTable dt = SQLHelper<EmployeeModel>.LoadDataFromSP("spGetAllEmployeeByKPIPositionID", new string[] { "@KPIPostionID" }, new object[] { KPIPostionID });
            DataTable dt = SQLHelper<EmployeeModel>.LoadDataFromSP("spGetAllEmployeeByKPIPositionID",
                                                    new string[] { "@KPISessionID", "@DepartmentID" },
                                                    new object[] { _kpiSessionID, _departmentID });
            grdData.DataSource = dt;
        }
        private bool SaveData()
        {
            grvData.FocusedRowHandle = -1;
            foreach (int item in lstDel)
            {
                SQLHelper<KPIPositionEmployeeModel>.DeleteModelByID(item);
            }
            lstDel = new List<int>();

            foreach (int item in lstInsert)
            {
                KPIPositionEmployeeModel newModel = new KPIPositionEmployeeModel()
                {
                    KPIPosiotionID = KPIPostionID,
                    EmployeeID = item
                };
                SQLHelper<KPIPositionEmployeeModel>.Insert(newModel);
            }
            lstAdd = new List<int>();
            lstInsert = new List<int>();

            /*SQLHelper<KPIPositionEmployeeModel>.DeleteByAttribute("KPIPosiotionID", KPIPostionID);
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                if (isCheck && (ID > 0))
                {
                    KPIPositionEmployeeModel newModel = new KPIPositionEmployeeModel()
                    {
                        KPIPosiotionID = KPIPostionID,
                        EmployeeID = ID
                    };
                    SQLHelper<KPIPositionEmployeeModel>.Insert(newModel);
                }
            }*/
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                LoadEmployee();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void repositoryItemCheckEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPositionEmployeeID));
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (TextUtils.ToBoolean(e.OldValue) == true) // Trường hợp bỏ chọn
            {
                bool isDelete = MessageBox.Show(
                    $"Bạn có muốn xóa Nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName))}] hay không ?",
                    TextUtils.Caption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes;

                if (!isDelete)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (!lstDel.Contains(ID)) lstDel.Add(ID);
                    lstAdd.Remove(empID);
                    lstInsert.Remove(empID);
                }
            }
            else // Trường hợp chọn
            {
                int positionID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPositionID));

                if (positionID != 0 && positionID == KPIPostionID)
                {
                    // Nhân viên đã có đúng KPI Position rồi => không cần insert nữa

                    lstInsert.Remove(empID);
                    return;
                }
                else if (positionID != 0 && positionID != KPIPostionID)
                {
                    // Nhân viên đã có vị trí khác => hỏi có muốn gán lại không
                    bool isAlready = MessageBox.Show(
                        $"Nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName))}] đã được gán vị trí [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colPositionName))}]! " +
                        $"Bạn có muốn gán lại vị trí cho nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName))}] không?",
                        TextUtils.Caption,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes;

                    if (isAlready)
                    {
                        SQLHelper<KPIPositionEmployeeModel>.DeleteByAttribute("EmployeeID", empID);
                        if (!lstAdd.Contains(empID)) lstAdd.Add(empID);
                        if (!lstInsert.Contains(empID)) lstInsert.Add(empID);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else // positionID == 0 => chưa gán bao giờ
                {
                    if (!lstAdd.Contains(empID)) lstAdd.Add(empID);
                    if (!lstInsert.Contains(empID)) lstInsert.Add(empID);
                }
            }
        }



        private void frmKPIPositionEmployeeDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
