using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmType : _Forms
    {
        public frmType()
        {
            InitializeComponent();
        }

        private void frmType_Load(object sender, EventArgs e)
        {
            LoadType();
        }

        void LoadType()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.EmployeeTypeOverTime");
            grdData.DataSource = dt; 
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTypeDetail frm = new frmTypeDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadType();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeTypeOvertimeModel model = (EmployeeTypeOvertimeModel)EmployeeTypeOvertimeBO.Instance.FindByPK(ID);
            frmTypeDetail frm = new frmTypeDetail();
            frm.type = model;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadType();
                grvData.FocusedRowHandle = focusedHandle;
            }    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            string Code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeCode));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa mã {0} không?", Code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeTypeOvertimeBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null,null);
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colRatio)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 0)
            //    {
            //        e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString() + "%";
            //    }
            //}
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
