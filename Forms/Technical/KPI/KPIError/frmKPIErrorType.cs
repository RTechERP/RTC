using BMS.Model;
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
    public partial class frmKPIErrorType : _Forms
    {
        public frmKPIErrorType()
        {
            InitializeComponent();
        }

        private void frmKPIErrorType_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            var dt = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0).OrderBy(p=>p.STT);
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmKPIErrorTypeDetail frm = new frmKPIErrorTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            KPIErrorTypeModel model = SQLHelper<KPIErrorTypeModel>.FindByID(ID);
            frmKPIErrorTypeDetail frm = new frmKPIErrorTypeDetail();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            KPIErrorTypeModel model = SQLHelper<KPIErrorTypeModel>.FindByID(ID);
            if (MessageBox.Show($"Bạn có chắc muốn xóa loại lỗi với mã [{model.Code}] không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                model.IsDelete = true;
                SQLHelper<KPIErrorTypeModel>.Update(model);
                LoadData();
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}