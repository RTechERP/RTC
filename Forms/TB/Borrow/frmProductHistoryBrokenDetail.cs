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
    public partial class frmProductHistoryBrokenDetail : _Forms
    {
        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        DataTable _dtName = new DataTable();
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public UsersModel user = new UsersModel();
        public int warehouseID;
        public frmProductHistoryBrokenDetail()
        {
            InitializeComponent();
        }
        public frmProductHistoryBrokenDetail(int WarehouseID)
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
            _dtList = TextUtils.Select($"SELECT * FROM ProductRTC WHERE Number > 0 AND WarehouseID = {warehouseID}");            
            grdData.DataSource = _dtList;
            
        }
        /// <summary>
        /// load dữ liệu data cho mượn
        /// </summary>
        private void loadDataBorrow()
        {
            _dtB = TextUtils.Select("SELECT top 1 * FROM ProductRTC where id = 0");
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
            if (txtLose.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập số lượng thiết bị đã hỏng để hoàn thành xác nhận!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //update số lượng hỏng, số lượng hiện có vào data ProductRTC
            int id = TextUtils.ToInt(grvData2.GetFocusedRowCellValue(colID2));
            if (id == 0) return;
            ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(id);
            model.Number = model.Number - TextUtils.ToDecimal(txtLose.Text.Trim());
            ProductRTCBO.Instance.Update(model);
            //insert thông tin thiết bị hỏng vào HistoryProductRTC
            oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetFocusedRowCellValue(colID2));// xác nhận ID
            oHistoryModel.NumberBorrow = TextUtils.ToDecimal(txtLose.Text.Trim());// số lượng hỏng
            oHistoryModel.DateBorrow = TextUtils.ToDate(dtpLoseDate.Value.ToString());// ngày xác nhận
            oHistoryModel.PeopleID = TextUtils.ToInt(cboNameGetLose.EditValue.ToString());// ID người làm hỏng
            oHistoryModel.Status = 3; // đã hỏng
            oHistoryModel.Note = txtNote.Text;// note
            oHistoryModel.WarehouseID = warehouseID;
            HistoryProductRTCBO.Instance.Insert(oHistoryModel);
            
            this.DialogResult = DialogResult.OK;
            //frmProductBroken.ActiveForm.Close();
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
        /// <summary>
        /// validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtLose.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập số lượng thiết bị đã hỏng để hoàn thành xác nhận!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //update số lượng hỏng, số lượng hiện có vào data ProductRTC
            for (int i = 0; i < grvData2.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(i,colID2));
                if (id == 0) return;
                ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(id);
                model.Number = model.Number - TextUtils.ToDecimal(txtLose.Text.Trim());
                ProductRTCBO.Instance.Update(model);
                //insert thông tin thiết bị hỏng vào HistoryProductRTC
                oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetRowCellValue(i,colID2));// xác nhận ID
                oHistoryModel.NumberBorrow = TextUtils.ToDecimal(txtLose.Text.Trim());// số lượng hỏng
                oHistoryModel.DateBorrow = TextUtils.ToDate(dtpLoseDate.Value.ToString());// ngày xác nhận
                oHistoryModel.PeopleID = TextUtils.ToInt(cboNameGetLose.EditValue.ToString());// ID người làm hỏng
                oHistoryModel.Status = 3; // đã hỏng
                oHistoryModel.Note = txtNote.Text;// note
                oHistoryModel.WarehouseID = warehouseID;
                HistoryProductRTCBO.Instance.Insert(oHistoryModel);
            }
            //MessageBox.Show("Xác nhận hoàn tất!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
