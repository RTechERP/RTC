using DevExpress.XtraEditors;
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
    public partial class frmCourseType : _Forms
    {
        public frmCourseType()
        {
            InitializeComponent();
        }

      
        private void frmCourseType_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            List<CourseTypeModel> lst = SQLHelper<CourseTypeModel>.FindByAttribute("IsDeleted", 0);
            grdCourse.DataSource = lst;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grdData.FocusedRowHandle;
            frmCourseTypeDetail frm = new frmCourseTypeDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grdData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int rowHandle = grdData.FocusedRowHandle;
            int id = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colID));
            string typeName = TextUtils.ToString(grdData.GetFocusedRowCellValue(colCourseTypeName));
            frmCourseTypeDetail frm = new frmCourseTypeDetail();
            CourseTypeModel model = SQLHelper<CourseTypeModel>.FindByID(id);

            if(model.ID <= 0)
            {
                //MessageBox.Show($"Không tìm thấy loại khóa học {typeName}", "Thông báo");
                return;
            }
            frm.courseType = model;
            //frm.id = id;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            //    grdData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colID));
            string typeName = TextUtils.ToString(grdData.GetFocusedRowCellValue(colCourseTypeName));
            CourseTypeModel model = SQLHelper<CourseTypeModel>.FindByID(id);
            if(model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của Loại khóa học {typeName}", "Thông báo");
            }
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa Loại khóa học {typeName}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                model.IsDeleted = true;
                SQLHelper<CourseTypeModel>.Update(model);
                LoadData();
            }
        }

        private void grdData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = grdData.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnSua_ItemClick(null, null);
        }

        private void grdData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int focusRowHandle = grdData.FocusedRowHandle;
            if(e.RowHandle == focusRowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        private void grdCourse_Click(object sender, EventArgs e)
        {

        }

        private void frmCourseType_Load_1(object sender, EventArgs e)
        {

        }
    }
}