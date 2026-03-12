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
    public partial class frmFirm : _Forms
    {
        public frmFirm()
        {
            InitializeComponent();
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            loadData();

            //TextUtils.ExcuteSQL($"aa");
        }

        private void loadData()
        {
            //DataTable dt = TextUtils.Select($"SELECT * FROM dbo.Firm ");
            var list = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.IsDelete.ToString(), 0);
            grdData.DataSource = list;
        }

        /// <summary>
        /// cliick add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FirmModel model = new FirmModel();
            frmFirmDetail frm = new frmFirmDetail();
            frm.oFirmModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        /// <summary>
        /// fix tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            editDataProduct();
        }
        /// <summary>
        /// void edit data
        /// </summary>
        private void editDataProduct()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //FirmModel model = (FirmModel)FirmBO.Instance.FindByPK(ID);
            FirmModel model = SQLHelper<FirmModel>.FindByID(ID);
            frmFirmDetail frm = new frmFirmDetail();
            frm.oFirmModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        /// <summary>
        /// delete sản phẩm khỏi danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string firmCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFirmCode));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa có mã: {0} không?", firmCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //FirmBO.Instance.Delete(ID);

                var myDict = new Dictionary<string, object>()
                {
                    { FirmModel_Enum.IsDelete.ToString(),true},
                    { FirmModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { FirmModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                SQLHelper<FirmModel>.UpdateFieldsByID(myDict, ID);
                grvData.DeleteSelectedRows();
            }
        }
        /// <summary>
        /// find data in DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            //findData();
        }
        /// <summary>
        /// event editData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            //if (Global.IsAdmin)
            editDataProduct();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}


