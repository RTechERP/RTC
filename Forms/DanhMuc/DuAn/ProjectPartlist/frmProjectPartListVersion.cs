using BMS.Business;
using BMS.Model;
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
    public partial class frmProjectPartListVersion : _Forms
    {
        /// <summary>
        /// 0: Phiên bản Partlist
        /// 1: Phiên bản nhân công
        /// </summary>
        int type = 0;

        public ProjectPartListVersionModel partListVersion = new ProjectPartListVersionModel();
        public ProjectWorkerVersionModel workerVersion = new ProjectWorkerVersionModel();

        public int projectId = 0;
        public int projectSolutionId = 0;
        public int statusVersion = 0;

        public Action SaveEvent;

        public frmProjectPartListVersion(int type)
        {
            InitializeComponent();
            this.type = type;

        }

        private void frmProjectPartListVersion_Load(object sender, EventArgs e)
        {
            this.Text = type == 0 ? "PHIÊN BẢN DANH MỤC VẬT TƯ" : "PHIÊN BẢN NHÂN CÔNG";
            cboStatusVersion.SelectedIndex = statusVersion;

            LoadProjectSolution();
            LoadProjectType();
            LoadDetail();

            //var projectSeclected = (ProjectModel)cboProjectSolution.GetSelectedDataRow();
            //string typeText = type == 0 ? "DANH MỤC VẬT TƯ" : "NHÂN CÔNG DỰ ÁN";
            //string projectCode = projectSeclected == null ? "" : projectSeclected.ProjectCode;
            //this.Text = $"PHIÊN BẢN {typeText} - {projectCode}";
        }

        void LoadProjectSolution()
        {
            //List<ProjectSolutionModel> list = SQLHelper<ProjectSolutionModel>.FindAll().OrderByDescending(x => x.STT).ToList();

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectSolution", "A", new string[] { "@ProjectID" }, new object[] { projectId });

            cboProjectSolution.Properties.DisplayMember = "CodeSolution";
            cboProjectSolution.Properties.ValueMember = "ID";
            cboProjectSolution.Properties.DataSource = dt;
            cboProjectSolution.EditValue = projectSolutionId;
        }

        void LoadProjectType()
        {
            List<ProjectTypeModel> projects = SQLHelper<ProjectTypeModel>.FindAll();
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DataSource = projects;
        }

        void LoadDetail()
        {
            if (type == 0)
            {
                if (partListVersion.ID > 0)
                {
                    cboProjectType.EditValue = partListVersion.ProjectTypeID;
                    txtCode.Text = partListVersion.Code;
                    txtSTT.Value = TextUtils.ToInt(partListVersion.STT);

                    chkIsActive.Checked = TextUtils.ToBoolean(partListVersion.IsActive);
                    cboStatusVersion.SelectedIndex = TextUtils.ToInt(partListVersion.StatusVersion);
                    txtDescriptionVersion.Text = partListVersion.DescriptionVersion;
                }
                else
                {
                    //projectSolutionId = TextUtils.ToInt(cboProjectSolution.EditValue);
                    //int projectTypeId = TextUtils.ToInt(cboProjectType.EditValue);

                    //var exp1 = new Expression("ProjectSolutionID", projectSolutionId);
                    //var exp2 = new Expression("ProjectTypeID", projectTypeId);
                    //var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2));
                    //txtSTT.Value = versions.Count > 0 ? versions.Max(x => x.STT) + 1 : 1;
                    //txtCode.Text = $"V{txtSTT.Value}";

                    LoadVersionCode();
                }


            }
            else
            {
                if (workerVersion.ID > 0)
                {
                    txtCode.Text = workerVersion.Code;
                    txtSTT.Value = workerVersion.STT;
                    txtDescriptionVersion.Text = workerVersion.DescriptionVersion;
                    chkIsActive.Checked = workerVersion.IsActive;
                    cboProjectType.EditValue = workerVersion.ProjectTypeID;
                    cboStatusVersion.SelectedIndex = workerVersion.StatusVersion;
                }
                else
                {
                    //var version = SQLHelper<ProjectWorkerVersionModel>.FindByAttribute("ProjectID", TextUtils.ToInt(cboProjectSolution.EditValue)).OrderByDescending(x => x.STT).FirstOrDefault();
                    //txtSTT.Value = version != null ? version.STT + 1 : 1;
                    //txtCode.Text = $"V{txtSTT.Value}";

                    LoadVersionCode();
                }
            }

        }


        void LoadVersionCode()
        {
            projectSolutionId = TextUtils.ToInt(cboProjectSolution.EditValue);
            int projectTypeId = TextUtils.ToInt(cboProjectType.EditValue);

            var exp1 = new Expression("ProjectSolutionID", projectSolutionId);
            var exp2 = new Expression("ProjectTypeID", projectTypeId);
            var exp3 = new Expression("IsDeleted", 1, "<>");
            if (type == 0)
            {
                if (partListVersion.ID > 0) return;
                var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3));
                txtSTT.Value = versions.Count > 0 ? versions.Max(x => TextUtils.ToInt(x.STT)) + 1 : 1;
                txtCode.Text = $"V{txtSTT.Value}";
            }
            else
            {
                //var version = SQLHelper<ProjectWorkerVersionModel>.FindByAttribute("ProjectID", TextUtils.ToInt(cboProjectSolution.EditValue)).OrderByDescending(x => x.STT).FirstOrDefault();
                //txtSTT.Value = version != null ? version.STT + 1 : 1;
                //txtCode.Text = $"V{txtSTT.Value}";

                if (workerVersion.ID > 0) return;
                var versions = SQLHelper<ProjectWorkerVersionModel>.FindByExpression(exp1.And(exp2).And(exp3));
                txtSTT.Value = versions.Count > 0 ? versions.Max(x => x.STT) + 1 : 1;
                txtCode.Text = $"V{txtSTT.Value}";
            }

        }

        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboProjectSolution.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Giải pháp!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboProjectType.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Danh mục!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã phiên bản!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCode.Text.Trim());
                var exp2 = new Expression("ProjectSolutionID", TextUtils.ToInt(cboProjectSolution.EditValue));
                var exp3 = new Expression("ProjectTypeID", TextUtils.ToInt(cboProjectType.EditValue));
                var exp4 = new Expression("ID", partListVersion.ID, "<>");
                var exp5 = new Expression("StatusVersion", cboStatusVersion.SelectedIndex);
                var exp7 = new Expression(ProjectPartListVersionModel_Enum.IsDeleted.ToString(), 0);

                if (type == 0)
                {
                    var version = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5).And(exp7));
                    if (version.Count > 0)
                    {
                        MessageBox.Show($"Mã phiên bản [{txtCode.Text.Trim()}] đã tồn tại!", "Thông báo");
                        return false;
                    }
                }
                else
                {
                    exp4 = new Expression("ID", workerVersion.ID, "<>");
                    var exp6 = new Expression("IsDeleted", 1, "<>");
                    var version = SQLHelper<ProjectWorkerVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5).And(exp6));
                    if (version.Count > 0)
                    {
                        MessageBox.Show($"Mã phiên bản [{txtCode.Text.Trim()}] đã tồn tại!", "Thông báo");
                        return false;
                    }
                }

            }

            if (cboStatusVersion.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Trạng thái!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtDescriptionVersion.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mô tả!", "Thông báo");
                return false;
            }

            //Check sử dụng phiên bản nào chưa
            if (chkIsActive.Checked)
            {
                var exp1 = new Expression("ProjectSolutionID", TextUtils.ToInt(cboProjectSolution.EditValue));
                var exp2 = new Expression("ProjectTypeID", TextUtils.ToInt(cboProjectType.EditValue));
                var exp3 = new Expression("IsActive", 1);
                var exp4 = new Expression("ID", partListVersion.ID, "<>");
                var exp5 = new Expression("StatusVersion", cboStatusVersion.SelectedIndex);
                var exp6 = new Expression("IsDeleted", 1, "<>");
                if (type == 0)
                {
                    var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5).And(exp6));
                    if (versions.Count > 0)
                    {
                        MessageBox.Show($"Danh mục [{cboProjectType.Text}] đã có phiên bản khác được sử dụng.\nVui lòng kiểm tra lại!", "Thông báo");
                        return false;
                    }
                }
                else
                {
                    exp4 = new Expression("ID", workerVersion.ID, "<>");
                    var versions = SQLHelper<ProjectWorkerVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5).And(exp6));
                    if (versions.Count > 0)
                    {
                        MessageBox.Show($"Danh mục [{cboProjectType.Text}] đã có phiên bản khác được sử dụng.\nVui lòng kiểm tra lại!", "Thông báo");
                        return false;
                    }
                }
            }

            if (cboStatusVersion.SelectedIndex == 2 /*&& partListVersion.ID <= 0*/)
            {
                var exp1 = new Expression("ProjectTypeID", TextUtils.ToInt(cboProjectType.EditValue));
                var exp2 = new Expression("StatusVersion", cboStatusVersion.SelectedIndex);
                //var exp3 = new Expression("ID", partListVersion.ID, "<>");
                var exp4 = new Expression("ProjectSolutionID", TextUtils.ToInt(cboProjectSolution.EditValue));
                var exp5 = new Expression("IsDeleted", 0);

                if (type == 0)
                {
                    var exp3 = new Expression("ID", partListVersion.ID, "<>");
                    var versions = SQLHelper<ProjectPartListVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
                    if (versions.Count > 0)
                    {
                        MessageBox.Show($"Danh mục [{cboProjectType.Text}] đã có phiên bản PO!", "Thông báo");
                        return false;
                    }
                }
                else
                {
                    var exp3 = new Expression("ID", workerVersion.ID, "<>");
                    //var exp5 = new Expression("IsDeleted", 1, "<>");
                    var versions = SQLHelper<ProjectWorkerVersionModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
                    if (versions.Count > 0)
                    {
                        MessageBox.Show($"Danh mục [{cboProjectType.Text}] đã có phiên bản PO!", "Thông báo");
                        return false;
                    }
                }
            }

            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;

            if (type == 0)
            {
                partListVersion.ProjectSolutionID = TextUtils.ToInt(cboProjectSolution.EditValue);
                partListVersion.ProjectTypeID = TextUtils.ToInt(cboProjectType.EditValue);
                partListVersion.STT = TextUtils.ToInt(txtSTT.Value);
                partListVersion.Code = txtCode.Text.Trim();
                partListVersion.DescriptionVersion = txtDescriptionVersion.Text.Trim();
                partListVersion.IsActive = chkIsActive.Checked;
                partListVersion.StatusVersion = cboStatusVersion.SelectedIndex;

                if (partListVersion.ID > 0)
                {
                    //ProjectPartListVersionBO.Instance.Update(partListVersion);
                    SQLHelper<ProjectPartListVersionModel>.Update(partListVersion);
                }
                else
                {
                    //partListVersion.ID = (int)ProjectPartListVersionBO.Instance.Insert(partListVersion);
                    partListVersion.ID = SQLHelper<ProjectPartListVersionModel>.Insert(partListVersion).ID;
                }
            }
            else
            {
                workerVersion.ProjectSolutionID = TextUtils.ToInt(cboProjectSolution.EditValue);
                workerVersion.ProjectID = TextUtils.ToInt(cboProjectSolution.EditValue);
                workerVersion.STT = TextUtils.ToInt(txtSTT.Value);
                workerVersion.Code = txtCode.Text.Trim();
                workerVersion.DescriptionVersion = txtDescriptionVersion.Text.Trim();
                workerVersion.IsActive = chkIsActive.Checked;
                workerVersion.StatusVersion = cboStatusVersion.SelectedIndex;
                workerVersion.ProjectTypeID = TextUtils.ToInt(cboProjectType.EditValue);

                if (workerVersion.ID > 0)
                {
                    //ProjectWorkerVersionBO.Instance.Update(workerVersion);
                    SQLHelper<ProjectWorkerVersionModel>.Update(workerVersion);
                }
                else
                {
                    //partListVersion.ID = (int)ProjectWorkerVersionBO.Instance.Insert(workerVersion);
                    workerVersion.ID = SQLHelper<ProjectWorkerVersionModel>.Insert(workerVersion).ID;
                }
            }

            if (partListVersion.IsActive == false)
            {
                string sql = $"UPDATE dbo.ProjectPartList SET IsApprovedTBP = 0, IsApprovedPurchase = 0 WHERE ProjectPartListVersionID = {partListVersion.ID}";
                TextUtils.ExcuteSQL(sql);
            }

            if (SaveEvent != null) SaveEvent();

            return true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
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
                partListVersion = new ProjectPartListVersionModel();
                LoadDetail();
            }
        }

        private void frmProjectPartListVersion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtSTT_ValueChanged(object sender, EventArgs e)
        {
            txtCode.Text = $"V{txtSTT.Value}";
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            //LoadDetail();
        }

        private void cboProjectType_EditValueChanged(object sender, EventArgs e)
        {
            //LoadDetail();

            LoadVersionCode();
        }

        private void cboProjectSolution_EditValueChanged(object sender, EventArgs e)
        {
            LoadVersionCode();
        }
    }
}
