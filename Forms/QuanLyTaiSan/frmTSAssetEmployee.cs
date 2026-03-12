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
    public partial class frmTSAssetEmployee : _Forms
    {
        //public DataRow[] dtAsset;
        public List<DataRow> listRows = new List<DataRow>();

        /// <summary>
        /// 1: Cấp phát
        /// 2: Điều chuyển
        /// 3: Thu hồi
        /// </summary>
        public int type = 0;
        public frmTSAssetEmployee()
        {
            InitializeComponent();
        }

        private void frmTSAssetEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
        }

        void LoadData()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int statusID = type == 1 ? 1 : 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetByEmployee", "A",
                                                                    new string[] { "@EmployeeID", "@StatusID" },
                                                                    new object[] { employeeID, statusID });

            grdData.DataSource = dt;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }


        bool CheckValidate()
        {
            //int[] rowSelecteds = grvData.GetSelectedRows();

            foreach (DataRow row in listRows)
            {
                int employeeID = TextUtils.ToInt(row["EmployeeID"]);// grvData.GetRowCellValue(row, colEmployeeID));
                string employeeName = cboEmployee.Text;
                int employeeIDSelect = TextUtils.ToInt(cboEmployee.EditValue);

                int statusID = TextUtils.ToInt(row["StatusID"]);// grvData.GetRowCellValue(row, colStatusID));

                string status = TextUtils.ToString(row["Status"]);//grvData.GetRowCellValue(row, colStatus));
                string assetCode = TextUtils.ToString(row["TSCodeNCC"]);//grvData.GetRowCellValue(row, colTSCodeNCC));
                if (type == 1)
                {
                    if (statusID == 2)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (statusID == 3)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (statusID == 4)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (statusID == 6)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể Cấp phát !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (type == 2)
                {
                    if (employeeIDSelect != employeeID)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] không thuộc sử dụng của nhân viên [{employeeName}].\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //else
                    //{
                    //    if (statusID == 1)
                    //    {
                    //        MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return false;
                    //    }
                    //    else if (statusID == 3)
                    //    {
                    //        MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return false;
                    //    }
                    //    else if (statusID == 4)
                    //    {
                    //        MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return false;
                    //    }
                    //    else if (statusID == 6)
                    //    {
                    //        MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return false;
                    //    }

                    //}
                }
                else if (type == 3)
                {
                    if (employeeIDSelect != employeeID)
                    {
                        MessageBox.Show($"Tài sản [{assetCode}] không thuộc sử dụng của nhân viên [{employeeName}].\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //grvData.SetFocusedRowCellValue(colAssetManagementID, 0);
                        return false;
                    }
                    else
                    {
                        if (statusID == 1)
                        {
                            MessageBox.Show($"Tài sản [{assetCode}] {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else if (statusID == 3)
                        {
                            MessageBox.Show($"Tài sản [{assetCode}] đang {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else if (statusID == 4)
                        {
                            MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else if (statusID == 6)
                        {
                            MessageBox.Show($"Tài sản [{assetCode}] đã {status}. Không thể điều chuyển !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                    }
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckValidate()) return;

            //int[] rowSelecteds = grvData.GetSelectedRows();

            //foreach (int row in rowSelecteds)
            //{
            //    DataRow dataRow = grvData.GetDataRow(row);
            //    if (dataRow == null) continue;
            //    listRows.Add(dataRow);
            //}

            this.DialogResult = DialogResult.OK;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            bool isSelected = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue("IsSelected"));

            DataRow dataRow = grvData.GetDataRow(grvData.FocusedRowHandle);
            bool isExist = listRows.Contains(dataRow);
            if (isSelected && !isExist)
            {
                listRows.Add(dataRow);
            }
            else if (!isSelected)
            {
                listRows.Remove(dataRow);
            }
        }
    }
}
