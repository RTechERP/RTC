using BMS.Business;
using BMS.Model;
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
    public partial class frmRegisterIdeaScore : _Forms
    {
        public RegisterIdeaModel registerIdea = new RegisterIdeaModel();
        RegisterIdeaScoreModel ideaScore = new RegisterIdeaScoreModel();
        public frmRegisterIdeaScore()
        {
            InitializeComponent();

        }
        private void frmRegisterIdeaScore_Load(object sender, EventArgs e)
        {
            txtIdeaName.ReadOnly = true;
            chkIsTBP.ReadOnly = true;
            cboDepartment.ReadOnly = true;
            LoadTBP();
            LoadDepartment();
            LoadData();

        }
        private void LoadTBP()
        {
            EmployeeModel ideaAuthor = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(registerIdea.EmployeeID));
            if (ideaAuthor.ID == 0) return;
            DepartmentModel department = SQLHelper<DepartmentModel>.FindByID(TextUtils.ToInt(ideaAuthor.DepartmentID));

            if (Global.EmployeeID == department.HeadofDepartment)
            {
                chkIsTBP.Checked = true;
                cboDepartment.ReadOnly = false;
            }
        }

        private void LoadData()
        {
            if (registerIdea.ID > 0)
            {
                var exp1 = new Expression("DepartmentID", Global.DepartmentID);
                var exp2 = new Expression("RegisterIdeaID", registerIdea.ID);
                RegisterIdeaScoreModel exitsIdeaScore = SQLHelper<RegisterIdeaScoreModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                if (exitsIdeaScore != null)
                {
                    ideaScore = exitsIdeaScore;
                    if (ideaScore.Score >= 1)
                    {
                        cboScore.SelectedIndex = TextUtils.ToInt(ideaScore.Score - 1);
                    }
                }

            }

            RegisterIdeaDetailModel ideaDetail = SQLHelper<RegisterIdeaDetailModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID).FirstOrDefault();
            if (ideaDetail != null)
            {
                txtIdeaName.Text = ideaDetail.Description;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetRegisterIdeaScore", "A", new string[] { "@RegisterIdeaID", "@EmployeeID" }, new object[] { registerIdea.ID, Global.EmployeeID });
            grdData.DataSource = dt;

        }

        private void LoadDepartment()
        {

            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            if (registerIdea == null || registerIdea.ID == 0) return;

            List<RegisterIdeaScoreModel> listIdeaScore = SQLHelper<RegisterIdeaScoreModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID);
            string selectedDepartIDs = "";
            foreach (var item in listIdeaScore)
            {
                selectedDepartIDs += TextUtils.ToString(item.DepartmentID) + ',';
            }
            cboDepartment.SetEditValue(selectedDepartIDs);

        }

        private void btnSaveCLose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }

        }

        private bool SaveData()
        {
            if (string.IsNullOrEmpty(cboScore.Text))
            {
                MessageBox.Show($"Vui lòng nhập Điểm số!", "Thông báo");
                return false;
            }

            var exp1 = new Expression("RegisterIdeaID", registerIdea.ID);
            //Get ds phòng ban liên quan trong DB
            var exp3 = new Expression("IsTBP", 1, "<>");
            var exp4 = new Expression("IsBGD", 1, "<>");
            var exp5 = new Expression("Score", 0, "<=");
            List<RegisterIdeaScoreModel> listDeparment = SQLHelper<RegisterIdeaScoreModel>.FindByExpression(exp1.And(exp3).And(exp4).And(exp5));

            if (listDeparment.Count > 0) SQLHelper<RegisterIdeaScoreModel>.DeleteListModel(listDeparment);

            var listDeparmentCheckeds = cboDepartment.Properties.GetItems().GetCheckedValues();

            foreach (var item in listDeparmentCheckeds)
            {
                int departmentID = TextUtils.ToInt(item);
                var exp2 = new Expression("DepartmentID", departmentID);
                RegisterIdeaScoreModel scoreDepartment = SQLHelper<RegisterIdeaScoreModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                scoreDepartment = scoreDepartment ?? new RegisterIdeaScoreModel();

                if (scoreDepartment.IsTBP) continue;
                if (scoreDepartment.IsBGD) continue;
                if (scoreDepartment.Score > 0) continue;

                scoreDepartment.RegisterIdeaID = registerIdea.ID;
                scoreDepartment.DepartmentID = departmentID;

                if (scoreDepartment.ID <= 0)
                {
                    SQLHelper<RegisterIdeaScoreModel>.Insert(scoreDepartment);
                }
                else
                {
                    SQLHelper<RegisterIdeaScoreModel>.Update(scoreDepartment);
                }
            }

            //// luu phong ban lien quan
            //var exp1 = new Expression("Score", 0, "<>");
            //var exp2 = new Expression("RegisterIdeaID", registerIdea.ID);
            //List<RegisterIdeaScoreModel> listScoreApproved = SQLHelper<RegisterIdeaScoreModel>.FindByExpression(exp1.And(exp2));
            //List<DepartmentModel> listAllDepartment = SQLHelper<DepartmentModel>.FindAll();
            //List<int> listAllScoreID = new List<int>();
            //List<int> listScoreDepartID = new List<int>();
            //List<int> listSelectedDepartID = new List<int>();

            ////danh sách các phòng ban liên quan đã chấm điểm
            //foreach (var item in listScoreApproved)
            //{
            //    listScoreDepartID.Add(item.DepartmentID);
            //}
            ////danh sách các phòng ban checked
            //var listSelected = cboDepartment.Properties.GetItems().GetCheckedValues();
            //foreach (var item in listSelected)
            //{
            //    int id = TextUtils.ToInt(item);
            //    listSelectedDepartID.Add(id);
            //}

            //// check xem có phòng ban nào đã chấm điểm bị uncheck không
            //foreach (var item in listScoreDepartID)
            //{
            //    int isSelected = listSelectedDepartID.IndexOf(item);
            //    if (isSelected < 0)
            //    {
            //        DepartmentModel department = listAllDepartment.Where(d => d.ID == item).FirstOrDefault();
            //        if (department == null || department.Name == null)  return false;
            //        MessageBox.Show($"Phòng ban {department.Name} đã chấm điểm. Không thể chỉnh sửa!", "Thông báo");
            //        return false;
            //    }
            //}

            //// thêm các phòng ban mới và xóa các phòng ban cũ chưa chấm điểm

            //List<RegisterIdeaScoreModel> listAllScore = SQLHelper<RegisterIdeaScoreModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID);
            ////danh sách tất cả các phòng ban liên quan đã chấm điểm
            //foreach (var item in listAllScore)
            //{
            //    listAllScoreID.Add(item.DepartmentID);
            //}
            ////Thêm các phòng ban mới được tích vào listAllScoreID
            //foreach(var item in listSelectedDepartID)
            //{
            //    int index = listAllScoreID.IndexOf(item);
            //    if(index < 0)
            //    {
            //        listAllScoreID.Add(item);
            //    }
            //}
            //foreach (var item in listAllScoreID)
            //{
            //    int isScore = listScoreDepartID.IndexOf(item);
            //    int isChecked = listSelectedDepartID.IndexOf(item);
            //    var exp3 = new Expression("DepartmentID", item);
            //    var exp4 = new Expression("RegisterIdeaID", registerIdea.ID);                
            //    RegisterIdeaScoreModel scoreModel = SQLHelper<RegisterIdeaScoreModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            //    if(scoreModel == null)
            //    {
            //        scoreModel = new RegisterIdeaScoreModel();
            //    }
            //    decimal score = scoreModel.Score;             
            //    if (isScore < 0 && isChecked >= 0)
            //    {
            //        RegisterIdeaScoreModel insertScore = new RegisterIdeaScoreModel()
            //        {
            //            DepartmentID = item,
            //            RegisterIdeaID = scoreModel.RegisterIdeaID
            //        };
            //        RegisterIdeaScoreBO.Instance.Insert(insertScore);
            //    }
            //    if (isScore > 0 && score == 0 && isChecked >= 0)
            //    {
            //        RegisterIdeaScoreBO.Instance.Delete(scoreModel.ID);
            //    }
            //}

            ideaScore.Score = TextUtils.ToDecimal(cboScore.SelectedIndex + 1);
            ideaScore.RegisterIdeaID = registerIdea.ID;
            if (chkIsTBP.Checked == true)
            {
                ideaScore.IsApprovedTBP = true;
                ideaScore.DateApprovedTBP = DateTime.Now;
                ideaScore.IsTBP = true;
                ideaScore.DepartmentID = Global.DepartmentID;
                registerIdea.IsApprovedTBP = true;
                registerIdea.DateApprovedTBP = DateTime.Now;
            }
            else if (Global.DepartmentID == 1)
            {
                ideaScore.DepartmentID = Global.DepartmentID;
                ideaScore.IsApprovedBGD = true;
                ideaScore.DateApprovedBGD = DateTime.Now;
                ideaScore.IsBGD = true;
                registerIdea.IsApproved = true;
                registerIdea.DateApproved = DateTime.Now;
                registerIdea.ApprovedID = Global.EmployeeID;
            }
            else
            {
                ideaScore.DepartmentID = Global.DepartmentID;
            }

            if (ideaScore.ID > 0)
            {
                RegisterIdeaScoreBO.Instance.Update(ideaScore);
            }
            else
            {
                RegisterIdeaScoreBO.Instance.Insert(ideaScore);
            }
            RegisterIdeaBO.Instance.Update(registerIdea);
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                //this.DialogResult = DialogResult.OK;
                LoadData();
            }
        }
    }
}