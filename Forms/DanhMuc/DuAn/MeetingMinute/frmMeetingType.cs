using BMS.Utils;
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
    public partial class frmMeetingType : _Forms
    {
        public frmMeetingType()
        {
            InitializeComponent();
        }

        private void frmMeetingType_Load(object sender, EventArgs e)
        {
            LoadGroupID();
            LoadData();
        }
        private void LoadData()
        {
            List<MeetingTypeModel> lst = SQLHelper<MeetingTypeModel>.FindByAttribute("IsDelete", 0);
            grdData.DataSource = lst;
        }
        private void LoadGroupID()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 1, GroupName = "Nội bộ"},
                new {ID = 2, GroupName = "Khách hàng"}
            };
            cboGroup.DataSource = lst;
            cboGroup.ValueMember = "ID";
            cboGroup.DisplayMember = "GroupName";
        }
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmMeetingTypeDetails frm = new frmMeetingTypeDetails();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string typeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName));
            frmMeetingTypeDetails frm = new frmMeetingTypeDetails();
            MeetingTypeModel model = SQLHelper<MeetingTypeModel>.FindByID(id);
            
            if(model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy Loại cuộc họp {typeName}", "Thông báo");
                return;
            }

            frm.meetingType = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string typeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName));
            MeetingTypeModel model = SQLHelper<MeetingTypeModel>.FindByID(id);
            if (model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của Loại cuộc họp [{typeName}]","Thông báo");
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa Loại cuộc họp [{typeName}]","Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                model.IsDelete = true;
                SQLHelper<MeetingTypeModel>.Update(model);
                LoadData();
            }
        }
        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null,null);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int focusRowHandle = grvData.FocusedRowHandle;
            if (e.RowHandle == focusRowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }
    }
}
