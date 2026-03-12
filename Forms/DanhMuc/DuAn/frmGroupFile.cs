using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace BMS
{
    public partial class frmGroupFile : _Forms
    {
        public frmGroupFile()
        {
            InitializeComponent();
        }

        private void frmGroupFile_Load(object sender, EventArgs e)
        {
            loadGroupFile();
        }

        #region Methods
        /// <summary>
        /// load SALE
        /// </summary>
        private void loadGroupFile()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetGroupFile", "A", new string[] {"@FilterText"}, new object[] {txtFilterText.Text.Trim()});
            grdData.DataSource = dt;
        }
        #endregion

        #region Button Events
        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupFileModel model = new GroupFileModel();
            frmGroupFileDetail frm = new frmGroupFileDetail();
            frm.groupFileModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGroupFile();
            }
        }

        /// <summary>
        /// click button edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            GroupFileModel model = (GroupFileModel)GroupFileBO.Instance.FindByPK(ID);
            frmGroupFileDetail frm = new frmGroupFileDetail();
            frm.groupFileModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGroupFile();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string groupFileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupFileName));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa nhóm file : [{0}] không?", groupFileName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                GroupFileBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadGroupFile();
        }
        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}


