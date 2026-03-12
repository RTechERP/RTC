using BMS.Business;
using BMS.Model;
using BMS.Utils;
//using MSScriptControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPODetail : _Forms
    {
        public PODetailModel _PODetail = new PODetailModel();
        public POModel _PO = new POModel();
        DataTable dtt = new DataTable();
        //string _Code = "";
        public frmPODetail()
        {
            InitializeComponent();
        }
        private void frmPODetail_Load(object sender, EventArgs e)
        {
            // chỉnh tabelelayout chiếm 3 cột
            tableLayoutPanel1.SetColumnSpan(cboKhachHang, 2);
            tableLayoutPanel1.SetColumnSpan(txtDuan,2);
            tableLayoutPanel1.SetColumnSpan(lblDongiakhach, 2);

            loadCustomer();
            loadData();
            loadCbName();
            //this.cbName.EditValueChanged += new System.EventHandler(cbName_EditValueChanged);

        }

        DataTable dtKhachHang;
        // khách hàng
        private void loadCustomer()
        {
            dtKhachHang = new DataTable();
            dtKhachHang = TextUtils.Select("SELECT * FROM ProductKhachHang");
            cboKhachHang.Properties.DisplayMember = "TenKiHieu";
            cboKhachHang.Properties.ValueMember = "ID";
            cboKhachHang.Properties.DataSource = dtKhachHang;
        }


        private void loadData()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("[spLoadPONote]", new string[] { "@BillID" }, new object[] { _PO.ID });
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtDateTime.Text = TextUtils.ToString(dt.Rows[0]["CreateDate"]);
            txtDuan.Text = _PO.DuanID;
            cboKhachHang.EditValue = _PO.KhachhangID;
            txtDonbaogia.Text = _PO.PriceKhach.ToString();
        }
        private void loadCbName()
        {
            dtt = TextUtils.Select("SELECT DISTINCT pf.ID, pf.ProductCode, pf.ProductName FROM ProductSALE pf LEFT JOIN dbo.ProductSALE p ON pf.ProductGroupID= p.ID");

            cbName.DataSource = dtt;
            cbName.ValueMember = "ID";
            cbName.DisplayMember = "ProductName";
            colName.ColumnEdit = cbName;
        }

        bool saveData()
        {
            if (!ValidateForm()) return false;

            // focus: trỏ đến -> lưu và cất đi luôn
            grvData.Focus();
            //txtKhachHangDuAn.Focus();

            string nam = DateTime.Now.ToString("yyyy");
            string thang = DateTime.Now.ToString("MM");
            string ngay = DateTime.Now.ToString("dd");
            int soluong = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colSoLuong));
            _PO.DuanID = txtDuan.Text.Trim();
            _PO.DuanName = cboKhachHang.Text + "_" + ngay + thang + nam + "_" + txtDuan.Text;
            _PO.PriceKhach = TextUtils.ToDecimal(txtDonbaogia.Text) ;
            _PO.TotalpriceKhach = (TextUtils.ToDecimal(txtDonbaogia.Text) * soluong * 110) / 100;
            _PO.KhachhangID = TextUtils.ToInt(cboKhachHang.EditValue);
            _PO.CreateDate = TextUtils.ToDate3(txtDateTime.Value);
            if (_PO.ID > 0)
            {
                POBO.Instance.Update(_PO);
            }
            else
            {
                _PO.ID = (int)POBO.Instance.Insert(_PO);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                PODetailModel _PODetail = new PODetailModel();
                string a = TextUtils.ToString(grvData.GetRowCellValue(i, colModelDuan));
                if (a.Trim() == "") continue;
                _PODetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                _PODetail.BillID = _PO.ID;//Liên kết bảng Nhập Xuất
                _PODetail.POID = _PODetail.POID = TextUtils.ToInt(grvData.GetRowCellValue(i, colName));//ID Sản phẩm
                _PODetail.SoLuong = TextUtils.ToInt(grvData.GetRowCellValue(i, colSoLuong));
                _PODetail.ModelDuan = TextUtils.ToString(grvData.GetRowCellValue(i, colModelDuan));
                _PODetail.ModelChuan = TextUtils.ToString(grvData.GetRowCellValue(i, colModelChuan));
                if (_PODetail.ID > 0)
                {
                    PODetailBO.Instance.Update(_PODetail);
                }
                else
                {
                    PODetailBO.Instance.Insert(_PODetail);
                }
            }
            return true;
        
        }

        private bool ValidateForm()
        {
            if (txtDuan.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtDonbaogia.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đơn báo giá khách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                _PO = new POModel();
                txtDuan.Text = "";
                txtDonbaogia.Text = "";
                cboKhachHang.Text = "";
            }
            loadData(); 
        }
        
        private void frmGoodsDeliveryNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        // click vào khách hàng để tự động hiển thị ra địa chỉ
        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtKhachHang.Rows.Count <= 0) return;
            //if (cboKhachHang.Text.Trim() == "") return;
            //DataRow[] dr = dtKhachHang.Select($"ID={cboKhachHang.EditValue}");
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grvData.AddNewRow();
            grvData.FocusedColumn = grvData.VisibleColumns[0];
            grvData.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            addRowToolStripMenuItem_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
                return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colName));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                try
                {
                    if (strID > 0)
                    {
                        PODetailBO.Instance.Delete(strID);
                    }
                    grvData.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        private void btnSaveDong_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                _PO = new POModel();
                txtDuan.Text = "";
                txtDonbaogia.Text = "";
                cboKhachHang.Text = "";
            }
            loadData();
            this.Close();
        }
    }
}
