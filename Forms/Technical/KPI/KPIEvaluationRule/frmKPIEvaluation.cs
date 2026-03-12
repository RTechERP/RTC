using BMS.Model;
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
    public partial class frmKPIEvaluation : _Forms
    {
        public string deName;
        public int deparmentID = 0;
        public frmKPIEvaluation()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluation_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadData();
        }
        private void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIEvaluation", "LMKTable", new string[] { "@DepartmentID" }, new object[] { deparmentID});
            grdData.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationDetails frm = new frmKPIEvaluationDetails();
            frm.deparmentID = deparmentID;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                int rowHanlde = grvData.FocusedRowHandle;
                LoadData();
                grvData.FocusedRowHandle = rowHanlde;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            KPIEvaluationModel model = SQLHelper<KPIEvaluationModel>.FindByID(TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID)));
            if (model.ID <= 0) return;
            frmKPIEvaluationDetails frm = new frmKPIEvaluationDetails();
            frm.detail = model;
            frm.deparmentID = deparmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int rowHanlde = grvData.FocusedRowHandle;
                LoadData();
                grvData.FocusedRowHandle = rowHanlde;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            KPIEvaluationModel model = SQLHelper<KPIEvaluationModel>.FindByID(TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID)));
            if (model.ID <= 0) return;
            if(MessageBox.Show($"Bạn có chắc chắn muốn xóa mã nội dung [{model.EvaluationCode}] không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    { "IsDeleted", true}
                };
                SQLHelper<KPIEvaluationModel>.UpdateFieldsByID(newDict, model.ID);
                LoadData();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
