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
    public partial class frmTypeAsset : _Forms
    {
        public frmTypeAsset()
        {
            InitializeComponent();
        }

        private void frmTypeAsset_Load(object sender, EventArgs e)
        {
            LoadAsset();
        }
        void LoadAsset()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.TSAsset");
            grdData.DataSource = dt;
        }

        #region Button
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTypeAssetDetail frm = new frmTypeAssetDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadAsset();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            TSAssetModel model = (TSAssetModel)TSAssetBO.Instance.FindByPK(ID);
            frmTypeAssetDetail frm = new frmTypeAssetDetail();
            frm.tsas = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadAsset();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colAssetCode));
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa mã {0} không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TSAssetBO.Instance.Delete(ID);
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
