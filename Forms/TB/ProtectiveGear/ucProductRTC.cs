using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class ucProductRTC : UserControl
    {
        public int productRTCID = 0;
        public int histortyID = 0;
        public int statusProduct = 0;

        //lee min khooi update 11/10/2024
        List<int> hcnsIDs = SQLHelper<vUserGroupLinkModel>.FindByAttribute("Code", "N34").Select(p => p.UserID).ToList();
        bool isAdmin = false;


        bool isClick = false;

        public ucProductRTC()
        {
            InitializeComponent();
        }

        private void ucProductRTC_Load(object sender, EventArgs e)
        {
            isAdmin = hcnsIDs.Contains(Global.UserID) || (Global.IsAdmin && Global.EmployeeID <= 0);

            btnApprovedReturn.Enabled = isAdmin;
            btnDuyenMuon.Enabled = isAdmin;
            btnEdit.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
            btnApprovedGiaHan.Enabled = isAdmin;
            btnWashing.Enabled = isAdmin;
            btnBackToUse.Enabled = isAdmin;


            Panel panel = (Panel)this.Parent;
            panel.AllowDrop = isAdmin;
        }

        void UpdateStatus(int status)
        {
            string statusText = status == 0 ? "đưa vào sử dụng" : "đang giặt";
            //int productRTCID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));

            if (productRTCID <= 0) return;

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn cập nhật thành {statusText} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    { ProductRTCModel_Enum.Status.ToString(),status},
                };

                SQLHelper<ProductRTCModel>.UpdateFieldsByID(myDict, productRTCID);

                //LoadData();
            }


        }


        private void ucProductRTC_MouseDown(object sender, MouseEventArgs e)
        {
            //this.DoDragDrop(this, DragDropEffects.Move);
        }

        private void btnMove_MouseDown(object sender, MouseEventArgs e)
        {
            this.DoDragDrop(this, DragDropEffects.Move);
            isClick = true;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(id.ToString());

            HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
            if (model.Status > 0)
            {
                MessageBox.Show($"{lblProductName.Text} đang được mượn!", "Thông báo");
                return;
            }
            else if (statusProduct == 1)
            {
                MessageBox.Show($"{lblProductName.Text} đang giặt!", "Thông báo");
                return;
            }

            frmHistoryProductRTCDetailProtectiveGear frm = new frmHistoryProductRTCDetailProtectiveGear();
            frm.productRTCID = productRTCID;
            if (frm.ShowDialog() == DialogResult.Yes)
            {

            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {

                //int selectedRowHandle = selectedRowHandles[i];
                //int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                //int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                if (histortyID <= 0) return;
                HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
                bool isValid = model.Status != 1 && model.Status != 4 && model.Status != 7;
                if (isValid) return;


                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                if (isAdmin)
                {
                    model.Status = 0;
                    model.DateReturn = DateTime.Now;
                    model.AdminConfirm = true;

                    SQLHelper<HistoryProductRTCModel>.Update(model);
                    //HistoryProductRTCBO.Instance.Update(model);
                    //int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                    //TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                }
                else
                {
                    model.Status = 4;
                    //HistoryProductRTCBO.Instance.Update(model);
                    SQLHelper<HistoryProductRTCModel>.Update(model);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isAdmin) return;
                //int[] selectedRowHandles = grvData.GetSelectedRows();
                //if (selectedRowHandles.Length <= 0)
                //{
                //    MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa!", "Thông báo");
                //    return;
                //}

                HistoryProductRTCModel history = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
                if (histortyID <= 0) return;
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa các phiếu mượn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    //List<int> lstIDs = new List<int>();

                    //for (int i = 0; i < selectedRowHandles.Length; i++)
                    //{
                    //    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandles[i], colID));
                    //    if (id == 0) continue;
                    //    if (!lstIDs.Contains(id)) lstIDs.Add(id);
                    //}

                    //string idText = string.Join(",", lstIDs);
                    Dictionary<string, object> newDict = new Dictionary<string, object>()
                    {
                        {"IsDelete", 1},
                        {"UpdatedBy", Global.AppUserName},
                        {"UpdatedDate", DateTime.Now}
                    };
                    //Expression ex1 = new Expression("ID", idText, "IN");
                    SQLHelper<HistoryProductRTCModel>.UpdateFieldsByID(newDict, histortyID);
                    //LoadData();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + err.Message);
            }
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            try
            {

                //Int32[] selectedRowHandles = grvData.GetSelectedRows();
                //for (int i = 0; i < selectedRowHandles.Length; i++)
                //{
                //    int selectedRowHandle = selectedRowHandles[i];
                //    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                //    if (selectedRowHandle >= 0)
                //    {

                //    }
                //}


                //int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                if (histortyID == 0) return;
                //HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
                //bool isValid = 
                if (model.Status == 0) return;

                //DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn gia hạn sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo);
                //if (rs == DialogResult.No) return;

                frmHistoryProductRTCProtectiveGearExtend frm = new frmHistoryProductRTCProtectiveGearExtend();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //Update lịch sử gia hạn
                    HistoryProductRTCLogModel logModel = new HistoryProductRTCLogModel();
                    logModel.HistoryProductRTCID = histortyID;
                    logModel.DateReturnExpected = frm.dtpDateReturnExpected.Value;
                    SQLHelper<HistoryProductRTCLogModel>.Insert(logModel);


                    model.DateReturnExpected = frm.dtpDateReturnExpected.Value;
                    if (isAdmin)
                    {
                        model.Status = 1;

                        model.AdminConfirm = true;
                        //HistoryProductRTCBO.Instance.Update(model);
                        SQLHelper<HistoryProductRTCModel>.Update(model);
                        //int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                        //TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                    }
                    else
                    {
                        model.Status = 8;
                        //HistoryProductRTCBO.Instance.Update(model);
                        SQLHelper<HistoryProductRTCModel>.Update(model);
                    }
                }





                //LoadData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            List<HistoryProductRTCModel> list = new List<HistoryProductRTCModel>();
            //Int32[] selectedRowHandles = grvData.GetSelectedRows();
            //if (selectedRowHandles.Length <= 0) return;
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{

            //}

            //int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
            //string productCode = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colProductCode));
            //DateTime? dateReturn = TextUtils.ToDate4(grvData.GetRowCellValue(selectedRowHandle, colDate2));
            if (histortyID <= 0) return;
            HistoryProductRTCModel historyProductRTC = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
            if (historyProductRTC.Status == 0)
            {
                MessageBox.Show($"Sản phẩm [{lblProductName.Text}] đã trả! Không thể sửa người mượn!", "Thông báo");
                return;
            };
            //HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
            list.Add(historyProductRTC);

            //if (list.Count <= 0) return;
            frmEditPerson frm = new frmEditPerson();
            frm.list = list;
            frm.ProductName = "";
            frm.ProductCode = "";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //LoadData();
            }
        }

        private void btnDuyenMuon_Click(object sender, EventArgs e)
        {
            //int productHistoryID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
            //int Status = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colStatusNew));

            HistoryProductRTCModel historyProductRTCModel = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
            if (historyProductRTCModel.ID <= 0) return;
            //string statusText = historyProductRTCModel.Status == 7 ? "Duyệt mượn" : (historyProductRTCModel.Status == 8 ? "Duyệt gia hạn" : "");

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string statusText = btnApprovedGiaHan.Text;
            if (item != null) statusText = item.Text;

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {statusText} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (historyProductRTCModel.Status == 7 || historyProductRTCModel.Status == 8)
                {
                    historyProductRTCModel.Status = 1;
                    //historyProductRTCModel.DateReturnExpected = new DateTime(2024,10,31);
                    //historyProductRTCModel.DateReturn = null;
                }
                historyProductRTCModel.IsDelete = false;
                SQLHelper<HistoryProductRTCModel>.Update(historyProductRTCModel);
            }
        }

        private void btnApprovedReturn_Click(object sender, EventArgs e)
        {
            if (histortyID <= 0) return;
            HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(histortyID);
            bool isValid = model.Status != 1 && model.Status != 4 && model.Status != 7;
            if (isValid) return;

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn duyệt trả không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (Global.IsAdmin || isAdmin)
                {
                    model.Status = 0;
                    model.DateReturn = DateTime.Now;
                    model.AdminConfirm = true;
                    //HistoryProductRTCBO.Instance.Update(model);
                    SQLHelper<HistoryProductRTCModel>.Update(model);
                    //int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                    //TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                }
            }
        }

        private void btnApprovedGiaHan_Click(object sender, EventArgs e)
        {
            btnDuyenMuon_Click(null, null);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {

        }

        private void btnMove_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!isClick) return;


            //Panel panel = (Panel)this.Parent;
            //if (e.X == panel.Height - this.Height)
            //{
            //    panel.AutoScrollPosition = new Point(e.X + this.Height, e.Y);
            //}

        }

        private void ucProductRTC_Move(object sender, EventArgs e)
        {

        }

        private void ucProductRTC_MouseMove(object sender, MouseEventArgs e)
        {
            //if (isClick)
            //{
            //    Panel panel = (Panel)this.Parent;
            //    if (e.X == panel.Height - this.Height)
            //    {
            //        panel.AutoScrollPosition = new Point(e.X + this.Height, e.Y);
            //    }
            //}
        }

        private void ucProductRTC_MouseUp(object sender, MouseEventArgs e)
        {
            //isClick = false;
        }

        private void btnWashing_Click(object sender, EventArgs e)
        {
            UpdateStatus(1);
        }

        private void btnBackToUse_Click(object sender, EventArgs e)
        {
            UpdateStatus(0);
        }

        private void lblStatusText_Click(object sender, EventArgs e)
        {

            //barEditItem2.EditValue = $"{lblEmployee.Text}\r\n" +
            //                        $"{lblDateBorrow.Text}\r\n" +
            //                        $"{lblDateExpect.Text}";
            //barEditItem2.EditWidth = 250;
            //barEditItem2.EditHeight = 100;

            //popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            try
            {
                string url = pbImage.ImageLocation;
                if (string.IsNullOrWhiteSpace(url)) return;
                Process.Start(url);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
