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

namespace BMS
{
    public partial class frmProductHistoryLoseDetail : _Forms
    {
        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        DataTable _dtName = new DataTable();
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public UsersModel user = new UsersModel();
        private int warehouseID;
        public frmProductHistoryLoseDetail()
        {
            InitializeComponent();
        }
        public frmProductHistoryLoseDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductLose_Load(object sender, EventArgs e)
        {
            loadDataListProduct();
            loadDataBorrow();
            loadName();

        }
        /// <summary>
        /// load list data
        /// </summary>
        private void loadDataListProduct()
        {
            _dtList = TextUtils.Select($"SELECT * FROM ProductRTC WHERE Number > 0 and WarehouseID = {warehouseID}");            
            grdData.DataSource = _dtList;
            
        }
        /// <summary>
        /// load dữ liệu data cho mượn
        /// </summary>
        private void loadDataBorrow()
        {
            _dtB = TextUtils.Select($"SELECT top 1 * FROM ProductRTC where id = 0 and WarehouseID = {warehouseID}");
            grdData2.DataSource = _dtB;
        }
        /// <summary>
        /// load tên user phòng kỹ thuật
        /// </summary>
        void loadName()
        {
            _dtName = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cboNameGetLose.Properties.DisplayMember = "FullName";
            cboNameGetLose.Properties.ValueMember = "ID";
            cboNameGetLose.Properties.DataSource = _dtName;
        }
        /// <summary>
        /// lựa chọn thiết bị để mượn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMove_Click(object sender, EventArgs e)
        {
            //SelectDataBorrow();
            int[] lstIndex = grvData.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], colID));
                if (id == 0) continue;
                DataRow[] rs = _dtList.Select("ID = " + id);
                _dtB.ImportRow(rs[0]);                
            }
            grvData.DeleteSelectedRows();
        }
        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //frmProductHistoryLoseDetail.ActiveForm.Close();
        }
        /// <summary>
        /// return lại thiết bị khi chọn nhầm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int[] lstIndex = grvData2.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(lstIndex[i],colID2));
                if (id == 0) continue;
                DataRow[] rs = _dtB.Select("ID = " + id);
                _dtList.ImportRow(rs[0]);
                //_dtB.Rows.Add(rs[0]);
            }
            grvData2.DeleteSelectedRows();
        }

        bool ValidateForm()
        {
            if (txtLose.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập số lượng thiết bị đã mất để hoàn thành xác nhận!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// xác nhận thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLose_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            for (int i = 0; i < grvData2.RowCount; i++)
            {
                //update số lượng mất, số lượng hiện có vào data ProductRTC
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(i,colID2));
                if (id == 0) return;
                //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(id);
                //model.Number = model.Number - TextUtils.ToDecimal(txtLose.Text.Trim());
                //ProductRTCBO.Instance.Update(model);
                // insert thông tin thiết bị mất vào HistoryProductRTC
                oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetRowCellValue(i,colID2));
                oHistoryModel.NumberBorrow = TextUtils.ToDecimal(txtLose.Text.Trim());
                oHistoryModel.DateBorrow = TextUtils.ToDate(dtpLoseDate.Value.ToString());
                oHistoryModel.PeopleID = TextUtils.ToInt(cboNameGetLose.EditValue.ToString());
                oHistoryModel.Status = 2; // đã mất
                oHistoryModel.Note = txtNote.Text;
                oHistoryModel.WarehouseID = warehouseID;
                HistoryProductRTCBO.Instance.Insert(oHistoryModel);
            }
            //MessageBox.Show("Xác nhận hoàn tất!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
