using BMS;
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

namespace Forms.Employee.TeamPhongBan
{
    public partial class frmEmployeeTeam : _Forms
    {
        List<EmployeeTeamModel> _list = SQLHelper<EmployeeTeamModel>.FindAll();
        public frmEmployeeTeam()
        {
            InitializeComponent();
            loadTeam();
        }
        void loadTeam()
        {
            DataTable dt = TextUtils.GetTable("spGetEmployeeTeam");
            grdTeam.DataSource = dt;
            //grvTeam.Columns["DepartmentName"].Group();
            //grvTeam.Columns["DepartmentName"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //grvTeam.Columns["STT"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            //grvTeam.ExpandAllGroups();
        }

        private void frmTeamPhongBan_Load(object sender, EventArgs e)
        {
            loadTeam();
        }
        Task SaveEventCallBack(EmployeeTeamModel arg)
        {
            this.Invoke(new Action(() =>
            {
                EmployeeTeamModel model = _list.FirstOrDefault(o => o.ID == arg.ID);
                if (model == null)
                {
                    _list.Add(arg);
                    
                }
                else
                {
                    model.Name = arg.Name;
                    model.Code = arg.Code;
                    model.DepartmentID = arg.DepartmentID;
                    model.STT = arg.STT;
                    model.UpdatedBy = arg.UpdatedBy;
                    model.UpdatedDate = arg.UpdatedDate;
                    model.CreatedBy = arg.CreatedBy;
                    model.CreatedBy = arg.CreatedBy;

                }
            }));
            loadTeam();
            return Task.CompletedTask;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeeTeamDetail frm = new frmEmployeeTeamDetail();
            frm.SaveEvent += SaveEventCallBack;
            int maxSTT = _list.Any() ? _list.Max(x => x.STT ?? 1) : 0;
            frm.team.STT = maxSTT + 1;
            frm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvTeam.GetFocusedRowCellValue("ID"));
            EmployeeTeamModel model = SQLHelper<EmployeeTeamModel>.FindByID(id);
            if (model.ID <= 0)
            {
                //TextUtils.ShowError("Vui lòng chọn 1 team để sửa!");    
                return;
            }
            frmEmployeeTeamDetail frm = new frmEmployeeTeamDetail();
            frm.team = model;
            frm.SaveEvent += SaveEventCallBack;
            frm.Show();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvTeam.GetFocusedRowCellValue("ID"));
            EmployeeTeamModel model = SQLHelper<EmployeeTeamModel>.FindByID(id);
            if (model.ID <= 0)
            {
                TextUtils.ShowError("Vui lòng chọn 1 team để xóa!");
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa [" + model.Name + "] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                Dictionary<string,object> fieldValues = new Dictionary<string, object>
                    {
                        { "IsDeleted", 1 }
                    };
                SQLHelper<EmployeeTeamModel>.UpdateFieldsByID(fieldValues, id);
                loadTeam();
            }
        }

        private void grvTeam_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}