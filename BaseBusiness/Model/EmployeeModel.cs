using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? UserID { get; set; } // int, null
        public string Code { get; set; } // nvarchar(550), null
        public string FullName { get; set; } // nvarchar(550), null
        public DateTime? BirthOfDate { get; set; } // datetime, null
        public int? Sex { get; set; } // int, null
        public string Qualifications { get; set; } // nvarchar(550), null
        public string BankAccount { get; set; } // nvarchar(550), null
        public string BHYT { get; set; } // nvarchar(550), null
        public string MST { get; set; } // nvarchar(550), null
        public string BHXH { get; set; } // nvarchar(550), null
        public string CMTND { get; set; } // nvarchar(550), null
        public string JobDescription { get; set; } // nvarchar(550), null
        public bool? NhanVienTuVan { get; set; } // bit, null
        public string Telephone { get; set; } // nvarchar(550), null
        public string HandPhone { get; set; } // nvarchar(550), null
        public string HomeAddress { get; set; } // nvarchar(550), null
        public string Resident { get; set; } // nvarchar(550), null
        public string PostalCode { get; set; } // nvarchar(550), null
        public int? DepartmentID { get; set; } // int, null
        public int? Status { get; set; } // int, null
        public string Communication { get; set; } // nvarchar(550), null
        public DateTime? PassExpireDate { get; set; } // datetime, null
        public bool? IsCashier { get; set; } // bit, null
        public int? CashierNo { get; set; } // int, null
        public string EmailCom { get; set; } // nvarchar(550), null
        public string Email { get; set; } // nvarchar(550), null
        public DateTime? StartWorking { get; set; } // datetime, null
        public int? UserGroupID { get; set; } // int, null
        public int? UserGroupSXID { get; set; } // int, null
        public int? MainViewID { get; set; } // int, null
        public string Position { get; set; } // nvarchar(550), null
        public bool? IsSetupFunction { get; set; } // bit, null
        public string ImagePath { get; set; } // nvarchar(550), null
        public string CreatedBy { get; set; } // nvarchar(550), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(550), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? isAdminSale { get; set; } // int, null
        public int? RoleID { get; set; } // int, null
        public int? TeamID { get; set; } // int, null
        public int? Leader { get; set; } // int, null
        public string IDChamCongCu { get; set; } // nvarchar(550), null
        public string IDChamCongMoi { get; set; } // nvarchar(550), null
        public string AnhCBNV { get; set; } // ntext, null
        public int? ChucVuHDID { get; set; } // int, null
        public int? ChuVuID { get; set; } // int, null
        public string DvBHXH { get; set; } // nvarchar(550), null
        public string DiaDiemLamViec { get; set; } // nvarchar(550), null
        public string NoiSinh { get; set; } // nvarchar(550), null
        public int? GioiTinh { get; set; } // int, null
        public string DanToc { get; set; } // nvarchar(550), null
        public string TonGiao { get; set; } // nvarchar(550), null
        public string QuocTich { get; set; } // nvarchar(550), null
        public int? TinhTrangHonNhanID { get; set; } // int, null
        public string SoCMTND { get; set; } // nvarchar(250), null
        public DateTime? NgayCap { get; set; } // datetime, null
        public string NoiCap { get; set; } // nvarchar(150), null
        public string DcThuongTru { get; set; } // nvarchar(550), null
        public string DcTamTru { get; set; } // nvarchar(550), null
        public string SDTCaNhan { get; set; } // nvarchar(50), null
        public string EmailCaNhan { get; set; } // nvarchar(550), null
        public string SDTCongTy { get; set; } // nvarchar(50), null
        public string EmailCongTy { get; set; } // nvarchar(550), null
        public string NguoiLienHeKhiCan { get; set; } // nvarchar(150), null
        public string MoiQuanHe { get; set; } // nvarchar(150), null
        public string SDTNguoiThan { get; set; } // nvarchar(50), null
        public int? LoaiHDLDID { get; set; } // int, null
        /// <summary>
        /// 1: 
        /// </summary>
        public string TinhTrangKyHD { get; set; } // nvarchar(150), null
        public DateTime? NgayBatDauHD { get; set; } // datetime, null
        public DateTime? NgayKetThucHD { get; set; } // datetime, null
        public string SoHD { get; set; } // nvarchar(150), null
        public string SoSoBHXH { get; set; } // varchar(150), null
        public int? NguoiGiuSoBHXH { get; set; } // int, null
        public DateTime? NgayBatDauBHXHCty { get; set; } // datetime, null
        public decimal? MucDongBHXHHienTai { get; set; } // decimal(18,2), null
        public decimal? LuongThuViec { get; set; } // decimal(18,2), null
        public decimal? LuongCoBan { get; set; } // decimal(18,2), null
        public decimal? AnCa { get; set; } // decimal(18,2), null
        public decimal? XangXe { get; set; } // decimal(18,2), null
        public decimal? DienThoai { get; set; } // decimal(18,2), null
        public decimal? NhaO { get; set; } // decimal(18,2), null
        public decimal? TrangPhuc { get; set; } // decimal(18,2), null
        public decimal? ChuyenCan { get; set; } // decimal(18,2), null
        public decimal? Khac { get; set; } // decimal(18,2), null
        public decimal? TongPhuCap { get; set; } // decimal(18,2), null
        public decimal? TongLuong { get; set; } // decimal(18,2), null
        public decimal? GiamTruBanThan { get; set; } // decimal(18,2), null
        public int? SoNguoiPT { get; set; } // int, null
        public decimal? TongTien { get; set; } // decimal(18,2), null
        public string STKChuyenLuong { get; set; } // varchar(50), null
        public bool? SYLL { get; set; } // bit, null
        public bool? GiayKS { get; set; } // bit, null
        public bool? CMNDorCCCD { get; set; } // bit, null
        public bool? SoHK { get; set; } // bit, null
        public bool? XNNS { get; set; } // bit, null
        public bool? BangCap { get; set; } // bit, null
        public bool? CV { get; set; } // bit, null
        public bool? DXV { get; set; } // bit, null
        public bool? CamKetTs { get; set; } // bit, null
        public bool? ToTrinhTD { get; set; } // bit, null
        public bool? ThuMoiNhanViec { get; set; } // bit, null
        public bool? QDTD { get; set; } // bit, null
        public bool? HDTV { get; set; } // bit, null
        public bool? DGTV { get; set; } // bit, null
        public bool? HDLDXDTH { get; set; } // bit, null
        public bool? DGChuyenHD { get; set; } // bit, null
        public bool? HDLDKXDTH { get; set; } // bit, null
        public bool? TinhTrangCapDongPhuc { get; set; } // bit, null
        public bool? GiayKSK { get; set; } // bit, null
        public DateTime? NgayBatDauThuViec { get; set; } // datetime, null
        public DateTime? NgayKetThucThuViec { get; set; } // datetime, null
        public string SoHDTV { get; set; } // nvarchar(100), null
        public DateTime? NgayBatDauHDXDTH { get; set; } // datetime, null
        public DateTime? NgayKetThucHDXDTH { get; set; } // datetime, null
        public string SoHDXDTH { get; set; } // nvarchar(100), null
        public DateTime? NgayHieuLucHDKXDTH { get; set; } // datetime, null
        public string SoHDKXDTH { get; set; } // nvarchar(100), null
        public DateTime? NgayBatDauBHXH { get; set; } // datetime, null
        public DateTime? NgayKetThucBHXH { get; set; } // datetime, null
        public string SoNhaDcThuongTru { get; set; } // nvarchar(150), null
        public string DuongDcThuongTru { get; set; } // nvarchar(150), null
        public string PhuongDcThuongTru { get; set; } // nvarchar(150), null
        public string QuanDcThuongTru { get; set; } // nvarchar(150), null
        public string TinhDcThuongTru { get; set; } // nvarchar(150), null
        public string SoNhaDcTamTru { get; set; } // nvarchar(150), null
        public string DuongDcTamTru { get; set; } // nvarchar(150), null
        public string PhuongDcTamTru { get; set; } // nvarchar(150), null
        public string QuanDcTamTru { get; set; } // nvarchar(150), null
        public string TinhDcTamTru { get; set; } // nvarchar(150), null
        public bool? HDLDXDTHYear { get; set; } // bit, null
        public bool? DGChuyenHDYear { get; set; } // bit, null
        public DateTime? EndWorking { get; set; } // datetime, null
        public string ReasonDeleted { get; set; } // nvarchar(max), null
        public string CodeOld { get; set; } // nvarchar(550), null
        public int? ProjectTypeID { get; set; } // int, null
        public int? EmployeeTeamID { get; set; } // int, null
        public bool? GiayXacNhanCuTru { get; set; } // bit, null
        public string UserZaloID { get; set; } // nvarchar(250), null
        public int? TaxCompanyID { get; set; } // int, null
    }
    public enum EmployeeModel_Enum{
        ID,
        UserID,
        Code,
        FullName,
        BirthOfDate,
        Sex,
        Qualifications,
        BankAccount,
        BHYT,
        MST,
        BHXH,
        CMTND,
        JobDescription,
        NhanVienTuVan,
        Telephone,
        HandPhone,
        HomeAddress,
        Resident,
        PostalCode,
        DepartmentID,
        Status,
        Communication,
        PassExpireDate,
        IsCashier,
        CashierNo,
        EmailCom,
        Email,
        StartWorking,
        UserGroupID,
        UserGroupSXID,
        MainViewID,
        Position,
        IsSetupFunction,
        ImagePath,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        isAdminSale,
        RoleID,
        TeamID,
        Leader,
        IDChamCongCu,
        IDChamCongMoi,
        AnhCBNV,
        ChucVuHDID,
        ChuVuID,
        DvBHXH,
        DiaDiemLamViec,
        NoiSinh,
        GioiTinh,
        DanToc,
        TonGiao,
        QuocTich,
        TinhTrangHonNhanID,
        SoCMTND,
        NgayCap,
        NoiCap,
        DcThuongTru,
        DcTamTru,
        SDTCaNhan,
        EmailCaNhan,
        SDTCongTy,
        EmailCongTy,
        NguoiLienHeKhiCan,
        MoiQuanHe,
        SDTNguoiThan,
        LoaiHDLDID,
        TinhTrangKyHD,
        NgayBatDauHD,
        NgayKetThucHD,
        SoHD,
        SoSoBHXH,
        NguoiGiuSoBHXH,
        NgayBatDauBHXHCty,
        MucDongBHXHHienTai,
        LuongThuViec,
        LuongCoBan,
        AnCa,
        XangXe,
        DienThoai,
        NhaO,
        TrangPhuc,
        ChuyenCan,
        Khac,
        TongPhuCap,
        TongLuong,
        GiamTruBanThan,
        SoNguoiPT,
        TongTien,
        STKChuyenLuong,
        SYLL,
        GiayKS,
        CMNDorCCCD,
        SoHK,
        XNNS,
        BangCap,
        CV,
        DXV,
        CamKetTs,
        ToTrinhTD,
        ThuMoiNhanViec,
        QDTD,
        HDTV,
        DGTV,
        HDLDXDTH,
        DGChuyenHD,
        HDLDKXDTH,
        TinhTrangCapDongPhuc,
        GiayKSK,
        NgayBatDauThuViec,
        NgayKetThucThuViec,
        SoHDTV,
        NgayBatDauHDXDTH,
        NgayKetThucHDXDTH,
        SoHDXDTH,
        NgayHieuLucHDKXDTH,
        SoHDKXDTH,
        NgayBatDauBHXH,
        NgayKetThucBHXH,
        SoNhaDcThuongTru,
        DuongDcThuongTru,
        PhuongDcThuongTru,
        QuanDcThuongTru,
        TinhDcThuongTru,
        SoNhaDcTamTru,
        DuongDcTamTru,
        PhuongDcTamTru,
        QuanDcTamTru,
        TinhDcTamTru,
        HDLDXDTHYear,
        DGChuyenHDYear,
        EndWorking,
        ReasonDeleted,
        CodeOld,
        ProjectTypeID,
        EmployeeTeamID,
        GiayXacNhanCuTru,
        UserZaloID,
        TaxCompanyID,
        }
}
