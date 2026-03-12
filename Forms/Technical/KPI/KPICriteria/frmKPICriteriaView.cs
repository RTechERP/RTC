using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class frmKPICriteriaView : _Forms
    {
        public int criteriaYear = 0;
        public int criteriaQuarter = 0;
        public frmKPICriteriaView()
        {
            InitializeComponent();
        }

        private void frmKPICriteriaView_Load(object sender, EventArgs e)
        {
            txtQuarter.Value = criteriaQuarter > 0 ? criteriaQuarter : 1;
            txtYear.Value = criteriaYear > 2024 ? criteriaYear : 2024; 
            LoadEvaluatePoint();
            LoadProjectType();
            LoadData();
        }
        private void LoadEvaluatePoint()
        {
            List<object> lst = new List<object>()
            {
                new {
                        point1 = ">= 100%",
                        point2 = "90% <= B+ < 100%",
                        point3 = "80% <= B < 90%",
                        point4 = "70% <= B- < 80%",
                        point5 = "60% <= C < 70%",
                        point6 = "< 60% (Không có thưởng)"
                    }
            };
            gridControl1.DataSource = lst;
        }

        private void LoadProjectType()
        {
            List<object> lst = new List<object>()
            {
                new {ProjectType = "Dự án đơn giản",
                    PLC = "Dự án có PLC điều khiển ít cụm và IO, và 1, 2 servo phát xung chạy điểm hay biến tần, vision …",
                    Vision = "Dự án check có không, đọc code đơn giản dùng smart camera (VPM hoặc SC của Hik), code reader (DL code)",
                    SoftWare = "- Nghiệp vụ đơn giản, database ít bảng, không link nhiều, chỉ bao gồm thêm sửa xóa danh mục, có thêm phần log data \n- Có thể truyền thông đơn giản với các thiết bị ngoại vi"},

                new {ProjectType = "Dự án phức tạp",
                    PLC = "Dự án lớn như SDV, LG. có nhiều cụm, IO, remote IO, truyền thông PLC CIM, Robot + vision align, có tiêu chuẩn thiết kế an toàn riêng theo ISO của nhà máy như safety, đi dây …",
                    Vision = "Dự án PC base nhiều camera, alignment (auto calib) nhiều camera với robot, cần xử lý và truyền thông data lớn, xây dựng giao diện, check các lỗi khó, tạo được procedure riêng trên Halcon",
                    SoftWare = "- Nghiệp vụ phức tạp, database lớn, dữ liệu lưu trữ rất nhiều, liên tục \n- Hệ thống báo cáo nhiều, phức tạp, lấy lên từ nhiều bảng \n- Làm việc vời nhiều thiết bị truyền thông, việc truyền nhận diễn ra liên tục, cần xử lý bất đồng bộ nhiều"}
            };

            grdProject.DataSource = lst;
        }

        private void LoadData()
        {
            Expression ex1 = new Expression("KPICriteriaYear", criteriaYear);
            Expression ex2 = new Expression("KPICriteriaQuater", criteriaQuarter);
            Expression ex3 = new Expression(KPICriteriaModel_Enum.IsDeleted, 0);
            List<KPICriteriaModel> lstCol = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2).And(ex3));
            gridBand2.Columns.Clear();
            foreach (KPICriteriaModel item in lstCol)
            {
                BandedGridColumn col = new BandedGridColumn();
                col.Caption = item.CriteriaCode + " : " + item.CriteriaName;
                col.Visible = true;
                col.FieldName = item.CriteriaCode;
                col.Width = 300;
                col.ColumnEdit = repositoryItemMemoEdit2;
                gridBand2.Columns.Add(col);
            }
            DataTable data = SQLHelper<KPICriteriaModel>.LoadDataFromSP("spGetKpiCriteriaPivot",
                                                                        new string[] { "@Year" , "@Quater" },
                                                                        new object[] { criteriaYear, criteriaQuarter });
            grdData.DataSource = data;
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
