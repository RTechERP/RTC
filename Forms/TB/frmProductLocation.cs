using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;

namespace Forms.TB
{
    public partial class frmProductLocation : _Forms
    {
        public frmProductLocation()
        {
            InitializeComponent();
        }

        private void frmDeviceLocation_Load(object sender, EventArgs e)
        {

        }
        void loadData()
        {
            DataTable dt = TextUtils.Select("select * from ProductLocation");
            grdData.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProductLocationDetails frm = new frmProductLocationDetails();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ProductLocationModel model = (ProductLocationModel)ProductLocationBO.Instance.FindByPK(ID);
            frmProductLocationDetails frm = new frmProductLocationDetails();
            frm.location = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if(MessageBox.Show("Bạn có chắc muốn xoá vị trí này ?",TextUtils.Caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductLocationBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }
    }
}
