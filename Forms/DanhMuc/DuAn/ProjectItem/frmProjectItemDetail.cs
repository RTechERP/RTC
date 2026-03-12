using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using Forms.Classes;
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
    public partial class frmProjectItemDetail : _Forms
    {

        public ProjectItemModel projectItem = new ProjectItemModel();
        bool _isUpdatePlan = false;

        public frmProjectItemDetail()
        {
            InitializeComponent();
        }

        private void frmProjectItemDetail_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;

            LoadProject();
            LoadType();
            LoadUser();
            LoadEmployee();
            LoadSTT();
            LoadData();
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = list;
        }

        //void LoadType()
        //{
        //    //DataTable dt = TextUtils.Select("select ID, ProjectTypeName from ProjectType");
        //    List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.FindAll();
        //    cboTypeProjectItem.Properties.DisplayMember = "ProjectTypeName";
        //    cboTypeProjectItem.Properties.ValueMember = "ID";
        //    cboTypeProjectItem.Properties.DataSource = list;
        //    cboTypeProjectItem.Properties.AutoExpandAllNodes = true;
        //}

        void LoadType()
        {
            DataTable dt = TextUtils.GetTable("spGetProjectTypeChildren");

            cboTypeProjectItem.Properties.DisplayMember = "ProjectTypeName";
            cboTypeProjectItem.Properties.ValueMember = "ID";
            cboTypeProjectItem.Properties.DataSource = dt;
            cboTypeProjectItem.Properties.AutoExpandAllNodes = true;
        }

        void LoadUser()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserProjectItem", new string[] { "@ProjectID" }, new object[] { 0 });

            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = dt;
            cboUser.EditValue = Global.UserID;
        }

        void LoadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeRequestProjectItem", new string[] { }, new object[] { });
            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";
            cboEmployeeRequest.Properties.DataSource = list;
        }

        void LoadSTT()
        {
            var listItems = SQLHelper<ProjectItemModel>.FindByAttribute("ProjectID", TextUtils.ToInt(cboProject.EditValue));
            txtSTT.Value = TextUtils.ToInt(listItems.Max(x => x.STT)) + 1;

            if (projectItem.ID > 0)
            {
                txtSTT.Value = TextUtils.ToDecimal(projectItem.STT);
            }
        }

        void LoadData()
        {
            if (projectItem.ID <= 0) return;
            cboProject.EditValue = projectItem.ProjectID;
            cboStatus.SelectedIndex = projectItem.Status;
            cboTypeProjectItem.EditValue = projectItem.TypeProjectItem;
            cboUser.EditValue = projectItem.UserID;
            cboEmployeeRequest.EditValue = projectItem.EmployeeIDRequest;
            txtMission.Text = projectItem.Mission;
            txtReasonLate.Text = projectItem.ReasonLate;
            txtNote.Text = projectItem.Note;

            //Dự kiến
            dtpPlanStartDate.EditValue = projectItem.PlanStartDate;
            dtpPlanEndDate.EditValue = projectItem.PlanEndDate;
            txtTotalDayPlan.Value = projectItem.TotalDayPlan;

            //Thự tế
            dtpActualStartDate.EditValue = projectItem.ActualStartDate;
            dtpActualEndDate.EditValue = projectItem.ActualEndDate;
            txtPercentageActual.Value = projectItem.PercentageActual;

            cboProject.Enabled = !(projectItem.ID > 0);
            btnSave.Enabled = btnSaveNew.Enabled = btnAddReasonLate.Enabled = CheckIsPermission(projectItem.CreatedBy.Trim(), projectItem.UserID, projectItem.EmployeeIDRequest);
        }

        bool CheckIsPermission(string createdby, int userID, int employeeRequest)
        {
            int projectId = TextUtils.ToInt(cboProject.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectEmployeePermisstion", "A",
                                                        new string[] { "@ProjectID", "@EmployeeID" },
                                                        new object[] { projectId, Global.EmployeeID });

            int valueRow = TextUtils.ToInt(dt.Rows[0]["RowNumber"]);

            if (createdby == Global.LoginName.Trim() || userID == Global.UserID || employeeRequest == Global.EmployeeID) //Người tạo hạng mục hoặc người phụ trách hạng mục hoặc người giao việc
            {
                return true;
            }

            if (valueRow > 0 || Global.IsAdmin)
            {
                return true;
            }

            return false;
        }

        bool CheckValidate()
        {
            DateTime? planStart = TextUtils.ToDate4(dtpPlanStartDate.EditValue);
            DateTime? planEnd = TextUtils.ToDate4(dtpPlanEndDate.EditValue);
            DateTime? actualStart = TextUtils.ToDate4(dtpActualStartDate.EditValue);
            DateTime? actualEnd = TextUtils.ToDate4(dtpActualEndDate.EditValue);

            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng nhập Trạng thái!", "Thông báo");
                return false;
            }
            else if (cboStatus.SelectedIndex == 2)
            {
                if (!actualStart.HasValue)
                {
                    MessageBox.Show("Vui lòng nhập Ngày bắt đầu (THỰC TẾ)!", "Thông báo");
                    return false;
                }

                if (!actualEnd.HasValue)
                {
                    MessageBox.Show("Vui lòng nhập Ngày kết thúc (THỰC TẾ)!", "Thông báo");
                    return false;
                }
            }

            if (TextUtils.ToInt(cboTypeProjectItem.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Kiểu!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboUser.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người phụ trách!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeRequest.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người giao việc!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtMission.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Công việc!", "Thông báo");
                return false;
            }

            if (!planStart.HasValue)
            {
                MessageBox.Show($"Vui lòng nhập Ngày bắt đầu dự kiến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!planEnd.HasValue)
            {
                MessageBox.Show($"Vui lòng nhập Ngày kết thúc dự kiến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (planStart.HasValue && planEnd.HasValue)
            {
                var totalDays = (planEnd.Value.Date - planStart.Value.Date).TotalDays;
                if (totalDays < 0)
                {
                    MessageBox.Show("Ngày kết thúc dự kiến không được nhỏ hơn Ngày bắt đầu dự kiến!", "Thông báo");
                    return false;
                }
            }

            if (actualStart.HasValue && actualEnd.HasValue)
            {
                var totalDays = (actualEnd.Value.Date - actualStart.Value.Date).TotalDays;
                if (totalDays < 0)
                {
                    MessageBox.Show("Ngày kết thúc thực tế không được nhỏ hơn Ngày bắt đầu thực tế!", "Thông báo");
                    return false;
                }
            }

            var exp1 = new Expression("ProjectID", TextUtils.ToInt(cboProject.EditValue));
            var exp2 = new Expression("STT", txtSTT.Value.ToString());
            var exp3 = new Expression("ID", projectItem.ID, "<>");
            var listItems = SQLHelper<ProjectItemModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (listItems.Count() > 0)
            {
                MessageBox.Show($"STT {txtSTT.Value} đã tồn tại!", "Thông báo");
                return false;
            }


            if (projectItem.ID > 0)
            {
                ProjectItemModel itemOld = new ProjectItemModel()
                {
                    TypeProjectItem = projectItem.TypeProjectItem,
                    UserID = projectItem.UserID,
                    EmployeeIDRequest = projectItem.EmployeeIDRequest,
                    PlanStartDate = projectItem.PlanStartDate,
                    PlanEndDate = projectItem.PlanEndDate,
                    TotalDayPlan = projectItem.TotalDayPlan,
                    Mission = projectItem.Mission,
                };

                string totalDayPlans = String.Format("{0:0.0}", txtTotalDayPlan.Value);
                ProjectItemModel itemNew = new ProjectItemModel()
                {
                    TypeProjectItem = TextUtils.ToInt(cboTypeProjectItem.EditValue),
                    UserID = TextUtils.ToInt(cboUser.EditValue),
                    EmployeeIDRequest = TextUtils.ToInt(cboEmployeeRequest.EditValue),
                    PlanStartDate = TextUtils.ToDate4(dtpPlanStartDate.EditValue),
                    PlanEndDate = TextUtils.ToDate4(dtpPlanEndDate.EditValue),
                    TotalDayPlan = TextUtils.ToDecimal(totalDayPlans),
                    Mission = txtMission.Text.Trim(),
                };

                var resultCompare = TextUtils.DeepEquals(itemOld, itemNew);
                bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));
                _isUpdatePlan = !equal;
                if (!equal)
                {
                    string propertyText = "";
                    List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);
                    foreach (string property in propertys)
                    {
                        propertyText += TextUtils.ToString(cGlobVar.projectItem.GetType().GetProperty(property).GetValue(cGlobVar.projectItem)) + "; ";
                    }

                    if ((string.IsNullOrEmpty(txtReasonLate.Text.Trim()) || txtReasonLate.Text.Trim().ToLower() == projectItem.ReasonLate.Trim().ToLower()))
                    {
                        MessageBox.Show($"Bạn vừa thay đổi thông tin [{propertyText}].\nVui lòng nhập Lý do phát sinh!", "Thông báo");
                        return false;
                    }
                }
            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;

            SaveLogItem();
            projectItem.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            projectItem.Code = GetItemCode();
            projectItem.STT = txtSTT.Value.ToString();
            projectItem.Status = cboStatus.SelectedIndex;
            projectItem.TypeProjectItem = TextUtils.ToInt(cboTypeProjectItem.EditValue);
            projectItem.UserID = TextUtils.ToInt(cboUser.EditValue);
            projectItem.EmployeeIDRequest = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            projectItem.Mission = txtMission.Text.Trim();
            projectItem.ReasonLate = txtReasonLate.Text.Trim();
            projectItem.Note = txtNote.Text.Trim();

            //Dự kiến
            projectItem.PlanStartDate = TextUtils.ToDate4(dtpPlanStartDate.EditValue);
            projectItem.PlanEndDate = TextUtils.ToDate4(dtpPlanEndDate.EditValue);
            projectItem.TotalDayPlan = txtTotalDayPlan.Value;
            //projectItem.PercentItem = CalculatorPercentItem();

            //Thực tế
            projectItem.ActualStartDate = TextUtils.ToDate4(dtpActualStartDate.EditValue);
            projectItem.ActualEndDate = TextUtils.ToDate4(dtpActualEndDate.EditValue);
            projectItem.PercentageActual = txtPercentageActual.Value;
            if (projectItem.ActualStartDate.HasValue && projectItem.ActualEndDate.HasValue)
            {
                projectItem.TotalDayActual = TextUtils.ToDecimal((projectItem.ActualEndDate.Value.Date - projectItem.ActualStartDate.Value.Date).TotalDays);
            }

            if (projectItem.ActualEndDate.HasValue)
            {
                projectItem.UpdatedDateActual = DateTime.Now;
            }

            //var itemLate = ProjectItemLate();
            projectItem.ItemLate = ProjectItemLate();// TextUtils.ToInt(itemLate.GetType().GetProperty("itemLate").GetValue(itemLate));

            //if (projectItem.PlanEndDate.HasValue && projectItem.ActualEndDate.HasValue)
            //{
            //    projectItem.IsUpdateLate = projectItem.ActualEndDate.Value.to
            //}
            //projectItem.IsUpdateLate = TextUtils.ToBoolean(itemLate.GetType().GetProperty("updateLate").GetValue(itemLate));

            if (projectItem.ID <= 0)
            {
                SQLHelper<ProjectItemModel>.Insert(projectItem);
            }
            else
            {
                SQLHelper<ProjectItemModel>.Update(projectItem);
            }
            UpdatePercentItem();
            return true;
        }

        void SaveLogItem()
        {
            //foreach (DataRow row in dataRows)
            //{

            //}

            //int id = TextUtils.ToInt(row["ID"]);
            //ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);
            if (!_isUpdatePlan) return;
            if (projectItem.ID <= 0) return;
            ProjectItemLogModel log = new ProjectItemLogModel();
            log.ProjectItemID = projectItem.ID;
            log.Status = projectItem.Status;
            log.ContentLog = projectItem.Mission;
            log.Note = projectItem.Note;
            log.DateStart = projectItem.PlanStartDate;
            log.DateEnd = projectItem.PlanEndDate;

            SQLHelper<ProjectItemLogModel>.Insert(log);

            projectItem.IsApproved = 0;
            SQLHelper<ProjectItemLogModel>.Update(log);
        }
        string GetItemCode()
        {
            var listItem = SQLHelper<ProjectItemModel>.FindByAttribute("ProjectID", TextUtils.ToInt(cboProject.EditValue));
            string code = $"{cboProject.Text}_{listItem.Count() + 1}";
            return code;
        }

        void UpdatePercentItem()
        {
            var listItem = SQLHelper<ProjectItemModel>.FindByAttribute("ProjectID", TextUtils.ToInt(cboProject.EditValue));
            decimal totalDay = listItem.Sum(x => x.TotalDayPlan);
            foreach (ProjectItemModel item in listItem)
            {
                if (totalDay <= 0) continue;
                item.PercentItem = (item.TotalDayPlan / totalDay) * 100;
                SQLHelper<ProjectItemModel>.Update(item);
            }
        }

        int ProjectItemLate()
        {
            int itemLate = 0;
            //bool updateLate = false;

            DateTime? planStart = TextUtils.ToDate4(dtpPlanStartDate);
            DateTime? planEnd = TextUtils.ToDate4(dtpPlanEndDate);
            DateTime? actualStart = TextUtils.ToDate4(dtpActualStartDate);
            DateTime? actualEnd = TextUtils.ToDate4(dtpActualEndDate);

            //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
            //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed
            if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
            {
                if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0)
                {
                    itemLate = 2;
                }
                else if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                {
                    itemLate = 2;
                }

            }

            //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
            //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
            if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
            {
                if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                {
                    itemLate = 1;
                }
            }

            //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
            //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed
            if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
            {
                if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                {
                    itemLate = 2;
                }
            }

            //Nếu chỉ có ngày bắt đầu dự kiến
            if (planStart.HasValue && !planEnd.HasValue && !actualStart.HasValue && !actualEnd.HasValue)
            {
                if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                {
                    itemLate = 2;
                }
            }

            return itemLate;
        }

        void LoadPlanDate(Control control)
        {
            if (Lib.LockEvents) return;
            try
            {
                Lib.LockEvents = true;
                DateTime? dateStart = TextUtils.ToDate4(dtpPlanStartDate.EditValue);
                DateTime? dateEnd = TextUtils.ToDate4(dtpPlanEndDate.EditValue);
                double totalDays = TextUtils.ToDouble(txtTotalDayPlan.Value);

                if (control.Name == dtpPlanStartDate.Name && dateStart.HasValue)
                {
                    if (totalDays > 0)
                    {
                        dtpPlanEndDate.EditValue = dateStart.Value.AddDays(totalDays - 1);
                    }
                    else if (dateEnd.HasValue)
                    {
                        txtTotalDayPlan.Value = TextUtils.ToDecimal((dateEnd.Value.Date - dateStart.Value.Date).TotalDays + 1);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (control.Name == txtTotalDayPlan.Name && totalDays > 0)
                {
                    if (dateStart.HasValue)
                    {
                        dtpPlanEndDate.EditValue = dateStart.Value.AddDays(totalDays - 1);
                    }
                    else if (dateEnd.HasValue)
                    {
                        dtpPlanStartDate.EditValue = dateEnd.Value.AddDays(-(totalDays - 1));
                    }
                    else
                    {
                        return;
                    }
                }
                else if (control.Name == dtpPlanEndDate.Name && dateEnd.HasValue)
                {
                    if (dateStart.HasValue)
                    {
                        totalDays = (dateEnd.Value.Date - dateStart.Value.Date).TotalDays + 1;
                        if (totalDays <= 0)
                        {
                            MessageBox.Show("Ngày kết thúc dự kiến không được nhỏ hơn Ngày bắt đầu dự kiến!", "Thông báo");
                            return;
                        }
                        txtTotalDayPlan.Value = TextUtils.ToDecimal(totalDays);
                    }
                    else if (totalDays > 0)
                    {
                        dtpPlanStartDate.EditValue = dateEnd.Value.AddDays(-(totalDays - 1));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }


        void LoadActualDate(Control control)
        {
            if (Lib.LockEvents) return;
            try
            {
                Lib.LockEvents = true;
                DateTime? dateStart = TextUtils.ToDate4(dtpActualStartDate.EditValue);
                DateTime? dateEnd = TextUtils.ToDate4(dtpActualEndDate.EditValue);
                int type = cboStatus.SelectedIndex;


                if (control.Name == dtpActualStartDate.Name && dateStart.HasValue)
                {
                    if (dateEnd.HasValue)
                    {
                        cboStatus.SelectedIndex = 2;
                        txtPercentageActual.Value = 100;
                    }
                    else
                    {
                        cboStatus.SelectedIndex = 1;
                        txtPercentageActual.Value = 0;
                    }
                }
                else if (control.Name == dtpActualEndDate.Name)
                {
                    cboStatus.SelectedIndex = 2;
                    txtPercentageActual.Value = 100;
                }
                else if (control.Name == cboStatus.Name)
                {
                    if (type == 0 || type == 3)
                    {
                        dtpActualStartDate.EditValue = dtpActualEndDate.EditValue = null;
                    }
                    else if (type == 1)
                    {
                        dtpActualStartDate.EditValue = DateTime.Now;
                        dtpActualEndDate.EditValue = null;
                    }
                    else if (type == 2)
                    {
                        dtpActualStartDate.EditValue = dtpActualEndDate.EditValue = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                projectItem = new ProjectItemModel();
                LoadSTT();

                txtMission.Clear();
                txtReasonLate.Clear();
                txtNote.Clear();

                txtMission.Focus();
            }
        }

        private void frmProjectItemDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtReasonLate_Click(object sender, EventArgs e)
        {

        }

        private void txtReasonLate_DoubleClick(object sender, EventArgs e)
        {
            btnAddReasonLate_Click(null, null);
        }

        private void dtpPlanStartDate_EditValueChanged(object sender, EventArgs e)
        {
            var control = (DateEdit)sender;
            LoadPlanDate(control);
        }

        private void dtpPlanEndDate_EditValueChanged(object sender, EventArgs e)
        {
            var control = (DateEdit)sender;
            LoadPlanDate(control);
        }

        private void txtTotalDayPlan_ValueChanged(object sender, EventArgs e)
        {
            var control = (NumericUpDown)sender;
            LoadPlanDate(control);
        }

        private void btnAddReasonLate_Click(object sender, EventArgs e)
        {
            if (projectItem.ID <= 0) return;
            ProjectItemModel model = SQLHelper<ProjectItemModel>.FindByID(projectItem.ID);
            frmProjectItemProblem frm = new frmProjectItemProblem();
            frm.projectItem = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var listProblems = frm.SetContent().Split('\n');
                txtReasonLate.Text = string.Join(Environment.NewLine, listProblems);

            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = (System.Windows.Forms.ComboBox)sender;
            LoadActualDate(control);
        }

        private void dtpActualStartDate_EditValueChanged(object sender, EventArgs e)
        {
            var control = (DateEdit)sender;
            LoadActualDate(control);
        }

        private void dtpActualEndDate_EditValueChanged(object sender, EventArgs e)
        {
            var control = (DateEdit)sender;
            LoadActualDate(control);
        }
    }
}
