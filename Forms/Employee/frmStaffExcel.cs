using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmStaffExcel : _Forms
    {
        public frmStaffExcel()
        {
            InitializeComponent();
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
                cboSheet.DataSource = null;
                cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                cboSheet_SelectionChangeCommitted(null, null);
            }
        }

        private void frmImportCheckForceExcel_Load(object sender, EventArgs e)
        {
            //ArrayList
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                grdData.DataSource = dt;
                grvData.PopulateColumns();
                grvData.BestFitColumns();
                grdData.Focus();


            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        DateTime start;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount - 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            txtRate.Text = "";

            int rowCount = grvData.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    if (i < 3) continue;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

                    EmployeeModel model = new EmployeeModel();
                    ArrayList array = EmployeeBO.Instance.FindByAttribute("Code", TextUtils.ToString(grvData.GetRowCellValue(i, "F2")));
                    if (array.Count > 0)
                    {
                        model = (EmployeeModel)array[0];
                    }
                    if (grvData.GetRowCellValue(i, "F2").ToString() != "")
                    {
                        DataTable dt1 = TextUtils.Select($"Select * from  EmployeeChucVuHD");
                        //DataTable dt2 = TextUtils.Select($"Select * from UserTeam");
                        DataTable dt3 = TextUtils.Select($"Select * from Department");
                        DataTable dtChucvu = TextUtils.Select($"Select * from EmployeeChucVu");
                        DataTable dt5 = TextUtils.Select($"Select * from EmployeeTinhTrangHonNhan");
                        DataTable dt6 = TextUtils.Select($"Select * from EmployeeLoaiHDLD");

                        model.Code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.IDChamCongCu = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                        model.IDChamCongMoi = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                        model.FullName = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));

                        //Chức vụ HĐ
                        string chucVuHD = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                        if (chucVuHD == "")
                        {
                            model.ChucVuHDID = 0;
                        }
                        //ArrayList cvhd = ChucVuHDBO.Instance.FindByAttribute("Name", chucVuHD);
                        DataRow[] dataRows1 = dt1.Select($"Name ='{chucVuHD.Trim()}'");
                        // model.ChucVuHDID = dataRows1.Length > 0 ? TextUtils.ToInt(dataRows1[0]["ID"]) : 0;
                        if (dataRows1.Length > 0)
                        {
                            model.ChucVuHDID = TextUtils.ToInt(dataRows1[0]["ID"]);
                        }
                        else
                        {
                            EmployeeChucVuHDModel modelchucvuHD = new EmployeeChucVuHDModel();
                            modelchucvuHD.Name = chucVuHD;
                            model.ChucVuHDID = (int)EmployeeChucVuHDBO.Instance.Insert(modelchucvuHD);
                        }


                        //Team
                        //string team = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));
                        //DataRow[] dataRows6 = dt2.Select($"Name = '{team}'");
                        ////model.ChucVuHDID = dataRows6.Length > 0 ? TextUtils.ToInt(dataRows6[0]["ID"]) : 0;
                        //if (dataRows6.Length > 0)
                        //{
                        //    model.TeamID = TextUtils.ToInt(dataRows6[0]["ID"]);
                        //}
                        //else
                        //{
                        //    UserTea modelchucvu = new ChucVuModel();
                        //    modelchucvu.Name = chucVuHD;
                        //    model.ID = (int)ChucVuBO.Instance.Insert(modelchucvu);
                        //}



                        //Department
                        string department = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                        DataRow[] dataRows2 = dt3.Select($"Name = '{department}'");
                        model.DepartmentID = dataRows2.Length > 0 ? TextUtils.ToInt(dataRows2[0]["ID"]) : 0;


                        //thêm chức vụ
                        string chucvu = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));
                        if (chucvu == "")
                        {
                            model.ChuVuID = 0;
                        }
                        DataRow[] dataRows3 = dtChucvu.Select($"Name ='{chucvu.Trim()}'");
                        // model.ChuVuID = dataRows3.Length > 0 ? TextUtils.ToInt(dataRows3[0]["ID"]) : 0;
                        if (dataRows3.Length > 0)
                        {
                            model.ChuVuID = TextUtils.ToInt(dataRows3[0]["ID"]);
                        }
                        else
                        {
                            EmployeeChucVuModel modelchucvu = new EmployeeChucVuModel();
                            modelchucvu.Name = chucvu;
                            model.ChuVuID = (int)EmployeeChucVuBO.Instance.Insert(modelchucvu);
                        }


                        model.DvBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));
                        model.DiaDiemLamViec = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));
                        model.BirthOfDate = TextUtils.ToDate(grvData.GetRowCellValue(i, "F12").ToString());
                        model.NoiSinh = TextUtils.ToString(grvData.GetRowCellValue(i, "F16"));
                        if (String.Compare(TextUtils.ToString(grvData.GetRowCellValue(i, "F17")), "Nam", true) == 0)
                        {
                            model.Sex = 1;
                        }
                        else if (String.Compare(TextUtils.ToString(grvData.GetRowCellValue(i, "F17")), "Nữ", true) == 0)
                        {
                            model.Sex = 0;
                        }
                        else
                        {
                            model.Sex = 2;
                        }
                        model.DanToc = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));
                        model.TonGiao = TextUtils.ToString(grvData.GetRowCellValue(i, "F19"));
                        model.QuocTich = TextUtils.ToString(grvData.GetRowCellValue(i, "F20"));

                        //Tình trạng hôn nhân
                        string tinhTrangHonNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F21"));
                        DataRow[] dataRows4 = dt5.Select($"Name = '{tinhTrangHonNhan}'");
                        model.TinhTrangHonNhanID = dataRows4.Length > 0 ? TextUtils.ToInt(dataRows4[0]["ID"]) : 0;


                        model.CMTND = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));
                        model.NgayCap = TextUtils.ToDate(grvData.GetRowCellValue(i, "F23").ToString());
                        model.NoiCap = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));
                        model.DcThuongTru = TextUtils.ToString(grvData.GetRowCellValue(i, "F25"));
                        model.DcTamTru = TextUtils.ToString(grvData.GetRowCellValue(i, "F26"));
                        model.SDTCaNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F27"));
                        //model.EmailCaNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F28"));
                        model.SDTCongTy = TextUtils.ToString(grvData.GetRowCellValue(i, "F28"));
                        model.EmailCongTy = TextUtils.ToString(grvData.GetRowCellValue(i, "F29"));
                        model.NguoiLienHeKhiCan = TextUtils.ToString(grvData.GetRowCellValue(i, "F30"));
                        model.MoiQuanHe = TextUtils.ToString(grvData.GetRowCellValue(i, "F31"));
                        model.SDTNguoiThan = TextUtils.ToString(grvData.GetRowCellValue(i, "F32"));
                        model.Qualifications = TextUtils.ToString(grvData.GetRowCellValue(i, "F34"));
                        model.TinhTrangKyHD = TextUtils.ToString(grvData.GetRowCellValue(i, "F40"));

                        //Loại HĐLĐ
                        string LoaiHDLDName = TextUtils.ToString(grvData.GetRowCellValue(i, "F39"));
                        DataRow[] dataRows5 = dt6.Select($"Name = '{LoaiHDLDName}'");
                        model.LoaiHDLDID = dataRows5.Length > 0 ? TextUtils.ToInt(dataRows5[0]["ID"]) : 0;


                        model.NgayBatDauThuViec = TextUtils.ToDate(grvData.GetRowCellValue(i, "F41").ToString());
                        model.NgayKetThucThuViec = TextUtils.ToDate(grvData.GetRowCellValue(i, "F42").ToString());
                        model.SoHDTV = TextUtils.ToString(grvData.GetRowCellValue(i, "F43").ToString());
                        model.NgayBatDauHDXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F44").ToString());
                        model.NgayKetThucHDXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F45").ToString());
                        model.SoHDXDTH = TextUtils.ToString(grvData.GetRowCellValue(i, "F46"));
                        model.NgayHieuLucHDKXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F47").ToString());
                        model.SoHDXDTH = TextUtils.ToString(grvData.GetRowCellValue(i, "F48"));
                        model.SoSoBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F49").ToString());
                        //model.NguoiGiuSoBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F50"));
                        model.NgayBatDauBHXHCty = TextUtils.ToDate(grvData.GetRowCellValue(i, "F51").ToString());
                        model.NgayBatDauBHXH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F52").ToString());
                        model.NgayKetThucBHXH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F53").ToString());
                        model.MucDongBHXHHienTai = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F54"));
                        model.LuongThuViec = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F55"));
                        model.LuongCoBan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F56"));
                        model.AnCa = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F57"));
                        model.XangXe = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F58"));
                        model.DienThoai = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F59"));
                        model.NhaO = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F60"));
                        model.TrangPhuc = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F61"));
                        //model.ChuyenCan = TextUtils.ToInt(grvData.GetRowCellValue(i, "F61"));
                        //model.Khac = TextUtils.ToInt(grvData.GetRowCellValue(i, "F62"));
                        //model.TongPhuCap = TextUtils.ToInt(grvData.GetRowCellValue(i, "F63"));
                        model.SoNguoiPT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F63"));
                        model.TongTien = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F64"));
                        model.GiamTruBanThan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F62"));
                        model.STKChuyenLuong = TextUtils.ToString(grvData.GetRowCellValue(i, "F66"));
                        model.MST = TextUtils.ToString(grvData.GetRowCellValue(i, "F65"));
                        //Check
                        //model.SYLL = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.GiayKS = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F68"))) ? false : true;
                        //model.CMNDorCCCD = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F69"))) ? false : true;
                        //model.SoHK = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F70"))) ? false : true;
                        //model.GiayKSK = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.XNNS = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.BangCap = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.CV = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.DXV = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.CamKetTs = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.ToTrinhTD = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.ThuMoiNhanViec = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.QDTD = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.HDTV = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.DGTV = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.HDLDXDTH = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.DGChuyenHD = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.HDLDKXDTH = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;
                        //model.TinhTrangCapDongPhuc = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F67"))) ? false : true;

                        model.SYLL = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F67")));
                        model.GiayKS = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F68")));
                        model.CMNDorCCCD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F69")));
                        model.SoHK = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F70")));
                        model.GiayKSK = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F71")));
                        model.XNNS = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F72")));
                        model.BangCap = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F73")));
                        model.CV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F74")));
                        model.DXV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F75")));
                        model.CamKetTs = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F76")));
                        model.ToTrinhTD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F77")));
                        model.ThuMoiNhanViec = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F78")));
                        model.QDTD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F79")));
                        model.HDTV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F80")));
                        model.DGTV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F81")));
                        model.HDLDXDTH = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F82")));
                        model.DGChuyenHD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F83")));
                        model.HDLDKXDTH = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F84")));
                        model.TinhTrangCapDongPhuc = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F85")));
                        if (model.ID > 0)
                        {

                            EmployeeBO.Instance.Update(model);

                        }
                        else
                            EmployeeBO.Instance.Insert(model);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
                }

            }
        }

        /// <summary>
        /// Hàm check giá trị checklist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool istrue(string s)
        {
            if (s == "")
            {
                return false;
            }
            return true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            enableControl(true);
        }

        private void frmStaffExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
