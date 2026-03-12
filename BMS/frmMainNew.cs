using Forms;
using Forms.Employee;
using Forms.Personal;
using Forms.Sale;
using Forms.Sale.EmployeeSaleManager;
using Forms.TB;
using Forms.Technical;
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
    public partial class frmMainNew : _Forms
    {
        public frmMainNew()
        {
            InitializeComponent();
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            accordionControl1.CollapseAll();
        }

        #region function

        #endregion

        #region ADMIN
        //Quyền hạn
        private void btnPermissionManager_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPermissionManager(), this);
        }

        //phân quyền
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmUserGroup(), this);
        }
        #endregion

        #region CÁ NHÂN
        //duyệt công
        private void accordionControlElement29_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmApprovedTP(false), this);
        }

        //duyệt yêu cầu công việc
        private void accordionControlElement30_Click(object sender, EventArgs e)
        {

            frmJobRequirement frm = new frmJobRequirement(true);
            frm.Tag = "JobRequirementApproveTBP";
            frm.Text = " TBP DUYỆT " + frm.Text;
            TextUtils.OpenChildForm(frm, this);
        }

        //duyệt đề nghị thanh toán
        private void accordionControlElement31_Click(object sender, EventArgs e)
        {
            frmPaymentOrder frm = new frmPaymentOrder(1);
            frm.Tag = "PaymentOrderApproveTBP";
            frm.Text = " TBP DUYỆT " + frm.Text;
            TextUtils.OpenChildForm(frm, this);
        }

        //duyệt hạng mục công việc
        private void accordionControlElement32_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmApprovedProjectItem(), this);
        }

        //duyệt partlist
        private void accordionControlElement33_Click(object sender, EventArgs e)
        {
            frmProjectPartList_New frm = new frmProjectPartList_New(true);
            frm.Tag = "ProjectPartList_NewApproveTBP";
            TextUtils.OpenChildForm(frm, this);
        }

        //duyệt vpp
        private void accordionControlElement34_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyRequests frm = new frmOfficeSupplyRequests(true);
            frm.Tag = "OfficeSupplyRequestApproveTBP";
            frm.Text = "TBP DUYỆT " + frm.Text;
            TextUtils.OpenChildForm(frm, this);
        }

        //tổng hợp công: đăng ký nghỉ
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonDayOff(), this);
        }

        //tổng hợp công: đi muộn - về sớm
        private void accordionControlElement305_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonEarlyLate(), this);
        }

        //tổng hợp công: wfh
        private void accordionControlElement306_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonWFH(), this);
        }

        //tổng hợp công: làm thêm
        private void accordionControlElement307_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonOT(), this);
        }

        //tổng hợp công: Công tác
        private void accordionControlElement308_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonBussiness(), this);
        }

        //tổng hợp công: quyên chấm công
        private void accordionControlElement309_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonNoFingerprint(), this);
        }

        //tổng hợp công: làm đêm
        private void accordionControlElement310_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonNightShift(), this);
        }

        //tổng hợp cá nhân
        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonalSynthetic(), this);
        }

        //tài sản cá nhân
        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonalProperty(), this);
        }

        //lịch sử mượn: demo
        private void accordionControlElement35_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPersonalHistory(1), this);
        }

        //lịch sử mượn: sale
        private void accordionControlElement36_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryBorrowSale(1, false), this);
        }

        //lịch sử mượn: đồ phòng sạch
        private void accordionControlElement37_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeProtectiveGear(), this);
        }

        //báo cáo công việc
        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmDailyReportTechnical(), this);
        }

        //công việc cá nhân
        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectItemInWeb(), this);
        }

        //kế hoạch công việc cá nhân
        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmWorkPlan(), this);
        }

        //tổng hợp vi phạm
        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSummaryPerson(), this);
        }

        //tồn kho theo sản phẩm
        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmInventoryByProduct(), this);
        }

        //tổng hợp dự án tham gia
        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectJoin(), this);
        }

        //đánh giá kpi
        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmKPIEvaluationEmployee(), this);
        }

        //đánh giá năng lực
        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            frmExamMultipleChoice frm = new frmExamMultipleChoice();
            frm.ShowDialog();
        }
        #endregion

        #region DANH MỤC

        //khách hàng
        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //nhà cung cấp
        private void accordionControlElement21_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSupplierSale(), this);
        }

        //vị trí thiết bị
        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProductLocation(), this);
        }

        //hãng
        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmFirm(), this);
        }

        //đơn vị tính
        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmUnitCount(), this);
        }

        //leader dự án
        private void accordionControlElement25_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmLeaderProject(), this);
        }

        //kho
        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmWarehouse(), this);
        }

        //vật tư kho sale
        private void accordionControlElement27_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProductSale(), this);
        }

        //vật tư kho demo
        private void accordionControlElement28_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProductRTC(), this);
        }
        #endregion

        #region CHỨC NĂNG CHUNG
        //tồn kho theo sản phẩm
        private void accordionControlElement39_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmInventoryByProduct(), this);
        }

        //phòng họp
        private void accordionControlElement40_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmMeetingRoom(), this);
        }

        //Yêu cầu công việc
        private void accordionControlElement41_Click(object sender, EventArgs e)
        {
            frmJobRequirement frm = new frmJobRequirement(false);
            frm.Tag = "JobRequirementApprove";
            TextUtils.OpenChildForm(frm, this);
        }

        //theo dõi đóng dấu
        private void accordionControlElement42_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTrackingMarks(), this);
        }

        //quản lý tip tricks: đăng ký
        private void accordionControlElement45_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmRegisterIdea(), this);
        }

        //quản lý tip tricks: tổng hợp
        private void accordionControlElement46_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmRegisterIdealSumarize(), this);
        }

        //khóa học: quản lý khóa học
        private void accordionControlElement47_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmCourse(), this);
        }

        //khóa học: đề thi
        private void accordionControlElement48_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmExam(), this);
        }

        //khóa học: kết quả thi
        private void accordionControlElement49_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmCourseExamPractice(), this);
        }

        //khóa học: kết quả thi quý
        private void accordionControlElement50_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmExamResult(), this);
        }

        //khóa học: tổng hợp khóa học
        private void accordionControlElement51_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSummaryOfExamResults(), this);
        }

        //khóa học: loại khóa học
        private void accordionControlElement52_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmCourseType(), this);
        }
        #endregion

        #region DỰ ÁN

        //dự án
        private void accordionControlElement54_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProject(), this);
        }


        //tiến độ công việc
        private void accordionControlElement55_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmGanttChartProjectItemGrid(), this);
        }

        //timeline công việc
        private void accordionControlElement56_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTimeLineWork(), this);
        }

        //khảo sát dự án
        private void accordionControlElement57_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectSurvey(), this);
        }

        //biên bản cuộc họp
        private void accordionControlElement58_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmMeetingMinutes(), this);
        }

        //hạng mục công việc chậm tiến độ
        private void accordionControlElement59_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectItemLate(), this);
        }

        //tổng hợp dự án cơ khí-agv
        private void accordionControlElement60_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectSummary(), this);
        }

        //timeline hạng mục công việc
        private void accordionControlElement61_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHangMucCongViecNew(), this);
        }

        //báo cáo vật tư phát sinh
        private void accordionControlElement62_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPartlistProblem(), this);
        }

        //tổng hợp dự án phòng ban
        private void accordionControlElement63_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectNew(), this);
        }

        //báo cáo thống kế: báo cáo dự án
        private void accordionControlElement67_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectReport(), this);
        }

        //báo cáo thống kế: báo cáo dự án đã po
        private void accordionControlElement68_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectReportPO(), this);
        }

        //báo cáo thống kế: báo cáo kiểu dự án
        private void accordionControlElement69_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectTypeReport(), this);
        }

        //báo cáo thống kế: báo cáo dự án chậm tiến độ
        private void accordionControlElement70_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectIsBehindSchedule(), this);
        }

        //báo cáo thống kế: báo cáo dự án team
        private void accordionControlElement71_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectReportTeam(), this);
        }

        //lịch sử giá
        private void accordionControlElement65_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryPricePartlist(), this);
        }

        //cài đặt: kiểu dự án
        private void accordionControlElement72_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectType(), this);
        }

        //cài đặt: lĩnh vực dự án
        private void accordionControlElement73_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectField(), this);
        }

        //cài đặt: leader kiểu dự án
        private void accordionControlElement74_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmLeaderProjectType(), this);
        }

        //cài đặt: loại biên bản cuộc họp
        private void accordionControlElement75_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmMeetingType(), this);
        }

        #endregion

        #region NHÂN SỰ

        //quản lý nv: phòng ban
        private void accordionControlElement95_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmDepartment(), this);
        }

        //quản lý nv: team
        private void accordionControlElement96_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmteamtrees(), this);
        }

        //quản lý nv: chức vụ
        private void accordionControlElement97_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPosition(), this);
        }

        //quản lý nv: nhân viên
        private void accordionControlElement98_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployee(), this);
        }

        //quản lý nv: hợp đồng
        private void accordionControlElement99_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmContract(), this);
        }

        //quản lý nv: quá trình công tác
        private void accordionControlElement100_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmWorkingProcess(), this);
        }

        //quản lý chấm công - lương: ngày nghỉ
        private void accordionControlElement101_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHoliday(), this);
        }

        //quản lý chấm công - lương: cơm ca
        private void accordionControlElement102_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmFoodOrder(), this);
        }

        //quản lý chấm công - lương: đăng ký nghỉ
        private void accordionControlElement103_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmDayOff(), this);
        }

        //quản lý chấm công - lương: đi muộn về sớm
        private void accordionControlElement104_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEarlyLate(), this);
        }

        //quản lý chấm công - lương: làm thêm
        private void accordionControlElement105_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmOvertime(), this);
        }

        //quản lý chấm công - lương: công tác
        private void accordionControlElement106_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeBussiness(), this);
        }

        //quản lý chấm công - lương: làm đêm
        private void L_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeNightShift(), this);
        }

        //quản lý chấm công - lương: wfh
        private void accordionControlElement107_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmRegisterWFH(), this);
        }

        //quản lý chấm công - lương: quên vân tay
        private void accordionControlElement108_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmNoFingerprint(), this);
        }

        //quản lý chấm công - lương: vân tay
        private void accordionControlElement109_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeAttendance(), this);
        }

        //quản lý chấm công - lương: thu hộ phòng ban
        private void accordionControlElement110_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmThuHoPhongBan(), this);
        }

        //quản lý chấm công - lương: lỗi 5s
        private void accordionControlElement111_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmError(), this);
        }

        //quản lý chấm công - lương: ngoại khóa
        private void accordionControlElement112_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeCurricular(), this);
        }

        //quản lý chấm công - lương: bảng chấm công
        private void accordionControlElement113_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeChamCongMaster(), this);
        }

        //quản lý chấm công - lương: bảng lương
        private void accordionControlElement114_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPayroll(), this);
        }

        //quản lý chấm công - lương: tổng hợp
        private void accordionControlElement115_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeSynthetic(), this);
        }

        //quản lý đăng ký xe: danh sách xe
        private void accordionControlElement116_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmVehicleManagement(), this);
        }

        //quản lý đăng ký xe: danh sách đặt xe
        private void accordionControlElement117_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmVehicleBookingManagement(), this);
        }

        //quản lý văn bản
        private void accordionControlElement88_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new FrmDocumnet(), this);
        }

        //quản lý vpp: danh sách vpp
        private void accordionControlElement118_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmOfficeSupply(), this);
        }

        //quản lý vpp: đơn vị tính
        private void accordionControlElement119_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmOfficeSupplyUnit(), this);
        }

        //quản lý vpp: đăng ký vpp
        private void accordionControlElement120_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyRequests frm = new frmOfficeSupplyRequests(false);
            frm.Tag = "OfficeSupplyRequests";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý vpp: tổng hợp vpp
        private void accordionControlElement121_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmOfficeSupplyRequestSummary(), this);
        }

        //báo cáo công việc
        private void accordionControlElement90_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmDailyReportHR(), this);
        }

        //tài sản: quản lý tài sản
        private void accordionControlElement122_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmAsset(), this);
        }

        //tài sản: nguồn gốc tài sản
        private void accordionControlElement123_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSourceAsset(), this);
        }

        //tài sản: loại tài sản
        private void accordionControlElement124_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTypeAsset(), this);
        }

        //tài sản: điều chuyển
        private void accordionControlElement125_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTranferAssetMaster(), this);
        }

        //tài sản: cấp phát
        private void accordionControlElement126_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTSAssetAllocation(), this);
        }

        //tài sản: thu hồi
        private void accordionControlElement127_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTSAssetRecovery(), this);
        }

        //quản lý film
        private void accordionControlElement92_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmFilmManagement(), this);
        }

        //tủ đồ bảo hộ: ds sản phẩm
        private void accordionControlElement128_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProductRTCProtectiveGear(), this);
        }

        //tủ đồ bảo hộ: nhập kho
        private void accordionControlElement129_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillImportTechnicalProtectiveGear(), this);
        }

        //tủ đồ bảo hộ: xuất kho
        private void accordionControlElement130_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillExportTechnicalProtectiveGear(), this);
        }

        //tủ đồ bảo hộ: tồn kho
        private void accordionControlElement131_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmInventoryDemoProtectiveGear(), this);
        }

        //tủ đồ bảo hộ: tủ đồ bảo hộ & phòng sách
        private void accordionControlElement132_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryProductRTCProtectiveGearNew(), this);
        }

        //tủ đồ bảo hộ: lịch sử mượn
        private void accordionControlElement133_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryProductRTCProtectiveGear(), this);
        }

        //tủ đồ bảo hộ: vị trí
        private void accordionControlElement134_Click(object sender, EventArgs e)
        {
            frmProductLocationTech frm = new frmProductLocationTech(5);
            frm.Tag = "BH";
            TextUtils.OpenChildForm(frm, this);
        }

        //danh sách bốc thăm
        private void accordionControlElement94_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeLuckyNumber(), this);
        }
        #endregion

        #region P.KỸ THUẬT

        //tồn kho demo
        private void accordionControlElement135_Click(object sender, EventArgs e)
        {
            frmInventoryDemo frm = new frmInventoryDemo(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //lịch sử nhập xuất
        private void accordionControlElement136_Click(object sender, EventArgs e)
        {
            frmProductReportNew frm = new frmProductReportNew(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //lịch sử mượn
        private void accordionControlElement137_Click(object sender, EventArgs e)
        {
            frmProductHistory frm = new frmProductHistory(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //đơn vị tính
        private void accordionControlElement138_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmUnitCountKT(), this);
        }

        //phiếu nhập
        private void accordionControlElement139_Click(object sender, EventArgs e)
        {
            frmBillImportTechnical frm = new frmBillImportTechnical(1);
            frm.Tag = "HN";

            TextUtils.OpenChildForm(frm, this);
        }

        //phiếu xuất
        private void accordionControlElement140_Click(object sender, EventArgs e)
        {
            frmBillExportTechnical_New frm = new frmBillExportTechnical_New(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //vị trí
        private void accordionControlElement141_Click(object sender, EventArgs e)
        {
            frmProductLocationTech frm = new frmProductLocationTech(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //ds sp không dùng
        private void accordionControlElement142_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProductExportAndBorrow(), this);
        }

        //quản lý qr code
        private void accordionControlElement143_Click(object sender, EventArgs e)
        {
            frmAddQRCode frm = new frmAddQRCode(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //mượn trả thiết bị qr code
        private void accordionControlElement144_Click(object sender, EventArgs e)
        {
            frmMain_QR frm = new frmMain_QR(1);
            frm.Tag = "HN";
            frm.ShowDialog();
        }

        //tra cứu serial number
        private void accordionControlElement145_Click(object sender, EventArgs e)
        {
            frmSearchProductTechSerialNumber frm = new frmSearchProductTechSerialNumber(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //báo cáo mượn
        private void accordionControlElement146_Click(object sender, EventArgs e)
        {
            frmBorrowReport frm = new frmBorrowReport(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //kế hoạch cv tổng hợp
        private void accordionControlElement147_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSummarizeWork(), this);
        }

        //quản lý lỗi: danh sách lỗi
        private void accordionControlElement152_Click(object sender, EventArgs e)
        {
            frmKPIError frm = new frmKPIError();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: danh sách nv lỗi
        private void accordionControlElement153_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployee frm = new frmKPIErrorEmployee();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp vị phạm lỗi
        private void accordionControlElement154_Click(object sender, EventArgs e)
        {
            frmSummaryKPIErrorEmployee frm = new frmSummaryKPIErrorEmployee();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp vị phạm lỗi theo tháng
        private void accordionControlElement155_Click(object sender, EventArgs e)
        {
            frmSummaryKPIErrorEmployeeMonth frm = new frmSummaryKPIErrorEmployeeMonth();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp nv nhiều lỗi
        private void accordionControlElement156_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployeeSummaryMax frm = new frmKPIErrorEmployeeSummaryMax();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá KPI: cá nhân
        private void accordionControlElement157_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationEmployee frm = new frmKPIEvaluationEmployee();
            frm.deName = "KT";
            frm.departmentID = 2; // vtn update 07 / 02 / 2025
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá KPI: admin
        private void accordionControlElement158_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - ADMIN KỸ THUẬT";
            frm.Tag = "ADMINKT";
            frm.typeID = 4;
            frm.departmentID = 2;
            //btnKPIEvaluationFactorScoringAdmin.SearchTags = TextUtils.ToString(frm.Tag);
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá KPI: trưởng / phó bộ phận
        private void accordionControlElement159_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - TRƯỞNG / PHÓ BỘ PHẬN";
            frm.Tag = "TBP";
            frm.typeID = 2;
            frm.departmentID = 2;
            //btnKPIEvaluationFactorScoring.SearchTags = TextUtils.ToString(frm.Tag);
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá KPI: ban giám đốc
        private void accordionControlElement160_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - BAN GIÁM ĐỐC";
            frm.Tag = "BGĐ";
            frm.typeID = 3;
            frm.departmentID = 2;
            TextUtils.OpenChildForm(frm, this);
        }

        //tổng hợp đánh giá KPI
        private void accordionControlElement150_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSummaryKPIEmployeePoint(), this);
        }

        //cài đặt: tiêu chuẩn
        private void accordionControlElement161_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmKPICriteria(), this);
        }

        //cài đặt: nội dung đánh giá
        private void accordionControlElement162_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactors frm = new frmKPIEvaluationFactors();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: rule đánh giá
        private void accordionControlElement163_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationRule frm = new frmKPIEvaluationRule();
            frm.departmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: vị trí nv
        private void accordionControlElement164_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmKPIPositionEmployee(2), this);
        }

        //cài đặt: mã rule
        private void accordionControlElement165_Click(object sender, EventArgs e)
        {
            frmKPIEvaluation frm = new frmKPIEvaluation();
            frm.deparmentID = 2;
            frm.deName = "KT";
            frm.Tag = "KT";
            TextUtils.OpenChildForm(frm, this);
        }
        #endregion

        #region P.KINH DOANH

        //tồn kho
        private void accordionControlElement166_Click(object sender, EventArgs e)
        {
            frmInventory frm = new frmInventory();
            frm.VP = "HN";
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //phiếu nhập
        private void accordionControlElement167_Click(object sender, EventArgs e)
        {
            frmBillImport frm = new frmBillImport();
            frm.WarehouseCode = "HN";
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //phiếu xuất
        private void accordionControlElement168_Click(object sender, EventArgs e)
        {
            frmBillExport frm = new frmBillExport();
            frm.WarehouseCode = "HN";
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //ls nhập xuất
        private void accordionControlElement169_Click(object sender, EventArgs e)
        {
            frmHistoryImportExport frm = new frmHistoryImportExport("HN");
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //ls mượn
        private void accordionControlElement170_Click(object sender, EventArgs e)
        {
            frmHistoryBorrowSale frm = new frmHistoryBorrowSale(1, true);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //báo cáo nhập xuất
        private void accordionControlElement171_Click(object sender, EventArgs e)
        {
            frmReportImportExport frm = new frmReportImportExport("HN");
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //ds sp theo dự án
        private void accordionControlElement172_Click(object sender, EventArgs e)
        {
            frmExportToExcelListProductProject frm = new frmExportToExcelListProductProject("HN");
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }


        //tra cứu serial number
        private void accordionControlElement173_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmSearchProductSerialNumber(), this);
        }


        //tính giá: tính giá thương mại
        private void accordionControlElement178_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTradePrice(), this);
        }

        //tính giá: báo giá thương mại
        private void accordionControlElement179_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmQuotationSale(), this);
        }

        //tính giá: tính giá máy
        private void accordionControlElement180_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectMachinePrice(), this);
        }

        //kpi: nv sale
        private void accordionControlElement181_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmployeeSaleManager(), this);
        }

        //kpi: kpi
        private void accordionControlElement182_Click(object sender, EventArgs e)
        {
            frmHistoryProductivityIndex frm = new frmHistoryProductivityIndex(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //kpi: mục tiêu
        private void accordionControlElement183_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmGoal(), this);
        }

        //kpi: tổng hợp báo cáo
        private void accordionControlElement184_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBonusCoefficient(), this);
        }

        //kpi: daily report sale
        private void accordionControlElement185_Click(object sender, EventArgs e)
        {
            frmDailyReportSale frm = new frmDailyReportSale(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //kpi: daily report sale admin
        private void accordionControlElement186_Click(object sender, EventArgs e)
        {
            frmDailyReportSaleAdmin frm = new frmDailyReportSaleAdmin(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //po khách hàng: ds po
        private void accordionControlElement187_Click(object sender, EventArgs e)
        {
            frmPOKH_New frm = new frmPOKH_New(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //po khách hàng: báo giá kh
        private void accordionControlElement188_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmQuotationKH(), this);
        }

        //po khách hàng: xuất po kh chi tiết
        private void accordionControlElement189_Click(object sender, EventArgs e)
        {
            frmPOKH_KPI_05102022 frm = new frmPOKH_KPI_05102022(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //po khách hàng: lịch sử po
        private void accordionControlElement190_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPOKHHistory(), this);
        }

        //vision base: kế hoạch tuần
        private void accordionControlElement191_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPlanWeek(), this);
        }

        //vision base: follow dự án
        private void accordionControlElement192_Click(object sender, EventArgs e)
        {
            frmFollowProjectBase frm = new frmFollowProjectBase(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }

        //vision base: khách hàng
        private void accordionControlElement193_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer(1);
            frm.Tag = "HN";
            TextUtils.OpenChildForm(frm, this);
        }
        #endregion

        #region P.MUA HÀNG

        // yc báo giá
        private void accordionControlElement194_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectPartlistPriceRequestOld(), this);
        }

        // yc báo giá theo loại
        private void accordionControlElement195_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectPartlistPriceRequestNew(0), this);
        }

        // yc mua hàng
        private void accordionControlElement196_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectPartlistPurchaseRequest(), this);
        }

        // po nhà cung cấp
        private void accordionControlElement197_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmPONCCNew(), this);
        }

        // tiền tệ
        private void accordionControlElement198_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmCurrency(), this);
        }

        //phân công công việc
        private void accordionControlElement199_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmProjectTypeAssign(), this);
        }

        //điều khoản thanh toán
        private void accordionControlElement200_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmRulePay(), this);
        }

        //thông tin nv mua
        private void accordionControlElement201_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmEmloyeePurchase(), this);
        }

        //ds hàng giữ
        private void accordionControlElement202_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmInventoryProject(), this);
        }
        #endregion

        #region P.KẾ TOÁN

        //phiếu xuất
        private void accordionControlElement203_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillExportAccountant(), this);
        }

        //trạng thái sp
        private void accordionControlElement204_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryExportAcountant(), this);
        }

        //ls hủy - nhận chứng từ
        private void accordionControlElement205_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmHistoryIsApprovedBillLog(), this);
        }

        //báo cáo tồn kho
        private void accordionControlElement206_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmInventoryByDate(), this);
        }

        //quản lý hợp đồng: loại hợp đồng
        private void accordionControlElement312_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmAccoutingContractTypeMaster(), this);
        }

        //quản lý hợp đồng: hợp đồng
        private void accordionControlElement313_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmAccountingContract(), this);
        }

        //đề nghị thanh toán
        private void accordionControlElement208_Click(object sender, EventArgs e)
        {
            frmPaymentOrder frm = new frmPaymentOrder(0);
            frm.Tag = "PaymentOrder";
            TextUtils.OpenChildForm(frm, this);
        }

        //hồ sơ chứng từ: loại chứng từ
        private void accordionControlElement211_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmDocumentImportExport(), this);
        }

        //hồ sơ chứng từ: phiếu nhập sale
        private void accordionControlElement212_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillDocumentImportType(), this);
        }

        //hồ sơ chứng từ: phiếu xuất sale
        private void accordionControlElement213_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillDocumentExportDetail(), this);
        }

        //hồ sơ chứng từ: phiếu nhập demo
        private void accordionControlElement214_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillDocumentImportTechnicalDetail(), this);
        }

        //hồ sơ chứng từ: phiếu xuất demo
        private void accordionControlElement215_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmBillDocumentExportTechnicalDetail(), this);
        }

        //hồ sơ chứng từ: hóa đơn
        private void accordionControlElement216_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmAccountingBill(), this);
        }

        //nv thuế: phòng ban
        private void accordionControlElement217_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTaxDepartment(), this);
        }

        //nv thuế: công ty
        private void accordionControlElement218_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTaxCompany(), this);
        }

        //nv thuế: chức vụ
        private void accordionControlElement219_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTaxEmployeePosition(), this);
        }

        //nv thuế: nhân viên
        private void accordionControlElement220_Click(object sender, EventArgs e)
        {
            TextUtils.OpenChildForm(new frmTaxEmployee(), this);
        }
        #endregion

        #region P.AGV

        //quản lý lỗi: ds lỗi
        private void accordionControlElement228_Click(object sender, EventArgs e)
        {
            frmKPIError frm = new frmKPIError();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: ds nv lỗi
        private void accordionControlElement229_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployee frm = new frmKPIErrorEmployee();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp vi phạm lỗi
        private void accordionControlElement230_Click(object sender, EventArgs e)
        {
            frmSummaryKPIErrorEmployee frm = new frmSummaryKPIErrorEmployee();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp vi phạm lỗi theo tháng
        private void accordionControlElement231_Click(object sender, EventArgs e)
        {
            frmSummaryKPIErrorEmployeeMonth frm = new frmSummaryKPIErrorEmployeeMonth();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //quản lý lỗi: tổng hợp nv nhiều lỗi
        private void accordionControlElement232_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployeeSummaryMax frm = new frmKPIErrorEmployeeSummaryMax();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá kpi: cá nhân
        private void accordionControlElement234_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationEmployee frm = new frmKPIEvaluationEmployee();
            frm.deName = "AGV";
            frm.Tag = "AGV";
            frm.departmentID = 9; // vtn update 07 / 02 / 2025
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá kpi: admin
        private void accordionControlElement235_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - ADMIN AGV";
            frm.Tag = "ADMINAGV";
            frm.typeID = 4;
            frm.departmentID = 9;
            //btnKPIEvaluationFactorScoringAdmin.SearchTags = TextUtils.ToString(frm.Tag);
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá kpi: trưởng / phó bộ phận
        private void accordionControlElement236_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - TRƯỞNG / PHÓ BỘ PHẬN AGV";
            frm.Tag = "TBPAGV";
            frm.typeID = 2;
            frm.departmentID = 9;
            //btnKPIEvaluationFactorScoring.SearchTags = TextUtils.ToString(frm.Tag);
            TextUtils.OpenChildForm(frm, this);
        }

        //đánh giá kpi: ban giám đốc
        private void accordionControlElement237_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactorScoring frm = new frmKPIEvaluationFactorScoring();
            frm.Text = "ĐÁNH GIÁ KPI - BAN GIÁM ĐỐC";
            frm.Tag = "BGĐAGV";
            frm.typeID = 3;
            frm.departmentID = 9;
            TextUtils.OpenChildForm(frm, this);
        }

        //tổng hợp đánh giá kpi
        private void accordionControlElement238_Click(object sender, EventArgs e)
        {
            frmSummaryKPIEmployeePoint frm = new frmSummaryKPIEmployeePoint();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }
        
        //cài đặt: tiêu chuẩn
        private void accordionControlElement240_Click(object sender, EventArgs e)
        {
            frmKPICriteria frm = new frmKPICriteria();
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: nội dung đánh giá
        private void accordionControlElement241_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationFactors frm = new frmKPIEvaluationFactors();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: rule đánh giá
        private void accordionControlElement242_Click(object sender, EventArgs e)
        {
            frmKPIEvaluationRule frm = new frmKPIEvaluationRule();
            frm.departmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: vị trí nv
        private void accordionControlElement243_Click(object sender, EventArgs e)
        {
            frmKPIPositionEmployee frm = new frmKPIPositionEmployee(9);
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }

        //cài đặt: mã rule
        private void accordionControlElement244_Click(object sender, EventArgs e)
        {
            frmKPIEvaluation frm = new frmKPIEvaluation();
            frm.deparmentID = 9;
            frm.deName = "AGV";
            frm.Tag = "AGV";
            TextUtils.OpenChildForm(frm, this);
        }
        #endregion

    }
}
