using BMS.Utils;
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
    public partial class frmCourseTypeDetail : _Forms
    {
        //public int id = 0;
        public CourseTypeModel courseType = new CourseTypeModel();
        public frmCourseTypeDetail()
        {
            InitializeComponent();
        }

        private void frmCourseTypeDetail_Load(object sender, EventArgs e)
        {
            LoadDetail();

        }
        private void LoadDetail()
        {

            //CourseTypeModel course = SQLHelper<CourseTypeModel>.FindByID(id);
            if (courseType.ID > 0)
            {
                txtSTT.Value = TextUtils.ToInt(courseType.STT);
                txtTypeCode.Text = courseType.CourseTypeCode;
                txtTypeName.Text = courseType.CourseTypeName;
                chkIsLearnInTurn.Checked = TextUtils.ToBoolean(courseType.IsLearnInTurn);
            }
            else
            {
                CourseTypeModel course = SQLHelper<CourseTypeModel>.FindAll().OrderByDescending(x => x.STT).FirstOrDefault() ?? new CourseTypeModel();
                txtSTT.Value = TextUtils.ToInt(course.STT) + 1;
                txtTypeCode.Text = "";
                txtTypeName.Text = "";
            }
        }
        //private void Reset()
        //{
        //    courseType = new CourseTypeModel();
        //    LoadDetail();
        //}

        private bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtTypeCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã loại khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTypeName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên loại khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Expression ex1 = new Expression(CourseTypeModel_Enum.ID.ToString(), courseType.ID, "<>");
            Expression ex2 = new Expression(CourseTypeModel_Enum.CourseTypeCode.ToString(), txtTypeCode.Text.Trim());
            Expression ex3 = new Expression(CourseTypeModel_Enum.IsDeleted.ToString(), 0);
            List<CourseTypeModel> lstCheck = SQLHelper<CourseTypeModel>.FindByExpression(ex1.And(ex2).And(ex3));
            if (lstCheck.Count > 0)
            {
                MessageBox.Show($"Mã loại khóa học [{txtTypeCode.Text.Trim()}] đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;

        }
        private bool SaveData()
        {
            try
            {
                if (!CheckValidate()) return false;

                CourseTypeModel newModel = SQLHelper<CourseTypeModel>.FindByID(courseType.ID);
                newModel.CourseTypeCode = txtTypeCode.Text.Trim();
                newModel.CourseTypeName = txtTypeName.Text.Trim();
                newModel.STT = TextUtils.ToInt(txtSTT.Value);
                newModel.IsDeleted = false;
                newModel.IsLearnInTurn = chkIsLearnInTurn.Checked;

                if (newModel.ID > 0) SQLHelper<CourseTypeModel>.Update(newModel);
                else newModel.ID = SQLHelper<CourseTypeModel>.Insert(newModel).ID;



                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void btnAddAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }

        private void btnAddAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                //Reset();
                courseType = new CourseTypeModel();
                LoadDetail();
            }
        }
    }
}