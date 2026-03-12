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
    public partial class frmMeetingTypeDetails : _Forms
    {
        public MeetingTypeModel meetingType = new MeetingTypeModel();
        public frmMeetingTypeDetails()
        {
            InitializeComponent();
        }

        private void frmMeetingTypeDetails_Load(object sender, EventArgs e)
        {
            LoadDetail();

        }
        private void LoadDetail()
        {
            txtTypeCode.Text = meetingType.TypeCode;
            txtTypeName.Text = meetingType.TypeName;
            txtTypeContent.Text = meetingType.TypeContent;
            cboGroupType.SelectedIndex = TextUtils.ToInt(meetingType.GroupID);
        }
        private void Reset()
        {
            meetingType = new MeetingTypeModel();
            LoadDetail();
        }
        private bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtTypeCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại cuộc họp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTypeName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại cuộc họp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTypeName.Text))
            {
                MessageBox.Show("Vui lòng nhập Nhóm loại cuộc họp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Expression ex1 = new Expression(MeetingTypeModel_Enum.ID.ToString(), meetingType.ID, "<>");
            Expression ex2 = new Expression(MeetingTypeModel_Enum.TypeCode.ToString(), txtTypeCode.Text.Trim());
            Expression ex3 = new Expression(MeetingTypeModel_Enum.IsDelete.ToString(), 0);
            List<MeetingTypeModel> lstCheck = SQLHelper<MeetingTypeModel>.FindByExpression(ex1.And(ex2).And(ex3));
            if (lstCheck.Count > 0)
            {
                MessageBox.Show("Mã loại cuộc họp đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;

            MeetingTypeModel newModel = SQLHelper<MeetingTypeModel>.FindByID(meetingType.ID);
            newModel.TypeCode = txtTypeCode.Text.Trim();
            newModel.TypeName = txtTypeName.Text.Trim();
            newModel.TypeContent = txtTypeContent.Text.Trim();
            newModel.GroupID = TextUtils.ToInt(cboGroupType.SelectedIndex);
            newModel.IsDelete = false;
            if (newModel.ID > 0) SQLHelper<MeetingTypeModel>.Update(newModel);
            else newModel.ID = SQLHelper<MeetingTypeModel>.Insert(newModel).ID;

            return true;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }

        }
    }
}
