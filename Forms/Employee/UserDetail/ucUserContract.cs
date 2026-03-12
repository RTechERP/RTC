using BMS;
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

namespace Forms.Employee.UserDetail
{
    public partial class ucUserContract : UserControl
    {
        /// <summary>
        /// variable
        /// </summary>


        //public  UsersModel usersModel;// l= new r UsersModel();

        public ucUserContract()
        {
            InitializeComponent();
           
        }
        private void ucUserContract_Load(object sender, EventArgs e)
        {
            loadCb();
           // loadData();
            this.cboLoaiHD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           
        }
       public  void loadCb()
        {
            //DataTable dt = TextUtils.Select("Select ID,Name from EmployeeLoaiHDLD");
            //cboLoaiHD.Invoke(new Action(() => TextUtils.PopulateCombo(cboLoaiHD, dt.Copy(), "Name", "ID", "-- Hợp Đồng --")));
            // TextUtils.PopulateCombo(cboLoaiHD, dt.Copy(), "Name", "ID", "-- Hợp Đồng --");

            List<EmployeeLoaiHDLDModel> listContract = SQLHelper<EmployeeLoaiHDLDModel>.FindAll();
            listContract.Insert(0, new EmployeeLoaiHDLDModel() { ID = 0, Name = "Không có" });

            //cboLoaiHD.DataSource = dt;
            cboLoaiHD.DataSource = listContract;
            cboLoaiHD.DisplayMember = "Name";
            cboLoaiHD.ValueMember = "ID";
        }
        public void loadData(ref EmployeeModel employee)
        {
            loadCb();
            //int ID = TextUtils.ToInt(usersModel.LoaiHDLDID);
            if (employee.ID > 0)
            {
                cboLoaiHD.SelectedValue = TextUtils.ToInt(employee.LoaiHDLDID);
                txtTinhTrangKyHD.Text = employee.TinhTrangKyHD;

                if (employee.LoaiHDLDID == 1)
                {
                    dtpDateStart.EditValue = employee.NgayBatDauThuViec;
                    dtpDateEnd.EditValue = employee.NgayKetThucThuViec;
                    txtSoHD.Text = TextUtils.ToString(employee.SoHDTV);
                }
                else if (employee.LoaiHDLDID == 2)
                {
                    dtpDateStart.EditValue = employee.NgayBatDauHDXDTH;
                    dtpDateEnd.EditValue = employee.NgayKetThucHDXDTH;
                    txtSoHD.Text = TextUtils.ToString(employee.SoHDXDTH);
                }
                else 
                {
                    dtpDateStart.EditValue = employee.NgayHieuLucHDKXDTH;
                    txtSoHD.Text = TextUtils.ToString(employee.SoHDKXDTH);
                }  
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboLoaiHD.SelectedValue) == 1)
            {
                label3.Text = "Ngày bắt đầu";
            }
            if (TextUtils.ToInt(cboLoaiHD.SelectedValue) == 2)
            {
                label3.Text = "Ngày hiệu lực";
            }

            if (TextUtils.ToInt(cboLoaiHD.SelectedValue) == 3)
            {
                label3.Text = "Ngày hiệu lực";
                dtpDateEnd.Enabled = false;
               
            }
            else
            {
                dtpDateEnd.Enabled = true;
            }
        }
       
        public bool save(ref EmployeeModel model)
        {
            try
            {
                //if (!validate()) return false;
                model.LoaiHDLDID =TextUtils.ToInt(cboLoaiHD.SelectedValue);
                //model.TinhTrangKyHD = txtTinhTrangKyHD.Text.Trim();
                model.TinhTrangKyHD = rdDaky.Checked ? rdDaky.Text:rdChuaky.Text;

                if (model.LoaiHDLDID == 1)
                {
                    model.NgayBatDauThuViec = TextUtils.ToDate(dtpDateStart.Text);
                    model.NgayKetThucThuViec = TextUtils.ToDate(dtpDateEnd.Text);
                    model.SoHDTV = TextUtils.ToString(txtSoHD.Text);
                }
                else if (model.LoaiHDLDID == 2)
                {
                    model.NgayBatDauHDXDTH = TextUtils.ToDate(dtpDateStart.Text);
                    model.NgayKetThucHDXDTH = TextUtils.ToDate(dtpDateEnd.Text);
                    model.SoHDXDTH = TextUtils.ToString(txtSoHD.Text);
                }
                else
                {
                    model.NgayHieuLucHDKXDTH = TextUtils.ToDate(dtpDateStart.Text);
                    model.SoHDKXDTH = TextUtils.ToString(txtSoHD.Text);
                }
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            } 
        }
        bool validate()
        {
            if(cboLoaiHD.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Loại hợp đồng hiện tại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtTinhTrangKyHD.Text))
            {
                MessageBox.Show("Vui lòng nhập Tình trạng kí hợp đồng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpDateStart.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày bắt đầu kí hợp đồng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpDateEnd.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày kết thúc kí hợp đồng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtSoHD.Text))
            {
                MessageBox.Show("Vui lòng nhập Số hợp đồng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void dtpDateStart_EditValueChanged(object sender, EventArgs e)
        {
            dtpDateSign.EditValue = dtpDateStart.EditValue;
            if (dtpDateStart.Text != "" && dtpDateEnd.Text != "")
            {
                if (dtpDateStart.DateTime > dtpDateEnd.DateTime)
                {
                    
                    MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpDateStart.Text = "";
                }
            }
        }

        private void dtpDateEnd_EditValueChanged(object sender, EventArgs e)
        {
           
            
            if (dtpDateEnd.Text != "")
            {
                if (dtpDateStart.DateTime > dtpDateEnd.DateTime)
                {
                    
                    MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpDateEnd.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmContractDetail frm = new frmContractDetail();
           
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadCb();
            //}
        }

        private void cboLoaiHD_ValueMemberChanged(object sender, EventArgs e)
        {
            //loadData();

        }

        private void btnAddLoaiHD_Click(object sender, EventArgs e)
        {
            frmContractDetail frm = new frmContractDetail();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCb();
            }
        }

        private void rdChuaky_CheckedChanged(object sender, EventArgs e)
        {
            if (rdChuaky.Checked)
            {
                dtpDateSign.EditValue = null;
            }
        }

        private void rdDaky_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDaky.Checked)
            {
                dtpDateSign.EditValue = dtpDateStart.EditValue;
            }
        }
    }
}
