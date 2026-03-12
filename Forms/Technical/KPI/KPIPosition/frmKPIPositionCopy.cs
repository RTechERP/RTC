using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.Utils.Gesture;
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
    public partial class frmKPIPositionCopy : _Forms
    {
        int _departmentID = 0;
        public KPISessionModel kpiSession = new KPISessionModel();
        public frmKPIPositionCopy(int departmentID)
        {
            InitializeComponent();
            _departmentID = departmentID;
        }
        private void KPISessionDetails_Load(object sender, EventArgs e)
        {
            //ChangeValueNameCode();
            LoadKPISession();

        }
        //private void LoadKPISession()
        //{
        //    var exp1 = new Expression(KPISessionModel_Enum.IsDeleted, 0);
        //    var exp2 = new Expression(KPISessionModel_Enum.DepartmentID, _departmentID);
        //    List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
        //    List<KPISessionModel> lst = SQLHelper<KPISessionModel>.fi.OrderByDescending(p => p.ID).ToList();
        //    cboKPISessionFrom.Properties.DataSource = cboKPISessionTo.Properties.DataSource = lst;
        //    cboKPISessionFrom.Properties.DisplayMember = "Code";
        //    cboKPISessionFrom.Properties.ValueMember = "ID";

        //    cboKPISessionTo.Properties.DisplayMember = "Code";
        //    cboKPISessionTo.Properties.ValueMember = "ID";
        //}

        private void LoadKPISession()
        {
            //int year = DateTime.Now.Year;
            //int quarter = (int)((DateTime.Now.Month + 2) / 3);
            //departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            var exp1 = new Expression(KPISessionModel_Enum.DepartmentID, _departmentID);
            var exp2 = new Expression(KPISessionModel_Enum.IsDeleted, 0);
            //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(p => p.QuarterEvaluation).ToList();
            cboKPISessionFrom.Properties.DataSource = lst;
            cboKPISessionFrom.Properties.DisplayMember = "Code";
            cboKPISessionFrom.Properties.ValueMember = "ID";

            cboKPISessionTo.Properties.DataSource = lst;
            cboKPISessionTo.Properties.DisplayMember = "Code";
            cboKPISessionTo.Properties.ValueMember = "ID";
            //KPISessionModel currentSession = lst.FirstOrDefault(p => p.YearEvaluation == year && p.QuarterEvaluation == quarter) ?? new KPISessionModel();
            //cboKPISession.EditValue = currentSession.ID;
            //LoadKpiExam();
        }


        private bool CheckValidate()
        {
            /* int year = TextUtils.ToInt(txtYear.Value);
             int quarter = TextUtils.ToInt(txtQuarter.Value);
             string code = txtCode.Text.Trim();
             string name = txtName.Text.Trim();
             if (ckbCopy.Checked)
             {
                 if (TextUtils.ToInt(cboKPISessionFrom.EditValue) <= 0)
                 {
                     MessageBox.Show("Vui lòng chọn dữ liệu Kỳ đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
             }
             if (year <= 0)
             {
                 MessageBox.Show($"Vui lòng nhập Năm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
             }
             if (quarter <= 0)
             {
                 MessageBox.Show($"Vui lòng nhập Quý!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
             }
             if (string.IsNullOrWhiteSpace(code))
             {
                 MessageBox.Show($"Vui lòng nhập Mã kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
             }
             if (string.IsNullOrWhiteSpace(name))
             {
                 MessageBox.Show($"Vui lòng nhập Tên kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
             }
             Expression ex4 = new Expression("YearEvaluation", year);
             Expression ex2 = new Expression("ID", kpiSession.ID, "<>");
             Expression e1 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartment.EditValue));
             Expression ex3 = new Expression("IsDeleted", 0);
             List<KPISessionModel> duplicate = SQLHelper<KPISessionModel>.FindByExpression(ex4.And(ex3.And(ex2).And(e1)));

             if (ckbCopy.Checked)
             {
                 if (duplicate.Any(p => p.QuarterEvaluation == quarter || p.Code == code))
                 {
                     if (MessageBox.Show($"Yếu tố đánh giá tại quý {quarter} năm {year} đã tồn tại! Bạn có muốn ghi đè dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
                 }
             }
             else
             {
                 if (duplicate.Any(p => p.QuarterEvaluation == quarter))
                 {
                     MessageBox.Show($"Quý [{quarter}] trong năm [{year}] đã có kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
                 if (duplicate.Any(p => p.Code == code))
                 {
                     MessageBox.Show($"Mã kỳ thi đã được sửa dụng trong năm [{year}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return false;
                 }
             }*/

            return true;
        }
        private bool SaveData()
        {
            /*  if (!CheckValidate()) return false;

              KPISessionModel model = SQLHelper<KPISessionModel>.FindByID(kpiSession.ID);
              model.YearEvaluation = TextUtils.ToInt(txtYear.Value);
              model.QuarterEvaluation = TextUtils.ToInt(txtQuarter.Value);
              model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
              model.Code = txtCode.Text.Trim();
              model.Name = txtName.Text.Trim();
              if (model.ID > 0)
              {
                  SQLHelper<KPISessionModel>.Update(model);
              }
              else
              {
                  model.ID = SQLHelper<KPISessionModel>.Insert(model).ID;
                  CreateAutoKPIExam(model);
                  CreateAutoKPIRule(model);
              }

              return true;
          }
          private void Reset()
          {
              txtCode.Clear();
              txtName.Clear();
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
              bool isSuccess = ckbCopy.Checked ? CopyData() : SaveData();
              if (isSuccess)
              {
                  kpiSession = new KPISessionModel();
                  Reset();
              }
          }

          private void btnSaveAndClose_Click(object sender, EventArgs e)
          {
              bool isSuccess = ckbCopy.Checked ? CopyData() : SaveData();
              if (isSuccess)
              {
                  this.DialogResult = DialogResult.OK;
              }
          }

          private void KPISessionDetails_FormClosing(object sender, FormClosingEventArgs e)
          {
              this.DialogResult = DialogResult.OK;
          }

          private void ChangeValueNameCode()
          {
              DepartmentModel department = (DepartmentModel)cboDepartment.GetSelectedDataRow() ?? new DepartmentModel();
              txtCode.Text = $"KPI_{TextUtils.ToString(department.Code).Trim()}_{txtYear.Value}_Q{txtQuarter.Value}";
              txtName.Text = $"Kỳ đánh giá KPI {TextUtils.ToString(department.Name).Trim()} quý {txtQuarter.Value}-{txtYear.Value}";
          }
          private void cboDepartment_EditValueChanged(object sender, EventArgs e)
          {
              ChangeValueNameCode();
          }
          private void txtYear_ValueChanged(object sender, EventArgs e)
          {
              ChangeValueNameCode();
          }

          private void txtQuarter_ValueChanged(object sender, EventArgs e)
          {
              ChangeValueNameCode();
          }
          private bool CopyData()
          {
              if (!CheckValidate()) return false;
              using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang sao chép dữ liệu..."))
              {
                  Expression e1 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartment.EditValue));
                  Expression e2 = new Expression("QuarterEvaluation", TextUtils.ToInt(txtQuarter.Value));
                  Expression e3 = new Expression("YearEvaluation", TextUtils.ToInt(txtYear.Value));
                  Expression e4 = new Expression("IsDeleted", 0);

                  //Xóa session cũ
                  List<KPISessionModel> lstOldSession = SQLHelper<KPISessionModel>.FindByExpression(e1.And(e2).And(e3).And(e4));
                  if (lstOldSession.Count > 0)
                  {
                      string lstOldSessionIDs = string.Join(",", lstOldSession.Select(p => p.ID));
                      Dictionary<string, object> newDict = new Dictionary<string, object>()
                      {
                          {KPISessionModel_Enum.IsDeleted.ToString(), 1},
                          {KPISessionModel_Enum.UpdatedBy.ToString(), Global.AppCodeName},
                          {KPISessionModel_Enum.UpdatedDate.ToString(), DateTime.Now}
                      };
                      SQLHelper<KPISessionModel>.UpdateFields(newDict, new Expression("ID", lstOldSessionIDs, "IN"));
                  }

                  // Insert phần tử mới
                  KPISessionModel model = new KPISessionModel();
                  model.YearEvaluation = TextUtils.ToInt(txtYear.Value);
                  model.QuarterEvaluation = TextUtils.ToInt(txtQuarter.Value);
                  model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
                  model.Code = txtCode.Text.Trim();
                  model.Name = txtName.Text.Trim();
                  model.ID = SQLHelper<KPISessionModel>.Insert(model).ID;


                  //Get các phần tử sao chép
                  Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboKPISessionFrom.EditValue));
                  Expression ex2 = new Expression("IsDeleted", 0);
                  List<KPIExamModel> lstExam = SQLHelper<KPIExamModel>.FindByExpression(ex1.And(ex2));

                  KPISessionModel sessionOld = (KPISessionModel)cboKPISessionFrom.GetSelectedDataRow() ?? new KPISessionModel();
                  string oldCode = $"{sessionOld.YearEvaluation}_Q{sessionOld.QuarterEvaluation}";
                  string oldName = $"Q{sessionOld.QuarterEvaluation}-{sessionOld.YearEvaluation}";

                  string newCode = $"{txtYear.Value}_Q{txtQuarter.Value}";
                  string newName = $"Q{txtQuarter.Value}-{txtYear.Value}";


                  string stt = "1.1".Remove("1.1".LastIndexOf("."));
                  foreach (KPIExamModel item in lstExam)
                  {
                      int oldId = item.ID;
                      item.KPISessionID = model.ID;
                      item.ID = 0;

                      item.ExamCode = item.ExamCode.Replace(oldCode, newCode);
                      item.ExamName = item.ExamName.Replace(oldName, newName);
                      item.ID = SQLHelper<KPIExamModel>.Insert(item).ID;

                      List<KPIExamPositionModel> listPosition = SQLHelper<KPIExamPositionModel>.FindByAttribute("KPIExamID", oldId);
                      foreach (KPIExamPositionModel position in listPosition)
                      {
                          position.KPIExamID = item.ID;
                          position.ID = 0;

                          SQLHelper<KPIExamPositionModel>.Insert(position);
                      }

                      //Get các phần tử sao chép!
                      List<KPIEvaluationFactorsModel> lstDetails = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                      new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                      new object[] { oldId, 0 }).OrderBy(p => p.ParentID).ToList();
                      foreach (KPIEvaluationFactorsModel detail in lstDetails)
                      {
                          //if (detail.STT == "1.1.1")
                          //{

                          //    int a = 1;
                          //}
                          detail.KPIExamID = item.ID;
                          detail.ID = 0;
                          if (detail.ParentID > 0)
                          {
                              //int indexST = detail.STT.LastIndexOf(".");
                              //string parentStt = detail.STT.Substring(0, indexST);
                              //KPIEvaluationFactorsModel parentModel = lstDetails.FirstOrDefault(p => p.STT == parentStt) ?? new KPIEvaluationFactorsModel();
                              //detail.ParentID = parentModel.ID;

                              var exp1 = new Expression(KPIEvaluationFactorsModel_Enum.STT.ToString(), detail.STT.Remove(detail.STT.LastIndexOf(".")));
                              var exp2 = new Expression(KPIEvaluationFactorsModel_Enum.KPIExamID.ToString(), item.ID);
                              var exp3 = new Expression(KPIEvaluationFactorsModel_Enum.EvaluationType.ToString(), detail.EvaluationType);
                              var exp4 = new Expression(KPIEvaluationFactorsModel_Enum.IsDeleted.ToString(), 0);

                              KPIEvaluationFactorsModel parent = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault() ?? new KPIEvaluationFactorsModel();
                              detail.ParentID = parent.ID;
                          }
                          detail.ID = SQLHelper<KPIEvaluationFactorsModel>.Insert(detail).ID;
                      }
                  };
                  CopyCriteria();
              }*/
            return true;
        }
        private bool CopyPositionEmPloyee()
        {
            //TN.Binh update 04/09/25
            if (cboKPISessionFrom.EditValue == null || cboKPISessionFrom.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn kỳ đánh giá muốn copy!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKPISessionFrom.Focus();
                return false;
            }
            if (cboKPISessionTo.EditValue == null || cboKPISessionTo.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn kỳ đánh giá muốn copy!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKPISessionTo.Focus();
                return false;
            }
            // Xoa du lieu cu
            Expression ex4 = new Expression(KPIPositionModel_Enum.KPISessionID, cboKPISessionTo.EditValue);
            List<KPIPositionModel> lstPositionOld = SQLHelper<KPIPositionModel>.FindByExpression(ex4);


            foreach (KPIPositionModel item in lstPositionOld)
            {
                //xoa mem
                // SQLHelper<KPIPositionModel>.DeleteByAttribute("KPISesionID", item.ID);
                var mydict = new Dictionary<string, object>() {
                    { KPIPositionModel_Enum.IsDeleted.ToString(), true },
                    {  KPIPositionModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    {  KPIPositionModel_Enum.UpdatedBy.ToString(), Global.LoginName }
                };
                SQLHelper<KPIPositionModel>.UpdateFieldsByID(mydict, item.ID);

                Expression ex5 = new Expression(KPIPositionEmployeeModel_Enum.KPIPosiotionID, item.ID);
                List<KPIPositionEmployeeModel> lstPositionEmployeeOld = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(ex5);

                var mydict2 = new Dictionary<string, object>() {
                    { KPIPositionEmployeeModel_Enum.IsDeleted.ToString(), true },
                    {  KPIPositionEmployeeModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    {  KPIPositionEmployeeModel_Enum.UpdatedBy.ToString(), Global.LoginName }
                };
                SQLHelper<KPIPositionEmployeeModel>.UpdateFieldsByID(mydict, item.ID);
            }


            // copy dữ liệu từ năm chọn sang quý và năm mới
            Expression ex1 = new Expression(KPIPositionModel_Enum.IsDeleted, 0);
            Expression ex2 = new Expression(KPIPositionModel_Enum.KPISessionID, TextUtils.ToInt(cboKPISessionFrom.EditValue));

            List<KPIPositionModel> lstData = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2));
            foreach (KPIPositionModel item in lstData)
            {
                //item.ID = 0;
                item.KPISessionID = TextUtils.ToInt(cboKPISessionTo.EditValue);
                item.CreatedBy = Global.LoginName;
                item.CreatedDate = DateTime.Now;

                int newID = SQLHelper<KPIPositionModel>.Insert(item).ID;

                //lưu nhân viên theo vị trí
                Expression ex3 = new Expression(KPIPositionEmployeeModel_Enum.KPIPosiotionID, item.ID);
                List<KPIPositionEmployeeModel> lstEmployee = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(ex3);
                if (lstEmployee.Count > 0)
                {
                    foreach (KPIPositionEmployeeModel itemEm in lstEmployee)
                    {
                        itemEm.KPIPosiotionID = newID;
                        itemEm.CreatedBy = Global.LoginName;
                        itemEm.CreatedDate = DateTime.Now;
                        SQLHelper<KPIPositionEmployeeModel>.Insert(itemEm);
                    }
                }
            }
            return true;
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CopyPositionEmPloyee())
            {
                MessageBox.Show("Copy thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Copy không thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (CopyPositionEmPloyee())
            {
                //MessageBox.Show("Copy thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Copy không thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
