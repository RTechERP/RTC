using BMS;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using BMS.Utils; 



namespace BMS
{
    public partial class frmAddExpertise : _Forms
    {
        public frmAddExpertise()
        {
            InitializeComponent();

        }

        private void frmAddExpertise_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            grdExpertise.DataSource = null;
            int kpiSessionID = 0;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
                DataTable dt = TextUtils.GetTable("spGetDataCourse");
                grdData.DataSource = dt;
                grvData.ExpandGroupLevel(0);
                grvData.ExpandGroupLevel(1);

                //DataTable data = TextUtils.GetDataTableFromSP("spGetPoistionTypeByKPISessionID",
                //                                    new string[] { "@KpiSessionID" },
                //                                    new object[] { kpiSessionID });


                DataTable data = SQLHelper<object>.GetTableData("SELECT * FROM dbo.KPIPositionType WHERE IsDeleted = 0");
                grdExpertise.DataSource = data;

                if (!data.Columns.Contains("OptionSelection"))
                {
                    data.Columns.Add("OptionSelection", typeof(bool));
                }

                foreach (DataRow dr in data.Rows)
                {
                    dr["OptionSelection"] = false;
                }
            }

        }

        void LoadSpecialtyByCourse(int courseID)
        {
            if (courseID <= 0) return;
            //DataTable data = TextUtils.GetDataTableFromSP("spGetPoistionTypeByKPISessionID",
            //                                              new string[] { "@KpiSessionID" },
            //                                              new object[] { 0 });

            DataTable data = SQLHelper<object>.GetTableData("SELECT * FROM dbo.KPIPositionType WHERE IsDeleted = 0");

            if (!data.Columns.Contains("OptionSelection"))
                data.Columns.Add("OptionSelection", typeof(bool));

            Expression exCourse = new Expression("CourseID", courseID);
            List<CourseSpecializationLinkModel> mapping =
                SQLHelper<CourseSpecializationLinkModel>.FindByExpression(exCourse);

            foreach (DataRow dr in data.Rows)
            {
                int specializationID = TextUtils.ToInt(dr["ID"]);
                var exist = mapping.FirstOrDefault(x => x.PositionTypeID == specializationID && x.IsDeleted == false);
                dr["OptionSelection"] = exist != null;
            }

            grdExpertise.DataSource = data;
        }
        private bool ValidateForm()
        {
            grvData.CloseEditor();
            grvData.UpdateCurrentRow();
            grvExpertise.CloseEditor();
            grvExpertise.UpdateCurrentRow();

            var index = grvData.GetSelectedRows();
            foreach(int i in index)
            {
                var rowId = Convert.ToInt32(grvData.GetRowCellValue(i, "ID"));
                if ( rowId <= 0)
                {
                    MessageBox.Show("Vui lòng chọn khóa học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }    


            bool anyChecked = false;
            for (int i = 0; i < grvExpertise.RowCount; i++)
            {
                bool isChecked = Convert.ToBoolean(grvExpertise.GetRowCellValue(i, "OptionSelection"));
                if (isChecked)
                {
                    anyChecked = true;
                    break;
                }
            }

            if (!anyChecked)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một chuyên môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool saveData()
        {

            if (!ValidateForm())
            {
                return false;
            }

            DataRow row = grvData.GetDataRow(grvData.FocusedRowHandle);
            int courseID = TextUtils.ToInt(row["ID"]);

            Expression ex = new Expression("CourseID", courseID);
            List<CourseSpecializationLinkModel> oldMapping =
                SQLHelper<CourseSpecializationLinkModel>.FindByExpression(ex);

            var listExpertise = new List<(int ID, bool IsChecked)>();
            for (int i = 0; i < grvExpertise.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvExpertise.GetRowCellValue(i, "ID"));
                bool chk = Convert.ToBoolean(grvExpertise.GetRowCellValue(i, "OptionSelection"));
                listExpertise.Add((id, chk));
            }

            try
            {
                foreach (var item in listExpertise)
                {
                    var exist = oldMapping.FirstOrDefault(x => x.PositionTypeID == item.ID);

                    if (item.IsChecked)
                    {
                        if (exist == null)
                        {
                            SQLHelper<CourseSpecializationLinkModel>.Insert(new CourseSpecializationLinkModel
                            {
                                CourseID = courseID,
                                PositionTypeID = item.ID,
                                IsDeleted = false
                            });
                        }
                        else
                        {
                            if (TextUtils.ToBoolean(exist.IsDeleted))
                            {
                                exist.IsDeleted = false;
                                SQLHelper<CourseSpecializationLinkModel>.Update(exist);
                            }
                        }
                    }
                    else
                    {

                        if (exist != null && exist.IsDeleted == false)
                        {
                            exist.IsDeleted = true;
                            SQLHelper<CourseSpecializationLinkModel>.Update(exist);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lưu thất bại: " + e.Message);
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                loadData();
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (grvData.FocusedRowHandle < 0) return;

                DataRow row = grvData.GetDataRow(grvData.FocusedRowHandle);
                if (row == null) return;

                int courseID = TextUtils.ToInt(row["ID"]);
                LoadSpecialtyByCourse(courseID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn khóa học: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.Close();
            }
        }
    }
}
