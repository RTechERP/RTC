using BMS.Business;
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
    public partial class frmSourceAsset : _Forms
    {
        public frmSourceAsset()
        {
            InitializeComponent();
        }

        private void frmSourceAsset_Load(object sender, EventArgs e)
        {
            LoadDataSource();
        }
        void LoadDataSource()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.TSSourceAsset");
            grdData.DataSource = dt;
        }

        #region Button 
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmSourceAssetDetail frm = new frmSourceAssetDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataSource();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            TSSourceAssetModel model = (TSSourceAssetModel)TSSourceAssetBO.Instance.FindByPK(ID);
            frmSourceAssetDetail frm = new frmSourceAssetDetail();
            frm.source = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataSource();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colSourceCode));
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa mã {0} không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TSSourceAssetBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        #endregion
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null,null);
        }
    }
}
