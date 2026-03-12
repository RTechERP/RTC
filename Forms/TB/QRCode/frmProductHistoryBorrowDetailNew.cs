using BMS.Business;
using BMS.Model;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmProductHistoryBorrowDetailNew : _Forms
    {
        public DataTable dtAll = new DataTable();
        List<string> Listqrcode = new List<string>();
        int warehouseID;

        List<string> _qrCodes = new List<string>();
        public frmProductHistoryBorrowDetailNew()
        {
            InitializeComponent();
        }
        public frmProductHistoryBorrowDetailNew(int WarehouseID, List<string> qrCodes)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
            _qrCodes = qrCodes;
        }

        private void frmProductHistoryBorrowDetailNew_Load(object sender, EventArgs e)
        {

            txtQrCode.SelectAll();
            txtQrCode.Focus();
            loadData();
            loadCB();
            txtProject.Text = "Test Văn Phòng";

        }
        void loadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {


                dtAll = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductQrCode, "A", new string[] { }, new object[] { });//spGetProductQrCode
                if (_qrCodes.Count > 0)
                {
                    foreach (var qrCode in _qrCodes)
                    {
                        DataSet dataSet = TextUtils.LoadDataSetFromSP(StoreProcedures.spGetProductRTCByQrCode,
                                new string[] { "@ProductRTCQRCode", "@WarehouseID" },
                                new object[] { qrCode, warehouseID });

                        if (dataSet.Tables[0].Rows.Count > 0) continue;

                        DataTable dt = dataSet.Tables[1];

                        if (dt.Rows.Count <= 0 || dt == null) continue;
                        grvData.BeginDataUpdate();
                        DataRow dr = dtAll.NewRow();
                        dr.BeginEdit();
                        dr["ID"] = dt.Rows[0]["ID"];//ID của ProductRTCQRCode
                        dr["ProductRTCID"] = dt.Rows[0]["ProductRTCID"];
                        dr["ProductQRCode"] = dt.Rows[0]["ProductQRCode"];
                        dr["ProductCode"] = dt.Rows[0]["ProductCode"];
                        dr["ProductName"] = dt.Rows[0]["ProductName"];
                        dr["ProductCodeRTC"] = dt.Rows[0]["ProductCodeRTC"];
                        dr["AddressBox"] = dt.Rows[0]["AddressBox"];
                        dr["Note"] = dt.Rows[0]["Note"];
                        dr["Soluong"] = 1;
                        dr.EndEdit();
                        if (Listqrcode.Contains(TextUtils.ToString(dt.Rows[0]["ProductQRCode"])))
                        {
                            grdData.DataSource = dtAll;
                            grvData.EndDataUpdate();
                        }
                        else
                        {
                            dtAll.Rows.Add(dr);
                            grdData.DataSource = dtAll;
                            grvData.EndDataUpdate();
                        }
                        Listqrcode.Add(TextUtils.ToString(dt.Rows[0]["ProductQRCode"]));
                    }
                }
                grdData.DataSource = dtAll;
            }
        }
        void loadCB()
        {
            //DataTable dt;
            //if (Global.IsAdmin || Global.UserID == 24)
            //{
            //    dt = TextUtils.Select($"Select ID,FullName,Code from Users WHERE Status <> 1");
            //}
            //else
            //{
            //    dt = TextUtils.Select($"Select ID,FullName,Code from Users WHERE ID = {Global.UserID}");
            //}
            //cbUser.Properties.DataSource = dt;
            //cbUser.Properties.DisplayMember = "FullName";
            //cbUser.Properties.ValueMember = "ID";
            int userId = Global.UserID;
            //if (Global.IsAdmin == true || Global.UserID == 24)
            //{
            //    userId = 0;
            //}
            bool isAdmin = Global.IDAdminDemo.Contains(userId);
            if (Global.IsAdmin == true || isAdmin) userId = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "a", new string[] { "@UsersID" }, new object[] { userId });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
            cbUser.EditValue = userId;
        }
        void LoadQrCode()
        {
            if (txtQrCode.Text.Trim().ToLower() == "ok")
            {
                btnSave_Click(null, null);
                return;
            }

            if (txtQrCode.Text.Trim().ToLower() == "logout")
            {
                isLogout = true;
            }

            //DataTable dt = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductRTCByQrCode, "A", new string[] { "@ProductRTCQRCode","@WarehouseID" }, new object[] { txtQrCode.Text.Trim(),warehouseID });
            DataSet dataSet = TextUtils.LoadDataSetFromSP(StoreProcedures.spGetProductRTCByQrCode,
                            new string[] { "@ProductRTCQRCode", "@WarehouseID" },
                            new object[] { txtQrCode.Text.Trim(), warehouseID });

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("Sản phẩm này đã có người mượn.\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = dataSet.Tables[1];

            if (dt.Rows.Count <= 0 || dt == null)
            {
                //ut
                //txtQrCode.SelectAll();
                return;

            }
            grvData.BeginDataUpdate();
            DataRow dr = dtAll.NewRow();
            dr.BeginEdit();
            dr["ID"] = dt.Rows[0]["ID"];//ID của ProductRTCQRCode
            dr["ProductRTCID"] = dt.Rows[0]["ProductRTCID"];
            dr["ProductQRCode"] = dt.Rows[0]["ProductQRCode"];
            dr["ProductCode"] = dt.Rows[0]["ProductCode"];
            dr["ProductName"] = dt.Rows[0]["ProductName"];
            dr["ProductCodeRTC"] = dt.Rows[0]["ProductCodeRTC"];
            dr["AddressBox"] = dt.Rows[0]["AddressBox"];
            dr["Note"] = dt.Rows[0]["Note"];
            dr["Soluong"] = 1;
            dr.EndEdit();
            if (Listqrcode.Contains(TextUtils.ToString(dt.Rows[0]["ProductQRCode"])))
            {
                grdData.DataSource = dtAll;
                grvData.EndDataUpdate();
            }
            else
            {
                dtAll.Rows.Add(dr);
                grdData.DataSource = dtAll;
                grvData.EndDataUpdate();
            }
            Listqrcode.Add(TextUtils.ToString(dt.Rows[0]["ProductQRCode"]));

            txtQrCode.SelectAll();

        }
        private void txtQrCode_TextChanged(object sender, EventArgs e)
        {
            //LoadQrCode();
        }

        private void txtQrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadQrCode();
            }
        }
        bool validate()
        {
            if (cbUser.Text == "")
            {
                MessageBox.Show("Người mượn không được để trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtProject.Text == "")
            {
                MessageBox.Show("Dự án không được để trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool save()
        {
            bool isAdmin = Global.IDAdminDemo.Contains(Global.UserID);
            if (!validate()) return false;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                HistoryProductRTCModel model = new HistoryProductRTCModel();
                model.ProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCID));
                model.ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colId));
                model.DateBorrow = dtpBorrowDate.Value;
                model.DateReturnExpected = dtpReturn.Value;
                model.PeopleID = TextUtils.ToInt(cbUser.EditValue);
                model.Project = txtProject.Text;
                model.Note = TextUtils.ToString(txtNote.Text);
                model.NumberBorrow = TextUtils.ToInt(grvData.GetRowCellValue(i, colSoluong));
                model.Status = 7;
                if (Global.IsAdmin || isAdmin) model.Status = 1;

                model.WarehouseID = warehouseID;
                HistoryProductRTCBO.Instance.Insert(model);

                //31102022  //update status trong bảng ProductRTCQRCode status=2(Đang mượn)
                TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 2 });
                //TextUtils.ExcuteSQL($" spUpdateStatusProductRTCQRCode '{model.ProductRTCQRCodeID}','{2}'");


                //KHông update lại số lượng trong bảng ProductRTC
                //ProductRTCModel pModel = ProductRTCBO.Instance.FindByPK(model.ProductRTCID) as ProductRTCModel;
                //if (pModel != null)
                //{
                //    pModel.NumberInStore -= TextUtils.ToInt(grvData.GetRowCellValue(i, colSoluong));
                //    ProductRTCBO.Instance.Update(pModel);
                //    _Forms frmProduct = (_Forms)Application.OpenForms["frmProductRTC"];
                //    _Forms frmMain = (_Forms)Application.OpenForms["frmMain"];
                //    if (frmProduct != null && frmMain != null)
                //    {
                //        frmProduct.Dispose();
                //        TextUtils.OpenChildForm(new frmProductRTC(), frmMain);
                //        TextUtils.OpenChildForm(new frmProductHistory(), frmMain);
                //        TextUtils.OpenChildForm(new frmAddQRCode(), frmMain);
                //    }
                //}
            }

            return true;
        }

        public bool isLogout = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn mượn các thiết bị này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (save())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if(e.Column==colDelete)
            //{
            //    grvData.DeleteSelectedRows();
            //}    
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCodeRTC));
            string ProductQRCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colQrcode)).Trim();
            if (MessageBox.Show($"Bạn có chắc muốn xoá thiết bị có mã  qrcode {ProductQRCode} không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Listqrcode.Remove(ProductQRCode);
                grvData.DeleteSelectedRows();

            }
        }

        private void frmProductHistoryBorrowDetailNew_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }


    }
}
