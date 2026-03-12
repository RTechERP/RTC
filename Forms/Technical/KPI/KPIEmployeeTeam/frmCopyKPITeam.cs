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
    public partial class frmCopyKPITeam : _Forms
    {
        public Action SaveEvent;
        public frmCopyKPITeam()
        {
            InitializeComponent();

        }
        void LoadData()
        {
            int currentYear = DateTime.Now.Year;
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;


            txtYearFrom.Value = currentYear;
            txtYearTo.Value = currentYear;
            txtQuarterTo.Value = currentQuarter;
        }
        private void frmCopyKPITeam_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            LoadData();
        }
  //      private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
  //      {
  //          int oldQuarter = TextUtils.ToInt(txtQuarterFrom.Value);
		//int newQuarter = TextUtils.ToInt(txtQuarterTo.Value);
		//int oldYear = TextUtils.ToInt(txtYearFrom.Value);
		//int newYear = TextUtils.ToInt(txtYearTo.Value);
		//int? departmentID = TextUtils.ToInt(cboDepartment.EditValue);

		//if (oldQuarter == newQuarter && oldYear == newYear)
		//{
		//	MessageBox.Show("Quý/Năm mới phải khác Quý/Năm cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//	return;
		//}
		//if (oldQuarter > newQuarter && oldYear >= newYear)
		//{
		//	MessageBox.Show("Quý/Năm mới phải lớn hơn Quý/Năm cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//	return;
		//}

		//Expression exp = new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, oldQuarter)
		//				.And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, oldYear))
		//				.And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));
		//Expression exp1 = new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, newQuarter)
		//				.And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, newYear))
		//				.And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));

		//if (departmentID > 0)
		//{
		//	Expression exp2 = new Expression(KPIEmployeeTeamModel_Enum.DepartmentID, departmentID);
		//	exp = exp.And(exp2);
		//	exp1 = exp1.And(exp2);
		//}

		//List<KPIEmployeeTeamModel> lstTeam = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(exp);
		//List<KPIEmployeeTeamModel> lstExistTeam = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(exp1);

		//if (!lstTeam.Any())
		//{
		//	MessageBox.Show("Không tìm thấy team gốc để copy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//	return;
		//}

		//if (lstExistTeam.Count > 0)
		//{
		//	DialogResult dialog = MessageBox.Show(
		//	  "Đã tồn tại Team ở Quý/Năm mới.\nBạn có muốn tiếp tục copy không?",
		//	  "Thông báo",
		//	  MessageBoxButtons.YesNo,
		//	  MessageBoxIcon.Question
		//	);

		//	if (dialog == DialogResult.No) return;
		//}

		//// Lấy toàn bộ cây team cần copy
		//List<KPIEmployeeTeamModel> allTeamsToCopy = GetTeamHierarchy(lstTeam, oldQuarter, oldYear);


		//// Mapping oldID -> newID
		//Dictionary<int, int> teamIdMapping = new Dictionary<int, int>();

		//// Insert team mới với ParentID tạm thời = 0
		//foreach (var oldTeam in allTeamsToCopy)
		//{
		//	KPIEmployeeTeamModel newTeam = new KPIEmployeeTeamModel
		//	{
		//		Name = oldTeam.Name,
		//		DepartmentID = oldTeam.DepartmentID,
		//		QuarterValue = newQuarter,
		//		YearValue = newYear,
		//		LeaderID = oldTeam.LeaderID,
		//		ParentID = 0,
		//		CreatedDate = DateTime.Now,
		//		CreatedBy = Global.AppUserName,
		//		IsDeleted = false
		//	};

		//	int newTeamID = SQLHelper<KPIEmployeeTeamModel>.Insert(newTeam).ID;
		//	teamIdMapping.Add(oldTeam.ID, newTeamID);

		//	// Copy chi tiết nhân viên
		//	CopyTeamDetails(oldTeam.ID, newTeamID);
		//}

		//// Cập nhật lại ParentID
		//foreach (var oldTeam in allTeamsToCopy)
		//{
		//	if (!teamIdMapping.ContainsKey(oldTeam.ID)) continue;

		//	int newTeamId = teamIdMapping[oldTeam.ID];
		//	KPIEmployeeTeamModel newTeam = SQLHelper<KPIEmployeeTeamModel>.FindByID(newTeamId);

		//	if (oldTeam.ParentID < 0)
		//	{
		//		// Vẫn gán về phòng ban
		//		newTeam.ParentID = oldTeam.ParentID;
		//	}
		//	else if (oldTeam.ParentID.HasValue && teamIdMapping.ContainsKey(oldTeam.ParentID.Value))
		//	{
		//		newTeam.ParentID = teamIdMapping[oldTeam.ParentID.Value];
		//	}

		//	SQLHelper<KPIEmployeeTeamModel>.Update(newTeam);
		//}

		//// Xóa các team + detail cũ (nếu có)
		//DeleteExistingTeams(lstExistTeam);

		//SaveEvent?.Invoke();
		//MessageBox.Show("Copy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
  //      }


  //      private List<KPIEmployeeTeamModel> GetTeamHierarchy(List<KPIEmployeeTeamModel> rootTeams, int quarter, int year)
  //      {
  //          if (!rootTeams.Any()) return rootTeams;

  //          var result = new List<KPIEmployeeTeamModel>(rootTeams);
  //          var queue = new Queue<int>();
  //          foreach (var team in rootTeams)
  //          {
  //              queue.Enqueue(team.ID);
  //          }
  //          while (queue.Count > 0)
  //          {
  //              var parentId = queue.Dequeue();
  //              Expression childExp = new Expression(KPIEmployeeTeamModel_Enum.ParentID, parentId)
  //                                   .And(new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, quarter))
  //                                   .And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, year))
  //                                   .And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));

  //              var children = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(childExp);

  //              foreach (var child in children)
  //              {
  //                  if (!result.Any(r => r.ID == child.ID)) // Avoid duplicates
  //                  {
  //                      result.Add(child);
  //                      queue.Enqueue(child.ID);
  //                  }
  //              }
  //          }

  //          return result;
  //      }

  //      private void CopyTeamDetails(int oldTeamID, int newTeamID)
  //      {
  //          List<KPIEmployeeTeamLinkModel> lstDetail = SQLHelper<KPIEmployeeTeamLinkModel>.FindByExpression(
  //              new Expression(KPIEmployeeTeamLinkModel_Enum.KPIEmployeeTeamID, oldTeamID)
  //              .And(new Expression(KPIEmployeeTeamLinkModel_Enum.IsDeleted, 0))
  //          );

  //          if (lstDetail.Any())
  //          {
  //              var newDetails = lstDetail.Select(d => new KPIEmployeeTeamLinkModel
  //              {
  //                  KPIEmployeeTeamID = newTeamID,
  //                  EmployeeID = d.EmployeeID,
  //                  CreatedDate = DateTime.Now,
  //                  CreatedBy = Global.AppUserName,
  //                  IsDeleted = false
  //              }).ToList();

  //              SQLHelper<KPIEmployeeTeamLinkModel>.InsertRange(newDetails);
  //          }
  //      }

  //      private void DeleteExistingTeams(List<KPIEmployeeTeamModel> lstExistTeam)
  //      {
  //          foreach (var team in lstExistTeam)
  //          {
  //              team.IsDeleted = true;
  //              team.UpdatedDate = DateTime.Now;
  //              team.UpdatedBy = Global.AppUserName;

  //              List<KPIEmployeeTeamLinkModel> lstDetail = SQLHelper<KPIEmployeeTeamLinkModel>.FindByExpression(
  //                  new Expression(KPIEmployeeTeamLinkModel_Enum.KPIEmployeeTeamID, team.ID)
  //                  .And(new Expression(KPIEmployeeTeamLinkModel_Enum.IsDeleted, 0))
  //              );

  //              foreach (var detail in lstDetail)
  //              {
  //                  detail.IsDeleted = true;
  //                  detail.UpdatedDate = DateTime.Now;
  //                  detail.UpdatedBy = Global.AppUserName;
  //              }

  //              if (lstDetail.Any())
  //                  SQLHelper<KPIEmployeeTeamLinkModel>.UpdateRange(lstDetail);
  //          }
  //          SQLHelper<KPIEmployeeTeamModel>.UpdateRange(lstExistTeam);
  //      }


        void LoadDepartment()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = lst;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
        }


        #region Nhật update copy
        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int oldQuarter = TextUtils.ToInt(txtQuarterFrom.Value);
            int newQuarter = TextUtils.ToInt(txtQuarterTo.Value);
            int oldYear = TextUtils.ToInt(txtYearFrom.Value);
            int newYear = TextUtils.ToInt(txtYearTo.Value);
            int? departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            if (oldQuarter == newQuarter && oldYear == newYear)
            {
                MessageBox.Show("Quý/Năm mới phải khác Quý/Năm cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (oldQuarter > newQuarter && oldYear >= newYear)
            {
                MessageBox.Show("Quý/Năm mới phải lớn hơn Quý/Năm cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Expression exp = new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, oldQuarter)
                            .And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, oldYear))
                            .And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));
            Expression exp1 = new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, newQuarter)
                            .And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, newYear))
                            .And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));

            if (departmentID > 0)
            {
                Expression exp2 = new Expression(KPIEmployeeTeamModel_Enum.DepartmentID, departmentID);
                exp = exp.And(exp2);
                exp1 = exp1.And(exp2);
            }

            List<KPIEmployeeTeamModel> lstTeam = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(exp);
            List<KPIEmployeeTeamModel> lstExistTeam = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(exp1);

            if (!lstTeam.Any())
            {
                MessageBox.Show("Không tìm thấy team gốc để copy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lstExistTeam.Count > 0)
            {
                DialogResult dialog = MessageBox.Show(
                  "Đã tồn tại Team ở Quý/Năm mới.\nBạn có muốn tiếp tục copy không?",
                  "Thông báo",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question
                );

                if (dialog == DialogResult.No) return;
            }

            // Lấy toàn bộ cây team cần copy
            List<KPIEmployeeTeamModel> allTeamsToCopy = GetTeamHierarchy(lstTeam, oldQuarter, oldYear);


            // Mapping oldID -> newID
            Dictionary<int, int> teamIdMapping = new Dictionary<int, int>();

            // Insert team mới với ParentID tạm thời = 0
            foreach (var oldTeam in allTeamsToCopy)
            {
                KPIEmployeeTeamModel newTeam = new KPIEmployeeTeamModel
                {
                    Name = oldTeam.Name,
                    DepartmentID = oldTeam.DepartmentID,
                    QuarterValue = newQuarter,
                    YearValue = newYear,
                    LeaderID = oldTeam.LeaderID,
                    ParentID = 0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Global.AppUserName,
                    IsDeleted = false
                };

                int newTeamID = SQLHelper<KPIEmployeeTeamModel>.Insert(newTeam).ID;
                teamIdMapping.Add(oldTeam.ID, newTeamID);

                // Copy chi tiết nhân viên
                CopyTeamDetails(oldTeam.ID, newTeamID);
            }

            // Cập nhật lại ParentID
            foreach (var oldTeam in allTeamsToCopy)
            {
                if (!teamIdMapping.ContainsKey(oldTeam.ID)) continue;

                int newTeamId = teamIdMapping[oldTeam.ID];
                KPIEmployeeTeamModel newTeam = SQLHelper<KPIEmployeeTeamModel>.FindByID(newTeamId);

                if (oldTeam.ParentID < 0)
                {
                    // Vẫn gán về phòng ban
                    newTeam.ParentID = oldTeam.ParentID;
                }
                else if (oldTeam.ParentID.HasValue && teamIdMapping.ContainsKey(oldTeam.ParentID.Value))
                {
                    newTeam.ParentID = teamIdMapping[oldTeam.ParentID.Value];
                }

                SQLHelper<KPIEmployeeTeamModel>.Update(newTeam);
            }

            // Xóa các team + detail cũ (nếu có)
            DeleteExistingTeams(lstExistTeam);

            SaveEvent?.Invoke();
            MessageBox.Show("Copy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private List<KPIEmployeeTeamModel> GetTeamHierarchy(List<KPIEmployeeTeamModel> selectedTeams, int quarter, int year)
        {
            var result = new List<KPIEmployeeTeamModel>();

            if (!selectedTeams.Any()) return result;

            // ===== Lấy tất cả descendants (con, cháu, …) =====
            var queue = new Queue<int>(selectedTeams.Select(t => t.ID));
            result.AddRange(selectedTeams);

            while (queue.Count > 0)
            {
                int parentId = queue.Dequeue();

                Expression childExp = new Expression(KPIEmployeeTeamModel_Enum.ParentID, parentId)
                                     .And(new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, quarter))
                                     .And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, year))
                                     .And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));

                var children = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(childExp);

                foreach (var child in children)
                {
                    if (!result.Any(r => r.ID == child.ID))
                    {
                        result.Add(child);
                        queue.Enqueue(child.ID);
                    }
                }
            }

            // ===== Lấy tất cả ancestors (cha, ông, …) =====
            var stack = new Stack<int>(selectedTeams.Select(t => t.ParentID ?? 0));

            while (stack.Count > 0)
            {
                int parentId = stack.Pop();
                if (parentId <= 0) continue; // âm = phòng ban, bỏ qua

                Expression parentExp = new Expression(KPIEmployeeTeamModel_Enum.ID, parentId)
                                      .And(new Expression(KPIEmployeeTeamModel_Enum.QuarterValue, quarter))
                                      .And(new Expression(KPIEmployeeTeamModel_Enum.YearValue, year))
                                      .And(new Expression(KPIEmployeeTeamModel_Enum.IsDeleted, 0));

                var parents = SQLHelper<KPIEmployeeTeamModel>.FindByExpression(parentExp);

                foreach (var parent in parents)
                {
                    if (!result.Any(r => r.ID == parent.ID))
                    {
                        result.Add(parent);
                        stack.Push(parent.ParentID ?? 0);
                    }
                }
            }

            return result;
        }


        private void CopyTeamDetails(int oldTeamID, int newTeamID)
        {
            List<KPIEmployeeTeamLinkModel> lstDetail = SQLHelper<KPIEmployeeTeamLinkModel>.FindByExpression(
                new Expression(KPIEmployeeTeamLinkModel_Enum.KPIEmployeeTeamID, oldTeamID)
                .And(new Expression(KPIEmployeeTeamLinkModel_Enum.IsDeleted, 0))
            );

            if (lstDetail.Any())
            {
                var newDetails = lstDetail.Select(d => new KPIEmployeeTeamLinkModel
                {
                    KPIEmployeeTeamID = newTeamID,
                    EmployeeID = d.EmployeeID,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Global.AppUserName,
                    IsDeleted = false
                }).ToList();

                SQLHelper<KPIEmployeeTeamLinkModel>.InsertRange(newDetails);
            }
        }

        private void DeleteExistingTeams(List<KPIEmployeeTeamModel> lstExistTeam)
        {
            foreach (var team in lstExistTeam)
            {
                team.IsDeleted = true;
                team.UpdatedDate = DateTime.Now;
                team.UpdatedBy = Global.AppUserName;

                List<KPIEmployeeTeamLinkModel> lstDetail = SQLHelper<KPIEmployeeTeamLinkModel>.FindByExpression(
                    new Expression(KPIEmployeeTeamLinkModel_Enum.KPIEmployeeTeamID, team.ID)
                    .And(new Expression(KPIEmployeeTeamLinkModel_Enum.IsDeleted, 0))
                );

                foreach (var detail in lstDetail)
                {
                    detail.IsDeleted = true;
                    detail.UpdatedDate = DateTime.Now;
                    detail.UpdatedBy = Global.AppUserName;
                }

                if (lstDetail.Any())
                    SQLHelper<KPIEmployeeTeamLinkModel>.UpdateRange(lstDetail);
            }
            SQLHelper<KPIEmployeeTeamModel>.UpdateRange(lstExistTeam);
        }
        #endregion
    }
}
