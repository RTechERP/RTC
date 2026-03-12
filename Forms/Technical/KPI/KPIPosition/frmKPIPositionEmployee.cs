using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors.Repository;
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
    public partial class frmKPIPositionEmployee : _Forms
    {
        public string deName;
        int _departmentID = 0;
        public int kpiSessionID = 0; //TN.Binh update 10/09/25
        public frmKPIPositionEmployee(int departmentID)
        {
            InitializeComponent();
            _departmentID = departmentID;
        }

        private void frmKPIPositionEmployee_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadDepartMentNew();
            LoadKPISession();
            LoadDataPositionNew();
        }

        //private void LoadDataPosition()
        //{
        //    Expression ex1 = new Expression("IsDeleted", 0);
        //    Expression ex2 = new Expression("ID", 1, "<>");
        //    List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2));
        //    grdMaster.DataSource = lst;
        //    LoadDetails();
        //}
        private void LoadDetails()
        {
            int PositionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPositionID));
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetEmployeeByKPIPositionID", new string[] { "@KPIPositionID" }, new object[] { PositionID });
            grdDetails.DataSource = dt;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            int rowHandled = grvMaster.FocusedRowHandle;
            frmKPIPositionDetails frm = new frmKPIPositionDetails(_departmentID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataPositionNew();
                grvMaster.FocusedRowHandle = rowHandled;
            }
        }

        private void btnUpdatePosition_Click(object sender, EventArgs e)
        {
            int rowHandled = grvMaster.FocusedRowHandle;
            int positionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPositionID));
            frmKPIPositionDetails frm = new frmKPIPositionDetails(_departmentID);
            frm.kpiPosition = SQLHelper<KPIPositionModel>.FindByID(positionID);

            frm.kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue); //TN.Binh update 03/09/25
            //frm.depID = TextUtils.ToInt(cboDepartment.EditValue); //TN.Binh update 04/09/25

            if (frm.kpiPosition.ID <= 0) return;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataPositionNew();
                grvMaster.FocusedRowHandle = rowHandled;
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnUpdatePosition_Click(null, null);
        }

        private void btnDeletePosition_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPositionCode));
            int positionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPositionID));
            KPIPositionModel model = SQLHelper<KPIPositionModel>.FindByID(positionID);
            if (model.ID <= 0) return;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa vị trí [{0}] hay không ?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                model.IsDeleted = true;
                SQLHelper<KPIPositionModel>.Update(model);
                LoadDataPositionNew();
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0) || Global.EmployeeID == 55; //C NGân
            int departmentID = Global.DepartmentID;
            if (isAdmin) departmentID = 0;

            int positionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPositionID));
            if (positionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn vị trí cần thêm nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int kpiSessionIDSelected = TextUtils.ToInt(cboKPISession.EditValue);
            frmKPIPositionEmployeeDetails frm = new frmKPIPositionEmployeeDetails(_departmentID, kpiSessionIDSelected);
            frm.KPIPostionID = positionID;
            //frm.departmentID = departmentID;
            //frm.kpiSessionID = kpiSessionID; // TNBinh update 08/09/25
            if (frm.ShowDialog() == DialogResult.OK) LoadDetails();
        }

        private void grvDetails_DoubleClick(object sender, EventArgs e)
        {
            btnAddEmployee_Click(null, null);
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvDetails.GetSelectedRows();


            if (rowSelected.Count() <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên cần xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ID = 0;
            //string fullName = "";
            //string positionName = "";
            int departmentID = 0;
            bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0) || Global.EmployeeID == 55; //C NGân
            foreach (int row in rowSelected)
            {
                //ID = TextUtils.ToInt(grvDetails.GetRowCellValue(row, colKPIPositionEmployeeID));
                //fullName = TextUtils.ToString(grvDetails.GetRowCellValue(row, colEmpFullName));
                //positionName = TextUtils.ToString(grvMaster.GetRowCellValue(row, colPositionName));
                departmentID = TextUtils.ToInt(grvDetails.GetRowCellValue(row, colDepartmentID));

                if (departmentID != Global.DepartmentID && !isAdmin)
                {
                    MessageBox.Show("Bạn không thể xóa nhân viên của phòng ban khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa danh sách nhân viên đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in rowSelected)
                {
                    ID = TextUtils.ToInt(grvDetails.GetRowCellValue(row, colKPIPositionEmployeeID));
                    //fullName = TextUtils.ToString(grvDetails.GetRowCellValue(row, colEmpFullName));
                    //positionName = TextUtils.ToString(grvMaster.GetRowCellValue(row, colPositionName));
                    //departmentID = TextUtils.ToInt(grvDetails.GetRowCellValue(row, colDepartmentID));

                    //if (departmentID != Global.DepartmentID && !isAdmin)
                    //{
                    //    MessageBox.Show("Bạn không thể xóa nhân viên của phòng ban khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    SQLHelper<KPIPositionEmployeeModel>.DeleteModelByID(ID);
                }

                LoadDetails();
            }



            //if (ID <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Nhân viên cần xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (MessageBox.Show($"Bạn có chắc muốn xóa Nhân viên [{fullName}] khỏi vị trí [{positionName}]", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    SQLHelper<KPIPositionEmployeeModel>.DeleteModelByID(ID);
            //    
            //}
        }



        #region TN.Bình Update 17/09
        //TN.Binh update 03/09/25 
        private void LoadDepartMentNew()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.DataSource = lst;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";

            cboDepartment.EditValue = _departmentID;
        }

        private void LoadKPISession()
        {
            Expression ex1 = new Expression("IsDeleted", 0);
            Expression ex2 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartment.EditValue));
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2)).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.Properties.DisplayMember = "Name";
        }

        private void LoadDataPositionNew()
        {

            //Expression ex1 = new Expression("IsDeleted", 0);
            //Expression ex2 = new Expression("ID", 1, "<>");
            //Expression ex3 = new Expression("KPISessionID", TextUtils.ToInt(cboKPISession.EditValue));
            //Expression ex4 = new Expression("KPISessionID", 0);
            //List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2).And(ex3.Or(ex4)));

            //RepositoryItemLookUpEdit repo = new RepositoryItemLookUpEdit();
            //repo.DataSource = new[]
            //{
            //    new { ID = 1, Name = "Kỹ thuật, Pro" },
            //    new { ID = 3, Name = "Senior" },
            //    new { ID = 4, Name = "Phó phòng" },
            //    new { ID = 2, Name = "Admin" },
            //};
            //repo.DisplayMember = "Name";   // Cột hiển thị
            //repo.ValueMember = "ID";       // Cột dữ liệu thực tế (TypePosition)

            //// Gán cho cột Loại vị trí trong grid
            //grvMaster.Columns["TypePosition"].ColumnEdit = repo;
            //grdMaster.DataSource = lst;



            int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIPosition", "spGetKPIPosition", new string[] { "@KPISessionID" }, new object[] { kpiSessionID });
            grdMaster.DataSource = dt;
            LoadDetails();
        }

        #endregion

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //========================= lee min khooi update 08/10/2024 =====================
            //rowHandleMaster = grvMaster.FocusedRowHandle;
            //int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);

            frmKPIPositionCopy frm = new frmKPIPositionCopy(_departmentID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataPositionNew();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataPositionNew();
        }

        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataPositionNew();
        }
    }
}
