using DevExpress.XtraEditors;
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
    public partial class frmChooseLeader : _Forms
    {
        public List<int> lstID = new List<int>();
        public frmChooseLeader()
        {
            InitializeComponent();
        }

        private void frmChooseLeader_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            LoadEmployee();

            ckSelect.CheckedChanged += ckSelect_CheckedChanged;
        }

        void LoadDepartment()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Department WITH(NOLOCK)");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }

        void LoadEmployee()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployee", new string[] { "@DepartmentID", "@Status" }, new object[] { departmentID, 0 });
            dt.Columns.Add("IsSelect", typeof(bool));
            grdData.DataSource = dt;
        }
        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            bool isSelection = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsSelection));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (isSelection)
            {
                if (!lstID.Contains(ID))
                    lstID.Add(ID);
            }
            else
            {
                lstID.Remove(ID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelection, true);

                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (!lstID.Contains(id))
                {
                    lstID.Add(id);
                }
            }
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelection, false);

                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                lstID.Remove(id);
            }
        }

        private void cboDepartment_EditValueChanged_1(object sender, EventArgs e)
        {
            LoadEmployee();
        }
    }
}