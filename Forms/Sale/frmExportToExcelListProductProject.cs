using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace Forms.Sale
{
    public partial class frmExportToExcelListProductProject : _Forms
    {
        public frmExportToExcelListProductProject()
        {
            InitializeComponent();
        }
        string _warehouseCode = "";
        public frmExportToExcelListProductProject(string warehouseCode)
        {
            InitializeComponent();
            _warehouseCode = warehouseCode;
        }
        private void frmExportToExcelListProductProject_Load(object sender, EventArgs e)
        {
            this.Text += " - " + _warehouseCode;
            loadProject();
            loadData();
        }

        /// <summary>
        /// Load dự án lên combo
        /// </summary>
        void loadProject()
        {
            
            DataTable dataTable = TextUtils.Select($"select ID, ProjectCode, ProjectName from Project order by ID desc");
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = dataTable;
        }

        /// <summary>
        /// Load danh sách sản phẩm
        /// </summary>
        void loadData()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            string projectCode = cboProject.Properties.GetDisplayTextByKeyValue(projectID);

            DataTable dataTable = TextUtils.LoadDataFromSP(StoreProcedure.spGetListProductImportExportByProjectID_New, "A",
                                                            new string[] { "@projectId", "@projectCode" , "@WarehouseCode" },
                                                            new object[] { projectID, projectCode,_warehouseCode });
            grdData.DataSource = dataTable;
        }


        /// <summary>
        /// Load DS sản phẩm khi chọn dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn dự án !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TextUtils.ExportExcel(grvData);
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
