using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Spreadsheet;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public frmImportExcel()
        {
            InitializeComponent();
        }

        private void frmImportExcelErrorRegister_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            cboImportMode.SelectedIndex = 0;
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    btnBrowse.Text = ofd.FileName;
            //    cboSheet.DataSource = null;
            //    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
            //    cboSheet_SelectionChangeCommitted(null, null);
            //    btnSave.Enabled = true;
            //}

            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var sw = new Stopwatch();
                sw.Start();

                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                var openTiming = sw.ElapsedMilliseconds;

                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = false,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });

                var tablenames = GetTablenames(ds.Tables);

                cboSheet.DataSource = tablenames;

                if (tablenames.Count > 0)
                    cboSheet.SelectedIndex = 0;
                btnSave.Enabled = true;
                cboSheet_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grvData.DataSource == null)
            {
                MessageBox.Show("Vui lòng chọn tên Sheet");
                return;
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount + 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        /// <summary>
        /// Insert UserID vào bảng Employee Tìm kiếm Users theo FullName
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="employee"></param>
        private int GetUserIDByFullName(string fullname)
        {
            ArrayList userArr = UsersBO.Instance.FindByAttribute("FullName", fullname);
            int id = 0;

            if (userArr.Count > 0)
            {
                UsersModel users = (UsersModel)userArr[0];
                id = users.ID;
            }

            return id;
        }


        /// <summary>
        /// Insert DepartmentID vào bảng Employee Tìm kiếm Department theo Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetDepartmentIDByName(string name)
        {
            int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"select ID from Department where Name LIKE N'%{name}%'"));
            return id;
        }


        private int GetChucVuHDIDByName(string name, string tableName)
        {
            int id = 0;
            if (tableName == "EmployeeChucVuHD")
            {
                ArrayList chucvuHDArr = EmployeeChucVuHDBO.Instance.FindByAttribute("Name", name);
                EmployeeChucVuHDModel chucVuHD = new EmployeeChucVuHDModel();
                if (chucvuHDArr.Count > 0)
                {
                    chucVuHD = (EmployeeChucVuHDModel)chucvuHDArr[0];
                    id = chucVuHD.ID;
                }
                else
                {
                    int count = TextUtils.ToInt(TextUtils.ExcuteScalar($"select COUNT(*) from {tableName}"));
                    if (count <= 0)
                    {
                        chucVuHD.Code = "CV1";
                    }
                    else
                    {
                        chucVuHD.Code = "CV" + count++;
                    }
                    chucVuHD.Name = name;

                    id = (int)EmployeeChucVuHDBO.Instance.Insert(chucVuHD);
                }
            }
            else
            {
                ArrayList chucvuArr = EmployeeChucVuBO.Instance.FindByAttribute("Name", name);
                EmployeeChucVuModel chucVu = new EmployeeChucVuModel();
                if (chucvuArr.Count > 0)
                {
                    chucVu = (EmployeeChucVuModel)chucvuArr[0];
                    id = chucVu.ID;
                }
                else
                {
                    int count = TextUtils.ToInt(TextUtils.ExcuteScalar($"select COUNT(*) from {tableName}"));
                    if (count <= 0)
                    {
                        chucVu.Code = "CV1";
                    }
                    else
                    {
                        chucVu.Code = "CV" + count++;
                    }
                    chucVu.Name = name;

                    id = (int)EmployeeChucVuBO.Instance.Insert(chucVu);
                }
            }

            return id;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            txtRate.Text = "";

            int mode = 0;
            cboImportMode.Invoke((Action)(() => { mode = cboImportMode.SelectedIndex; }));

            UpdateEmployee(mode);
            //int rowCount = grvData.RowCount;
            //for (int i = 0; i < rowCount; i++)
            //{
            //    try
            //    {
            //        int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
            //        if (stt <= 0) continue;
            //        progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
            //        txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

            //        EmployeeModel model = new EmployeeModel();
            //        //ArrayList array = EmployeeBO.Instance.FindByAttribute("Code", TextUtils.ToString(grvData.GetRowCellValue(i, "F2")));
            //        //if (array.Count > 0)
            //        //{
            //        //    model = (EmployeeModel)array[0];
            //        //}

            //        string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
            //        if (!string.IsNullOrEmpty(code))
            //        {

            //            var matchValue = SQLHelper<EmployeeModel>.FindByAttribute("Code", code).ToList();
            //            if (matchValue.Count > 0)
            //            {
            //                model = matchValue.FirstOrDefault();
            //            }

            //            string hdtv = TextUtils.ToString(grvData.GetRowCellValue(i, "F42")).Trim();
            //            string hdxd = TextUtils.ToString(grvData.GetRowCellValue(i, "F45")).Trim();
            //            string hdkxd = TextUtils.ToString(grvData.GetRowCellValue(i, "F47")).Trim();

            //            if (mode == 0)
            //            {
            //                DataTable dt1 = TextUtils.Select($"Select * from  EmployeeChucVuHD");
            //                DataTable dtChucvu = TextUtils.Select($"Select * from EmployeeChucVu");
            //                DataTable dt5 = TextUtils.Select($"Select * from EmployeeTinhTrangHonNhan");
            //                DataTable dt6 = TextUtils.Select($"Select * from EmployeeLoaiHDLD");

            //                model.Code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
            //                model.IDChamCongCu = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
            //                model.IDChamCongMoi = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
            //                model.FullName = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
            //                model.AnhCBNV = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));

            //                //Chức vụ HĐ
            //                string chucVuHD = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
            //                model.ChucVuHDID = GetChucVuHDIDByName(chucVuHD, "EmployeeChucVuHD");

            //                //Department
            //                string department = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));
            //                //DataRow[] dataRows2 = dt3.Select($"Name = '{department}'");
            //                //model.DepartmentID = dataRows2.Length > 0 ? TextUtils.ToInt(dataRows2[0]["ID"]) : 0;
            //                model.DepartmentID = GetDepartmentIDByName(department);


            //                //thêm chức vụ
            //                string chucvu = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));
            //                model.ChuVuID = GetChucVuHDIDByName(chucvu, "EmployeeChucVu");


            //                //Nhóm
            //                string groupName = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));


            //                model.DvBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));
            //                model.DiaDiemLamViec = TextUtils.ToString(grvData.GetRowCellValue(i, "F12"));
            //                model.BirthOfDate = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F13"));
            //                model.NoiSinh = TextUtils.ToString(grvData.GetRowCellValue(i, "F14"));
            //                string sex = TextUtils.ToString(grvData.GetRowCellValue(i, "F15"));
            //                if (String.Compare(sex, "Nam", true) == 0)
            //                {
            //                    model.GioiTinh = 1;
            //                }
            //                else if (String.Compare(sex, "Nữ", true) == 0)
            //                {
            //                    model.GioiTinh = 0;
            //                }
            //                else
            //                {
            //                    model.GioiTinh = 2;
            //                }
            //                model.DanToc = TextUtils.ToString(grvData.GetRowCellValue(i, "F16"));
            //                model.TonGiao = TextUtils.ToString(grvData.GetRowCellValue(i, "F17"));
            //                model.QuocTich = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));

            //                //Tình trạng hôn nhân
            //                string tinhTrangHonNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F19"));
            //                DataRow[] dataRows4 = dt5.Select($"Name = '{tinhTrangHonNhan}'");
            //                model.TinhTrangHonNhanID = dataRows4.Length > 0 ? TextUtils.ToInt(dataRows4[0]["ID"]) : 0;


            //                model.CMTND = TextUtils.ToString(grvData.GetRowCellValue(i, "F20"));
            //                model.NgayCap = TextUtils.ToDate(grvData.GetRowCellValue(i, "F21").ToString());
            //                model.NoiCap = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));

            //                model.DcThuongTru = TextUtils.ToString(grvData.GetRowCellValue(i, "F23"));
            //                model.DcTamTru = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));

            //                model.SDTCaNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F25"));
            //                model.EmailCaNhan = TextUtils.ToString(grvData.GetRowCellValue(i, "F26"));
            //                model.SDTCongTy = TextUtils.ToString(grvData.GetRowCellValue(i, "F27"));
            //                model.EmailCongTy = TextUtils.ToString(grvData.GetRowCellValue(i, "F28"));
            //                model.NguoiLienHeKhiCan = TextUtils.ToString(grvData.GetRowCellValue(i, "F29"));
            //                model.MoiQuanHe = TextUtils.ToString(grvData.GetRowCellValue(i, "F30"));
            //                model.SDTNguoiThan = TextUtils.ToString(grvData.GetRowCellValue(i, "F31"));

            //                model.Qualifications = TextUtils.ToString(grvData.GetRowCellValue(i, "F33"));


            //                //Loại HĐLĐ
            //                string LoaiHDLDName = TextUtils.ToString(grvData.GetRowCellValue(i, "F38"));
            //                DataRow[] dataRows5 = dt6.Select($"Name = '{LoaiHDLDName}'");
            //                model.LoaiHDLDID = dataRows5.Length > 0 ? TextUtils.ToInt(dataRows5[0]["ID"]) : 0;
            //                model.TinhTrangKyHD = TextUtils.ToString(grvData.GetRowCellValue(i, "F39"));

            //                model.NgayBatDauThuViec = TextUtils.ToDate(grvData.GetRowCellValue(i, "F40").ToString());
            //                model.NgayKetThucThuViec = TextUtils.ToDate(grvData.GetRowCellValue(i, "F41").ToString());
            //                model.SoHDTV = TextUtils.ToString(grvData.GetRowCellValue(i, "F42").ToString());

            //                model.NgayBatDauHDXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F43").ToString());
            //                model.NgayKetThucHDXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F44").ToString());
            //                model.SoHDXDTH = TextUtils.ToString(grvData.GetRowCellValue(i, "F45"));

            //                model.NgayHieuLucHDKXDTH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F46").ToString());
            //                model.SoHDXDTH = TextUtils.ToString(grvData.GetRowCellValue(i, "F47"));

            //                model.SoSoBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F48").ToString());
            //                //model.NguoiGiuSoBHXH = TextUtils.ToString(grvData.GetRowCellValue(i, "F49"));
            //                model.NgayBatDauBHXHCty = TextUtils.ToDate(grvData.GetRowCellValue(i, "F50").ToString());

            //                model.NgayBatDauBHXH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F51").ToString());
            //                model.NgayKetThucBHXH = TextUtils.ToDate(grvData.GetRowCellValue(i, "F52").ToString());

            //                model.MucDongBHXHHienTai = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F53"));
            //                model.LuongThuViec = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F54"));
            //                model.LuongCoBan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F55"));
            //                model.AnCa = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F56"));
            //                model.XangXe = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F57"));
            //                model.DienThoai = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F58"));
            //                model.NhaO = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F59"));
            //                model.TrangPhuc = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F60"));
            //                model.ChuyenCan = TextUtils.ToInt(grvData.GetRowCellValue(i, "F61"));
            //                model.Khac = TextUtils.ToInt(grvData.GetRowCellValue(i, "F62"));
            //                model.TongPhuCap = TextUtils.ToInt(grvData.GetRowCellValue(i, "F63"));
            //                model.TongLuong = TextUtils.ToInt(grvData.GetRowCellValue(i, "F64"));

            //                model.GiamTruBanThan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F65"));
            //                model.SoNguoiPT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F66"));
            //                model.TongTien = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F67"));

            //                model.MST = TextUtils.ToString(grvData.GetRowCellValue(i, "F68"));
            //                model.STKChuyenLuong = TextUtils.ToString(grvData.GetRowCellValue(i, "F69"));

            //                //Check
            //                model.SYLL = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F70")));
            //                model.GiayKS = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F71")));
            //                model.CMNDorCCCD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F72")));
            //                model.SoHK = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F73")));
            //                model.GiayKSK = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F74")));
            //                model.XNNS = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F75")));
            //                model.BangCap = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F76")));
            //                model.CV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F77")));
            //                model.DXV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F78")));
            //                model.CamKetTs = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F79")));
            //                model.ToTrinhTD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F80")));
            //                model.ThuMoiNhanViec = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F81")));
            //                model.QDTD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F82")));
            //                model.HDTV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F83")));
            //                model.DGTV = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F84")));
            //                model.HDLDXDTH = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F85")));
            //                model.DGChuyenHD = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F86")));
            //                model.HDLDKXDTH = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F87")));
            //                model.TinhTrangCapDongPhuc = istrue(TextUtils.ToString(grvData.GetRowCellValue(i, "F88")));


            //                //Insert UserID
            //                //model.UserID = GetUserIDByFullName(model.FullName);


            //            }
            //            else
            //            {
            //                if (!string.IsNullOrEmpty(hdtv))
            //                {
            //                    model.LoaiHDLDID = 1;
            //                }
            //                if (!string.IsNullOrEmpty(hdxd))
            //                {
            //                    model.LoaiHDLDID = 2;
            //                }
            //                if (!string.IsNullOrEmpty(hdkxd))
            //                {
            //                    model.LoaiHDLDID = 3;
            //                }
            //            }


            //            if (model.ID > 0)
            //            {
            //                EmployeeBO.Instance.Update(model);
            //            }
            //            else
            //            {
            //                model.ID = (int)EmployeeBO.Instance.Insert(model);
            //            }

            //            //Cập nhật chi tiết hợp đồng bảng EmployeeContract
            //            var exp1 = new Expression("EmployeeID", model.ID);

            //            EmployeeContractModel contract = new EmployeeContractModel();
            //            string sql = $"SELECT * FROM EmployeeContract WHERE EmployeeID = {model.ID} AND IsDelete = 0";
            //            List<EmployeeContractModel> listContract = SQLHelper<EmployeeContractModel>.SqlToList(sql);

            //            if (!string.IsNullOrEmpty(hdtv))
            //            {
            //                //string contractNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F42")).Trim();
            //                var exp2 = new Expression("ContractNumber", hdtv);
            //                //EmployeeContractModel contract = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();

            //                //EmployeeContractModel contract = new EmployeeContractModel();
            //                List<EmployeeContractModel> matchContract = listContract.Where(x => x.ContractNumber == hdtv).ToList();
            //                if (matchContract.Count > 0)
            //                {
            //                    //contract = new EmployeeContractModel();
            //                    contract = matchContract.First();
            //                }
            //                contract.EmployeeID = model.ID;
            //                contract.EmployeeLoaiHDLDID = 1;
            //                contract.DateStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F40"));
            //                contract.DateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F41"));
            //                contract.ContractNumber = hdtv;
            //                contract.StatusSign = TextUtils.ToString(grvData.GetRowCellValue(i, "F39")).ToLower().Trim() == "đã ký" ? 2 : 1;

            //                if (contract.ID > 0)
            //                {
            //                    EmployeeContractBO.Instance.Update(contract);
            //                }
            //                else
            //                {
            //                    contract.STT = listContract.Count > 0 ? listContract.Max(x => x.STT) + 1 : 1;
            //                    EmployeeContractBO.Instance.Insert(contract);
            //                }


            //            }

            //            if (!string.IsNullOrEmpty(hdxd))
            //            {
            //                //string contractNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F45")).Trim();
            //                //var exp2 = new Expression("ContractNumber", hdxd);
            //                //EmployeeContractModel contract = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();

            //                //if (contract == null)
            //                //{
            //                //    contract = new EmployeeContractModel();
            //                //}

            //                List<EmployeeContractModel> matchContract = listContract.Where(x => x.ContractNumber == hdxd).ToList();

            //                if (matchContract.Count > 0)
            //                {
            //                    //contract = new EmployeeContractModel();
            //                    contract = matchContract.First();
            //                }

            //                contract.EmployeeID = model.ID;
            //                contract.EmployeeLoaiHDLDID = 2;
            //                contract.DateStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F43"));
            //                contract.DateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F44"));
            //                contract.ContractNumber = hdxd;
            //                contract.StatusSign = TextUtils.ToString(grvData.GetRowCellValue(i, "F39")).ToLower().Trim() == "đã ký" ? 2 : 1;

            //                if (contract.ID > 0)
            //                {
            //                    EmployeeContractBO.Instance.Update(contract);
            //                }
            //                else
            //                {
            //                    contract.STT = listContract.Count > 0 ? listContract.Max(x => x.STT) + 1 : 1;
            //                    EmployeeContractBO.Instance.Insert(contract);
            //                }
            //            }

            //            if (!string.IsNullOrEmpty(hdkxd))
            //            {
            //                //string contractNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F47")).Trim();
            //                //var exp2 = new Expression("ContractNumber", hdkxd);
            //                //EmployeeContractModel contract = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();

            //                //if (contract == null)
            //                //{
            //                //    contract = new EmployeeContractModel();
            //                //}
            //                List<EmployeeContractModel> matchContract = listContract.Where(x => x.ContractNumber == hdkxd).ToList();

            //                if (matchContract.Count > 0)
            //                {
            //                    //contract = new EmployeeContractModel();
            //                    contract = matchContract.First();
            //                }

            //                contract.EmployeeID = model.ID;
            //                contract.EmployeeLoaiHDLDID = 3;
            //                contract.DateStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F46"));

            //                contract.ContractNumber = hdkxd;
            //                contract.StatusSign = TextUtils.ToString(grvData.GetRowCellValue(i, "F39")).ToLower().Trim().Contains("đã ký") ? 2 : 1;

            //                if (contract.ID > 0)
            //                {
            //                    EmployeeContractBO.Instance.Update(contract);
            //                }
            //                else
            //                {
            //                    contract.STT = listContract.Count > 0 ? listContract.Max(x => x.STT) + 1 : 1;
            //                    EmployeeContractBO.Instance.Insert(contract);
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
            //    }

            //}
        }
        bool istrue(string s)
        {
            if (s == "")
            {
                return false;
            }
            return true;
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //grvData.Columns.Clear();
            //try
            //{
            //    MyLib.ShowWaitForm("Loading data ...");
            //    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

            //    grdData.DataSource = dt;
            //    grvData.PopulateColumns();
            //    grvData.BestFitColumns();
            //    grdData.Focus();
            //}
            //catch (Exception ex)
            //{
            //    TextUtils.ShowError(ex);
            //    grdData.DataSource = null;
            //}
            //finally
            //{
            //    MyLib.CloseWaitForm();
            //}

            grdData.DataSource = null;
            try
            {
                MyLib.ShowWaitForm("Loading data ...");
                var tablename = cboSheet.SelectedItem.ToString();
                grdData.DataSource = ds.Tables[cboSheet.SelectedIndex];
                //grdData.DataMember = tablename;

            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
            finally
            {
                MyLib.CloseWaitForm();
            }

            //if (grdData.DataSource == null)
            //{
            //    try
            //    {
            //        MyLib.ShowWaitForm("Loading data ...");
            //        DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

            //        grdData.DataSource = dt;
            //        grvData.PopulateColumns();
            //        grvData.BestFitColumns();
            //        grvData.Focus();
            //        grvData.OptionsBehavior.Editable = false;
            //        grvData.OptionsBehavior.ReadOnly = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        TextUtils.ShowError(ex);
            //        grdData.DataSource = null;
            //    }
            //    finally
            //    {
            //        MyLib.CloseWaitForm();
            //    }
            //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.DialogResult = DialogResult.OK;
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        //Get tình trạng hôn nhân
        int GetTinhTrangHonNhanIDByName(string name)
        {
            int id = 0;
            EmployeeTinhTrangHonNhanModel status = new EmployeeTinhTrangHonNhanModel();
            List<EmployeeTinhTrangHonNhanModel> list = SQLHelper<EmployeeTinhTrangHonNhanModel>.FindAll();
            if (list.Count <= 0)
            {
                status.Code = $"TTHN_{list.Max(x => x.ID) + 1}";
                status.Name = name.Trim();
                id = (int)EmployeeTinhTrangHonNhanBO.Instance.Insert(status);
            }
            else
            {
                status = list.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower()).OrderByDescending(x => x.ID).FirstOrDefault();
                if (status == null)
                {
                    status = new EmployeeTinhTrangHonNhanModel();
                    status.Code = $"TTHN_{list.Max(x => x.ID) + 1}";
                    status.Name = name.Trim();
                    id = (int)EmployeeTinhTrangHonNhanBO.Instance.Insert(status);
                }
                else
                {
                    id = status.ID;
                }
            }
            return id;
        }


        //Get loại hợp đồng hiện tại
        int GetCurrentContractID(string contractNumberTV, string contractNumber12T, string contractNumber36T, string contractNumber)
        {
            int currentId = 0;
            if (!string.IsNullOrEmpty(contractNumberTV.Trim())) //Hợp đồng thủ việc
            {
                currentId = 1;
            }

            if (!string.IsNullOrEmpty(contractNumber12T.Trim())) //Hợp đồng 12t
            {
                currentId = 4;
            }

            if (!string.IsNullOrEmpty(contractNumber36T.Trim())) //Hợp đồng 36t
            {
                currentId = 2;
            }

            if (!string.IsNullOrEmpty(contractNumber.Trim())) //Hợp đồng không xác định thời hạn
            {
                currentId = 3;
            }

            return currentId;
        }

        void UpdateEmployee(int mode)
        {
            int i = 0;
            int totalRowSkip = 0;
            DataTable dataSource = (DataTable)grdData.DataSource;

            string sqlUpdateUser = "";
            foreach (DataRow row in dataSource.Rows)
            {
                i++;
                int stt = TextUtils.ToInt(row["F1"]);
                if (stt <= 0)
                {
                    totalRowSkip++;
                    continue;
                }

                progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{dataSource.Rows.Count - 1}"; }));

                EmployeeModel employee = new EmployeeModel();
                string code = TextUtils.ToString(row["F2"]).Trim();
                if (!string.IsNullOrEmpty(code))
                {
                    employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", code).FirstOrDefault();
                }

                employee = employee == null ? new EmployeeModel() : employee;

                if (mode == 0)//Thông tin chung
                {
                    string hdtv = TextUtils.ToString(row["F42"]).Trim();
                    string hd12t = TextUtils.ToString(row["F45"]).Trim();
                    string hd36t = TextUtils.ToString(row["F48"]).Trim();
                    string hd = TextUtils.ToString(row["F50"]).Trim();

                    //Thông tin chung
                    employee.CodeOld = employee.Code;
                    //employee.Code = code;
                    employee.IDChamCongMoi = TextUtils.ToString(row["F3"]).Trim();
                    employee.FullName = TextUtils.ToString(row["F4"]).Trim();
                    employee.AnhCBNV = TextUtils.ToString(row["F5"]).Trim();
                    employee.ChucVuHDID = GetChucVuHDIDByName(TextUtils.ToString(row["F6"]).Trim(), "EmployeeChucVuHD");
                    employee.DepartmentID = GetDepartmentIDByName(TextUtils.ToString(row["F7"]).Trim());
                    employee.ChuVuID = GetChucVuHDIDByName(TextUtils.ToString(row["F8"]).Trim(), "EmployeeChucVu");
                    employee.DvBHXH = TextUtils.ToString(row["F10"]).Trim();
                    employee.DiaDiemLamViec = TextUtils.ToString(row["F11"]).Trim();
                    employee.BirthOfDate = TextUtils.ToDate4(row["F12"]);
                    employee.NoiSinh = TextUtils.ToString(row["F13"]);
                    employee.GioiTinh = TextUtils.ToString(row["F14"]).ToLower().Contains("nữ") ? 0 : (TextUtils.ToString(row["F14"]).ToLower().Contains("nam") ? 1 : 2);
                    employee.DanToc = TextUtils.ToString(row["F15"]).Trim();
                    employee.TonGiao = TextUtils.ToString(row["F16"]).Trim();
                    employee.QuocTich = TextUtils.ToString(row["F17"]).Trim();
                    employee.TinhTrangHonNhanID = GetTinhTrangHonNhanIDByName(TextUtils.ToString(row["F18"]).Trim());

                    //Chứng minh nhân dân
                    employee.SoCMTND = TextUtils.ToString(row["F19"]).Trim();
                    employee.NgayCap = TextUtils.ToDate4(row["F20"]);
                    employee.NoiCap = TextUtils.ToString(row["F21"]).Trim();

                    //Địa chỉ
                    employee.DcThuongTru = TextUtils.ToString(row["F22"]).Trim();
                    employee.DcTamTru = TextUtils.ToString(row["F23"]).Trim();

                    //Thông tin liên hệ
                    employee.SDTCaNhan = TextUtils.ToString(row["F24"]).Trim();
                    employee.EmailCaNhan = TextUtils.ToString(row["F25"]).Trim();
                    employee.SDTCongTy = TextUtils.ToString(row["F26"]).Trim();
                    employee.EmailCongTy = TextUtils.ToString(row["F27"]).Trim();
                    employee.NguoiLienHeKhiCan = TextUtils.ToString(row["F28"]).Trim();
                    employee.MoiQuanHe = TextUtils.ToString(row["F29"]).Trim();
                    employee.SDTNguoiThan = TextUtils.ToString(row["F30"]).Trim();
                    employee.SDTNguoiThan = TextUtils.ToString(row["F30"]).Trim();

                    //Thông tin trình độ
                    employee.Qualifications = TextUtils.ToString(row["F32"]).Trim();

                    //Thông tin hợp đồng
                    employee.LoaiHDLDID = GetCurrentContractID(hdtv, hd12t, hd36t, hd);
                    employee.TinhTrangKyHD = TextUtils.ToString(row["F38"]).Trim();

                    employee.StartWorking = TextUtils.ToDate4(row["F39"]);
                    employee.EndWorking = TextUtils.ToDate4(row["F51"]);
                    employee.Status = !employee.EndWorking.HasValue ? 0 : 1;
                    //UsersModel user = SQLHelper<UsersModel>.FindByID(employee.userID);
                    //if (user != null)
                    //{
                    //    user.Status = employee.Status;
                    //}
                    sqlUpdateUser += $"UPDATE dbo.Users SET Status = {employee.Status}, UpdatedBy = '{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID = {employee.UserID}\n";

                    //Thông tin BHXH
                    employee.SoSoBHXH = TextUtils.ToString(row["F52"]).Trim();
                    EmployeeModel nguoigiuso = SQLHelper<EmployeeModel>.FindByAttribute("Code", TextUtils.ToString(row["F53"]).Trim()).FirstOrDefault();
                    employee.NguoiGiuSoBHXH = nguoigiuso == null ? 0 : nguoigiuso.ID;
                    employee.NgayBatDauBHXHCty = TextUtils.ToDate4(row["F54"]);
                    employee.NgayBatDauBHXH = TextUtils.ToDate4(row["F55"]);
                    employee.NgayKetThucBHXH = TextUtils.ToDate4(row["F56"]);
                    employee.MucDongBHXHHienTai = TextUtils.ToDecimal(row["F57"]);

                    //Thông tin lương
                    employee.LuongThuViec = TextUtils.ToDecimal(row["F58"]);
                    employee.LuongCoBan = TextUtils.ToDecimal(row["F59"]);
                    employee.AnCa = TextUtils.ToDecimal(row["F60"]);
                    employee.XangXe = TextUtils.ToDecimal(row["F61"]);
                    employee.DienThoai = TextUtils.ToDecimal(row["F62"]);
                    employee.NhaO = TextUtils.ToDecimal(row["F63"]);
                    employee.TrangPhuc = TextUtils.ToDecimal(row["F64"]);
                    employee.ChuyenCan = TextUtils.ToDecimal(row["F65"]);
                    employee.Khac = TextUtils.ToDecimal(row["F66"]);
                    employee.TongPhuCap = TextUtils.ToDecimal(row["F67"]);
                    employee.TongLuong = TextUtils.ToDecimal(row["F68"]);
                    employee.GiamTruBanThan = TextUtils.ToDecimal(row["F69"]);
                    employee.SoNguoiPT = TextUtils.ToInt(row["F70"]);
                    employee.TongTien = TextUtils.ToInt(row["F71"]);
                    employee.MST = TextUtils.ToString(row["F72"]).Trim();
                    employee.STKChuyenLuong = TextUtils.ToString(row["F73"]).Trim();

                    //Checklist hồ sơ
                    employee.SYLL = !TextUtils.ToString(row["F74"]).Trim().ToLower().Contains("");
                    employee.GiayKS = !TextUtils.ToString(row["F75"]).Trim().ToLower().Contains("");
                    employee.CMNDorCCCD = !TextUtils.ToString(row["F76"]).ToLower().Trim().Contains("");
                    employee.SoHK = !TextUtils.ToString(row["F77"]).Trim().ToLower().Contains("");
                    employee.GiayXacNhanCuTru = !TextUtils.ToString(row["F78"]).Trim().ToLower().Contains(""); //Giấy xác nhận cư trú
                    employee.GiayKSK = !TextUtils.ToString(row["F79"]).Trim().ToLower().Contains("");
                    employee.XNNS = !TextUtils.ToString(row["F80"]).Trim().ToLower().Contains("cheked");
                    employee.BangCap = !TextUtils.ToString(row["F81"]).Trim().ToLower().Contains("");
                    employee.CV = !TextUtils.ToString(row["F82"]).Trim().ToLower().Contains("");
                    employee.DXV = !TextUtils.ToString(row["F84"]).Trim().ToLower().Contains("");
                    employee.CamKetTs = !TextUtils.ToString(row["F84"]).Trim().ToLower().Contains("");
                    employee.ToTrinhTD = !TextUtils.ToString(row["F85"]).Trim().ToLower().Contains("");
                    employee.ThuMoiNhanViec = !TextUtils.ToString(row["F86"]).ToLower().Trim().Contains("");
                    employee.QDTD = !TextUtils.ToString(row["F87"]).Trim().ToLower().Contains("");
                    employee.HDTV = !TextUtils.ToString(row["F88"]).Trim().ToLower().Contains("");
                    employee.DGTV = !TextUtils.ToString(row["F89"]).Trim().ToLower().Contains("");
                    employee.HDLDXDTHYear = !TextUtils.ToString(row["F90"]).Trim().ToLower().Contains("");
                    employee.DGChuyenHDYear = !TextUtils.ToString(row["F91"]).Trim().ToLower().Contains("");
                    employee.HDLDXDTH = !TextUtils.ToString(row["F92"]).Trim().ToLower().Contains("");
                    employee.DGChuyenHD = !TextUtils.ToString(row["F93"]).Trim().ToLower().Contains("");
                    employee.HDLDXDTH = !TextUtils.ToString(row["F94"]).Trim().ToLower().Contains("");
                    employee.TinhTrangCapDongPhuc = !TextUtils.ToString(row["F95"]).Trim().ToLower().Contains("");
                    employee.Code = TextUtils.ToString(row["F96"]).Trim(); //Mã nv mới
                }
                else if (mode == 1) //update thông tin hợp đồng
                {
                    string hdtv = TextUtils.ToString(row["F42"]).Trim();
                    string hd12t = TextUtils.ToString(row["F45"]).Trim();
                    string hd36t = TextUtils.ToString(row["F48"]).Trim();
                    string hd = TextUtils.ToString(row["F50"]).Trim();

                    employee.LoaiHDLDID = GetCurrentContractID(hdtv, hd12t, hd36t, hd);
                    employee.TinhTrangKyHD = TextUtils.ToString(row["F38"]).Trim();
                }
                else if (mode == 2) //Update zalo id
                {
                    employee.UserZaloID = TextUtils.ToString(row["F3"]).Trim();
                }
                else if (mode == 3) //Update mã nv
                {
                    employee.CodeOld = employee.Code;
                    employee.Code = TextUtils.ToString(row["F96"]).Trim(); //Mã nv mới
                }

                if (employee.ID > 0)
                {
                    EmployeeBO.Instance.Update(employee);
                }
                else
                {
                    employee.ID = (int)EmployeeBO.Instance.Insert(employee);
                }


                if (mode == 1)
                {
                    string hdtv = TextUtils.ToString(row["F42"]).Trim();
                    string hd12t = TextUtils.ToString(row["F45"]).Trim();
                    string hd36t = TextUtils.ToString(row["F48"]).Trim();
                    string hd = TextUtils.ToString(row["F50"]).Trim();

                    List<EmployeeContractModel> listContract = new List<EmployeeContractModel>()
                    {
                        new EmployeeContractModel(){EmployeeID = employee.ID, EmployeeLoaiHDLDID = GetCurrentContractID(hdtv, "", "", ""), DateStart = TextUtils.ToDate4(row["F40"]),DateEnd = TextUtils.ToDate4(row["F41"]),ContractNumber = hdtv},
                        new EmployeeContractModel(){EmployeeID = employee.ID, EmployeeLoaiHDLDID = GetCurrentContractID("", hd12t, "", ""), DateStart = TextUtils.ToDate4(row["F43"]),DateEnd = TextUtils.ToDate4(row["F44"]),ContractNumber = hd12t},
                        new EmployeeContractModel(){EmployeeID = employee.ID, EmployeeLoaiHDLDID = GetCurrentContractID("", "", hd36t, ""), DateStart = TextUtils.ToDate4(row["F46"]),DateEnd = TextUtils.ToDate4(row["F47"]),ContractNumber = hd36t},
                        new EmployeeContractModel(){EmployeeID = employee.ID, EmployeeLoaiHDLDID = GetCurrentContractID("", "", "", hd), DateStart = TextUtils.ToDate4(row["F49"]),DateEnd = null,ContractNumber = hd},
                    };
                    UpdateEmployeeContract(listContract, employee.ID);

                    if (!string.IsNullOrWhiteSpace(sqlUpdateUser.Trim())) TextUtils.ExcuteSQL(sqlUpdateUser);

                }

            }
        }

        void UpdateEmployeeContract(List<EmployeeContractModel> list, int employeeId)
        {
            var exp1 = new Expression("EmployeeID", employeeId);
            var exp2 = new Expression("IsDelete", 0);
            List<EmployeeContractModel> listContract = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2));
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.ContractNumber))
                {
                    continue;
                }
                EmployeeContractModel contract = listContract.Where(x => x.ContractNumber.Trim() == item.ContractNumber.Trim()).FirstOrDefault();
                contract = contract == null ? new EmployeeContractModel() : contract;
                contract.EmployeeID = item.EmployeeID;
                contract.EmployeeLoaiHDLDID = item.EmployeeLoaiHDLDID;
                contract.DateStart = item.DateStart;
                contract.DateEnd = item.DateEnd;
                contract.ContractNumber = item.ContractNumber.Trim();
                if (contract.ID > 0)
                {
                    EmployeeContractBO.Instance.Update(contract);
                }
                else
                {
                    contract.STT = listContract.Count > 0 ? listContract.Max(x => x.STT) + 1 : 1;
                    EmployeeContractBO.Instance.Insert(contract);
                }
            }

        }
    }
}
