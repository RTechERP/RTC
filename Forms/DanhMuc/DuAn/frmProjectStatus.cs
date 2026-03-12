using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
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
    public partial class frmProjectStatus : _Forms
    {
        public int projectID;
        public frmProjectStatus()
        {
            InitializeComponent();
        }

        private void frmProjectStatus_Load(object sender, EventArgs e)
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectStatus", "A", new string[] { "@ProjectID" }, new object[] { projectID });
            grdData.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!saveData()) return;
            this.DialogResult = DialogResult.OK;
        }
        private bool saveData()
        {
            grvData.CloseEditor();
            if (!validate()) return false;


            ProjectModel project = (ProjectModel)ProjectBO.Instance.FindByPK(projectID);

            for (int i = 0; i < grvData.RowCount; i++)
            {
                ProjectStatusDetailModel model = new ProjectStatusDetailModel();

                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                bool selected = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colSelected));

                if (id > 0 || selected)
                {
                    if (id > 0)
                    {
                        model = (ProjectStatusDetailModel)ProjectStatusDetailBO.Instance.FindByPK(id);
                    }

                    model.ProjectID = projectID;
                    model.ProjectStatusID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectStatusID));
                    model.EstimatedStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEstimatedStartDate));
                    model.EstimatedEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEstimatedEndDate));
                    model.ActualStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualStartDate));
                    model.ActualEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualEndDate));
                    model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectStatusID));
                    model.Selected = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colSelected));

                    if (model.Selected)
                    {
                        project.ProjectStatus = model.ProjectStatusID;
                        ProjectBO.Instance.Update(project);
                    }

                    if (model.ID > 0)
                    {
                        model.ID = id;
                        ProjectStatusDetailBO.Instance.Update(model);
                    }
                    else
                    {
                        ProjectStatusDetailBO.Instance.Insert(model);
                    }
                }

            }

            
            



            return true;
        }
        private bool validate()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colStatus))))
                {
                    MessageBox.Show("Vui lòng chọn Trạng thái!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                var estimateStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEstimatedStartDate));
                var estimateEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEstimatedEndDate));
                var actualStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualStartDate));
                var actualEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualEndDate));
                if (estimateStartDate != null && estimateEndDate != null)
                {
                    if (estimateStartDate > estimateEndDate)
                    {
                        MessageBox.Show("Ngày bắt đầu dự kiến không được lớn hơn Ngày kết thúc dự kiến!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            return true;
        }


        private void grdData_Click(object sender, EventArgs e)
        {

        }
        ArrayList listIdDelete = new ArrayList();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string status = TextUtils.ToString(grvData.GetFocusedRowCellValue(colStatus));
            int rowIndex = grvData.GetSelectedRows()[0];
            if (MessageBox.Show(string.Format($"Bạn có chắc chắn muốn Trạng thái đáp án [{status}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                listIdDelete.Add(ID);
            }
        }

        private void chkSelected_EditValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colSelected, false);
            }

            grvData.SetFocusedRowCellValue(colSelected, true);
        }
    }

}