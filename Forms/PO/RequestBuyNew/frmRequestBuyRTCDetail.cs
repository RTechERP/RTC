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
    public partial class frmRequestBuyRTCDetail : _Forms
    {
        public RequestBuyRTCModel model = new RequestBuyRTCModel();
        public string PONumber;
        DataTable dtEmployee = new DataTable();
        DataTable dtDepartment = new DataTable();
        DataTable supplier = new DataTable();
        
       
        public frmRequestBuyRTCDetail()
        {
            InitializeComponent();
        }

        private void frmRequestBuyRTCDetail_Load(object sender, EventArgs e)
        {
            LoadCBProject();
            loadCb();
            loadCbSupplier();
            loadData();
            
            

        }
        void loadData()
        {
            if (model.ID > 0)
            { //{   cbProject.ReadOnly = true;
            //    cboEmployee.EditValue = model.NguoiYeuCauID;
            //    cbTTDH.SelectedValue = model.TinhTrangDonHang;
            //    cbTTTT.SelectedIndex =TextUtils.ToInt(model.TinhTrangTT);
            //    cbSupplier.EditValue= model.SupplierID;
            //    deNgayDat.EditValue = TextUtils.ToDate4(model.NgayDatHang);
            //    deNgayDuKien.EditValue = TextUtils.ToDate4(model.NgayDuKienHangVe);
            //    deNgayThucTe.EditValue = TextUtils.ToDate4(model.NgayVeThucTe);
            //    deHanTT.EditValue = TextUtils.ToDate4(model.HanTT);
            //    txtDonGiaNhap.Text = TextUtils.ToString(model.DonGiaNhap);
            //    txtHD.Text = model.HoaDon;
            //    txtSoPO.Text = PONumber;
            //    txtProductName.Text = model.ProductName_;
            //    txtProductCode.Text = model.ProductCode_;
            //    txtGuestCode.Text = model.GuestCode_;
            //    txtCongNo.Text = TextUtils.ToString(model.CongNo);
            //    txtVat.Text = TextUtils.ToString(model.Vat);
            //    txtThanhTien.Text = TextUtils.ToString(model.ThanhTien);
            //    cbProject.EditValue = model.ProjectID;
            //    txtTenSP.Text = model.TenSPMua;
            //    txtNote.Text = model.GhiChu;
            //    deNgayYeuCau.EditValue = TextUtils.ToDate4(model.NgayNhanYeuCau);
            //    txtSoluong.Text = TextUtils.ToString(model.Qty);

            }
            else
            {
                cbTTDH.SelectedIndex = -1;
                cbTTTT.SelectedIndex = -1;
            }
        }
        void loadCb()
        {
            //employee
            dtEmployee = TextUtils.Select("Select ID,FullName,Code,DepartmentID from Employee");
            cboEmployee.Properties.DataSource = dtEmployee;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            //Department
            dtDepartment = TextUtils.Select("Select * from Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";


            ////TinhTrangThanhToan
            //dtTTTT = TextUtils.Select("Select * from RequesBuyRTCTTTT");
            //cbTTTT.DataSource = dtTTTT;
            //cbTTTT.DisplayMember = "Name";
            //cbTTTT.ValueMember = "ID";


            //TinhTrangDonHang
            DataTable dtTTDH = TextUtils.Select("Select * from RequestBuyRTCTTDH");
            cbTTDH.DataSource = dtTTDH;
            cbTTDH.DisplayMember = "Name";
            cbTTDH.ValueMember = "ID";
        }
        void LoadCBProject()
        {
            //Project
            DataTable dtProject = TextUtils.Select("Select * from Project");
            cbProject.Properties.DataSource = dtProject;
            cbProject.Properties.DisplayMember = "ProjectCode";
            cbProject.Properties.ValueMember = "ID";
        }
        
        void loadCbSupplier()
        {
            supplier = TextUtils.Select("Select * from Supplier");
            cbSupplier.Properties.DataSource = supplier;
            cbSupplier.Properties.DisplayMember = "SupplierName";
            cbSupplier.Properties.ValueMember = "ID";
        }
        bool validate()
        {
            if(model.ID<=0)
            {
                if (cboEmployee.Text == "")
                {
                    MessageBox.Show("Người yêu cầu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //if (cbProject.Text == "")
                //{
                //    MessageBox.Show("Dự án  không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
                if (txtTenSP.Text == "")
                {
                    MessageBox.Show("Tên sản phẩm không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (deNgayYeuCau.Text == "")
                {
                    MessageBox.Show("Ngày yêu cầu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }    
            return true;
        }

        bool save()
        {
            if (!validate()) return false;
           // model.MaSPMua = txtProject.Text;
            //model.TenSPMua = txtTenSP.Text;
            //model.SupplierID =TextUtils.ToInt(cbSupplier.EditValue);
            //model.HoaDon = txtHD.Text;
            //model.NguoiYeuCauID =TextUtils.ToInt(cboEmployee.EditValue);
            //model.PhongBanID =TextUtils.ToInt(cbDepartment.EditValue);
            //model.CongNo = txtCongNo.Text;
            //model.DonGiaNhap =TextUtils.ToDecimal(txtDonGiaNhap.Text);
            //model.Vat =TextUtils.ToDecimal(txtVat.Text);
            //model.ThanhTien =TextUtils.ToDecimal(txtThanhTien.Text);
            //model.TinhTrangTT =TextUtils.ToBoolean(cbTTTT.SelectedIndex);
            //model.TinhTrangDonHang =TextUtils.ToInt(cbTTDH.SelectedValue);
            //model.NgayDatHang = TextUtils.ToDate4(deNgayDat.EditValue);
            //model.NgayDuKienHangVe = TextUtils.ToDate4(deNgayDuKien.EditValue);
            //model.NgayVeThucTe = TextUtils.ToDate4(deNgayThucTe.EditValue);
            //model.HanTT = TextUtils.ToDate4(deHanTT.EditValue);
            //model.NgayNhanYeuCau = TextUtils.ToDate4(deNgayYeuCau.EditValue);
            //model.GhiChu = txtNote.Text;
            //model.Qty = TextUtils.ToInt(txtSoluong.Text);
            //if (model.ID>0)
            //{
            //    RequestBuyRTCBO.Instance.Update(model);
            //}
            //else
            //{
            //    RequestBuyRTCBO.Instance.Insert(model);
            //}


            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            { this.DialogResult = DialogResult.OK; }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(cboEmployee.EditValue);

            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                if (TextUtils.ToInt(dtEmployee.Rows[i]["ID"]) == id)
                {
                    cbDepartment.EditValue = dtEmployee.Rows[i]["DepartmentID"];
                }
            }
        }


        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }


            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Nếu bạn muốn, bạn có thể cho phép nhập số thực với dấu chấm
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierDetail frm = new frmSupplierDetail();
            frm.ShowDialog();
        }

        private void txtVat_EditValueChanged(object sender, EventArgs e)
        {
            int qty = TextUtils.ToInt(txtSoluong.Text);
            int price= TextUtils.ToInt(txtDonGiaNhap.EditValue);
            decimal vat = TextUtils.ToDecimal(txtVat.EditValue);

            decimal s = qty * price;
            txtThanhTien.Text =TextUtils.ToString(s + s * vat/100);
        }

      
    }
}
