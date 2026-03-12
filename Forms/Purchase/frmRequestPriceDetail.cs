using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmRequestPriceDetail : _Forms
    {
        public RequestPriceDetailModel dModel = new RequestPriceDetailModel();

        public bool IsCopy;

        public frmRequestPriceDetail()
        {
            InitializeComponent();
        }

        private void frmRequestPriceDetail_Load(object sender, EventArgs e)
        {
            loadUser();
            loadData();

           
        }

        #region Methods
        /// <summary>
        /// Lấy danh sách người yêu cầu, người mua hàng
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cbAskPrice.DisplayMember = cbUser.DisplayMember = "FullName";
            cbAskPrice.ValueMember = cbUser.ValueMember = "ID";
            cbAskPrice.DataSource = cbUser.DataSource = dt;
        }

        private bool checkValid()
        {
            if (grvData.RowCount <= 0)
            {
                MessageBox.Show("Xin hãy chọn vật tư yêu cầu vào danh sách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!checkDetail())
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin vật tư(mã, tên).", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        bool checkDetail()
        {
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colPartCode));
                string name = TextUtils.ToString(grvData.GetRowCellValue(i, colPartName));
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))//|| string.IsNullOrWhiteSpace(supplier))
                {
                    return false;
                }
            }
            return true;
        }

        private void loadData()
        {
            try
            {
                //cboProject.EditValue = dModel.ProjectID;
                //cboCustomer.EditValue = dModel.CustomerID;
                //cboStatus.SelectedIndex = dModel.RequestStatus;
                //cboUser.EditValue = dModel.RequestPersonID;

                //txtCode.Text = dModel.RequestPriceCode;
                //txtPurpose.Text = dModel.Purpose;
                //txtNote.Text = dModel.Note;

                //dteDeadLine.EditValue = dModel.DeadLine;

                //DataTable dt = TextUtils.LoadDataFromSP("spGetRequestPriceDetail", "A"
                //   , new string[] { "@RequestPriceID" }
                //   , new object[] { dModel.ID });

                //if (IsCopy)
                //{
                //    dModel.ID = 0;
                //    int count = dt.Rows.Count;
                //    for (int i = 0; i < count; i++)
                //    {
                //        dt.Rows[i]["ID"] = 0;
                //    }
                //}
                DataTable dt = TextUtils.Select($"Select * From RequestPriceDetail Where ID = '{dModel.ID}'");
                grdData.DataSource = dt;
            }
            catch (Exception)
            {
                grdData.DataSource = null;
            }
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCode));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa vật tư [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                if (strID > 0)
                {
                    RequestPriceDetailBO.Instance.Delete(strID);
                }

                grvData.DeleteSelectedRows();
                //calculateFinishTotal();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValid()) return;
                grvData.FocusedRowHandle = -1;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                    RequestPriceDetailModel detail = new RequestPriceDetailModel();
                    if (id > 0)
                    {
                        detail = (RequestPriceDetailModel)RequestPriceDetailBO.Instance.FindByPK(id);
                    }
                    detail.ID = dModel.ID;
                    detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colAdd));
                    detail.PartCode = TextUtils.ToString(grvData.GetRowCellValue(i, colPartCode));
                    detail.PartName = TextUtils.ToString(grvData.GetRowCellValue(i, colPartName));
                    detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                    detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                    detail.Priority = TextUtils.ToInt(grvData.GetRowCellValue(i, colPriority)); // độ ưu tiên
                    detail.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, colManufacturer)); // hãng
                    detail.CreatedDate = TextUtils.ToDate3(grvData.GetRowCellValue(i, colCreatedDate)); // Ngày yc
                    detail.DeadLine = TextUtils.ToDate3(grvData.GetRowCellValue(i, colDeadLine)); // Ngày deadline
                    detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectName)); // dự án
                    detail.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID)); // người yêu cầu
                    detail.Link = TextUtils.ToString(grvData.GetRowCellValue(i, colLink)); // link sp (nếu có)
                    detail.FileName = TextUtils.ToString(grvData.GetRowCellValue(i, colFileName)); // file name được lựa chọn
                    detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote)); // ghi chú
                    if (detail.ID == 0)
                    {
                        RequestPriceDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        RequestPriceDetailBO.Instance.Update(detail);
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
            if (e.Column == colPartCode)
            {
                grvData.Focus();
                string partCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCode));
                DataTable dt = TextUtils.Select($"Select * From RequestPriceDetail Where PartCode = N'{partCode}' order by ID desc");
                DataRow[] rows = dt.Select();
                if (rows.Length > 0)
                {
                    grvData.SetFocusedRowCellValue(colPartName, TextUtils.ToString(rows[0]["PartName"]));
                    grvData.SetFocusedRowCellValue(colUnit, TextUtils.ToString(rows[0]["Unit"]));
                    grvData.SetFocusedRowCellValue(colManufacturer, TextUtils.ToString(rows[0]["Manufacturer"]));
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            int STT;
            try
            {
                DataTable dt = (DataTable)grdData.DataSource;
                DataRow dtrow = dt.NewRow();
                if (dt.Rows.Count == 0) STT = 1;
                else STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
                dtrow["STT"] = STT;
                dtrow["UserID"] = Global.UserID;
                dt.Rows.Add(dtrow);
                grdData.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// button lựa chọn file name 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                grvData.SetFocusedRowCellValue(colFileName, dialog.FileName);
            }
        }

        private void grvData_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colAdd)
                {
                    btnNew_Click(null, null);
                }
            }
        }
    }
}
