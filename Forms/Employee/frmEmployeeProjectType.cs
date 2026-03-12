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
    public partial class frmEmployeeProjectType : _Forms
    {
        List<int> selectedList = new List<int>();
        public frmEmployeeProjectType()
        {
            InitializeComponent();
        }

        private void frmEmployeeProjectType_Load(object sender, EventArgs e)
        {
            LoadProjectType();
        }

        private void LoadProjectType()
        {
            List<ProjectTypeModel> lst = SQLHelper<ProjectTypeModel>.FindAll();
            cboProjectType.Properties.DataSource = lst;
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.ValueMember = "ID";
        }
        private void cboProjectType_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            int projectTypeID = TextUtils.ToInt(cboProjectType.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeByProjectTypeID", "lmkTable", new string[] { "@ProjectTypeID" }, new object[] { projectTypeID });
            grdData.DataSource = dt;
        }

        private void ckbIsCheck_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (TextUtils.ToBoolean(e.OldValue) == true)
            {
                selectedList.Remove(empID);
            }
            else
            {
                if(!selectedList.Contains()) selectedList.Add(empID);
            }
        }

        
    }
}
