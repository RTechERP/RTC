using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmWorkPlan : _Forms
    {

        public WorkPlanModel model = new WorkPlanModel();
        private int _rownIndex;
        ArrayList lstIDDelete = new ArrayList();

        DataSet dataSet = new DataSet();
        public frmWorkPlan()
        {
            InitializeComponent();
        }

        private void frmWorkPlan_Load(object sender, EventArgs e)
        {
            //Load dữ liệu từ thứ 2 đến chủ nhật theo ngày hiện tại trong tuần
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime sunday = monday.AddDays(6);
            dtpStartDate.Value = monday;
            dtpEndDate.Value = sunday;

            

            txtPageNumber.Text = "1";
            loadUser();
            cbUser.EditValue = Global.UserID;
            loadWorkPlan();
            

            //Load tên người đăng nhập lên cb Người phụ trách@
            

            loadProject();
        }

        /// <summary>
        /// Load dự án lên combobox
        /// </summary>
        void loadProject()
        {
            DataTable dt = TextUtils.LoadDataFromSP("sp_GetProjectToCombobox", "A", new string[] { }, new object[] { });
            cboPoject.DisplayMember = "ProjectCode";
            cboPoject.ValueMember = "ID";
            cboPoject.DataSource = dt;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            if (!checkValidate())
            {
                return;
            }
            if (saveWorkPlan())
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
                
        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select($"select ID, Code, FullName from Users where Status <> 1 AND ID = {Global.UserID}");
            cbUser.Properties.DataSource = cbUsers.DataSource = dt;
            cbUser.Properties.DisplayMember = cbUsers.DisplayMember= "FullName";
            cbUser.Properties.ValueMember = cbUsers.ValueMember = "ID";
        }
        void loadWorkPlan()
        {
            DateTime dateS = dtpStartDate.Value.AddDays(-1);
            dateS = new DateTime(dateS.Year, dateS.Month, dateS.Day, 23, 59, 59);
            DateTime dateE = dtpEndDate.Value.AddDays(+1);
            dateE = new DateTime(dateE.Year, dateE.Month, dateE.Day, 0, 0, 0);

            dataSet = TextUtils.LoadDataSetFromSP("spGetWorkPlanPaging"
                , new string[] { "@PageNumber", "@PageSize", "@StartDate", "@EndDate", "@UserID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateS, dateE, TextUtils.ToInt(cbUser.EditValue) });
            
            //if (dataSet.Tables[0].Rows.Count == 0) return;
            grdData.DataSource = dataSet.Tables[0];
            txtTotalPage.Text = TextUtils.ToString(dataSet.Tables[1].Rows[0]["TotalPage"]);
        }

        bool saveWorkPlan()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int IDmaster = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                WorkPlanModel master = new WorkPlanModel();
                if (IDmaster > 0)
                {
                    master = (WorkPlanModel)(WorkPlanBO.Instance.FindByPK(IDmaster));
                }
                //master.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                master.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                master.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                master.WorkContent = TextUtils.ToString(grvData.GetRowCellValue(i, colWorkContent));
                master.Location = TextUtils.ToString(grvData.GetRowCellValue(i, colLocation));
                master.StartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colStartDate));
                master.EndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEndDate));
                master.TotalDay = TextUtils.ToInt(grvData.GetRowCellValue(i, colTotalDay));
                master.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProject));
                if (IDmaster > 0)
                {
                    WorkPlanBO.Instance.Update(master);
                    
                }
                else
                {
                    master.ID = (int)WorkPlanBO.Instance.Insert(master);
                    grvData.SetRowCellValue(i, colID, master.ID);
                }

                for (int j = 1; j <= master.TotalDay; j++)
                {
                    WorkPlanDetailModel workPlanDetail = new WorkPlanDetailModel();

                    DateTime dateDay = new DateTime(master.StartDate.Value.Year, master.StartDate.Value.Month, master.StartDate.Value.Day).AddDays(+(j - 1));
                    workPlanDetail = (WorkPlanDetailModel)WorkPlanDetailBO.Instance.FindByCode("DateDay","UserID", dateDay.ToString("yyyy-MM-dd"), master.UserID);

                    if (workPlanDetail == null)
                    {
                        workPlanDetail = new WorkPlanDetailModel();
                        workPlanDetail.UserID = master.UserID;
                        workPlanDetail.DateDay = dateDay;
                        workPlanDetail.Location = master.Location;
                        workPlanDetail.WorkContent = master.WorkContent;
                        workPlanDetail.WorkPlanID = master.ID;
                        workPlanDetail.ProjectID = master.ProjectID;
                        workPlanDetail.STT = i + 1;

                        WorkPlanDetailBO.Instance.Insert(workPlanDetail);
                    }
                    else
                    {
                        workPlanDetail.UserID = master.UserID;
                        workPlanDetail.DateDay = dateDay;
                        workPlanDetail.Location = master.Location;
                        workPlanDetail.WorkContent = master.WorkContent;
                        workPlanDetail.WorkPlanID = master.ID;
                        workPlanDetail.ProjectID = master.ProjectID;
                        workPlanDetail.STT = i + 1;

                        WorkPlanDetailBO.Instance.Update(workPlanDetail);
                    }
                   
                    
                }
            }
            return true;
        }

        bool checkValidate()
        {
            DateTime? dsInput = TextUtils.ToDate4(grvData.GetFocusedRowCellValue(colStartDate));
            DateTime? deInput = TextUtils.ToDate4(grvData.GetFocusedRowCellValue(colEndDate));
            
            decimal totalDay = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colTotalDay));
            int userIDInput = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));

            if (totalDay <= 0)
            {
                MessageBox.Show("Tổng số ngày phải lớn hơn 0.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //DataTable dt = dataSet.Tables[2];

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DateTime? ds = TextUtils.ToDate4(dt.Rows[i]["StartDate"]);
            //    DateTime? de = TextUtils.ToDate4(dt.Rows[i]["EndDate"]);
            //    int userID  = TextUtils.ToInt(dt.Rows[i]["UserID"]);

            //    if (userIDInput == userID)
            //    {
            //        if (dsInput >= ds && dsInput <= de)
            //        {
            //            MessageBox.Show("Bạn đã có kế hoạch cho ngày " + dsInput.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return false;
            //        }

            //        if (deInput >= ds && deInput <= de)
            //        {
            //            MessageBox.Show("Bạn đã có kế hoạch cho ngày " + deInput.Value.ToString("dd/MM/yyyy") + "\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return false;
            //        }
            //    }
            //}

            for (int i = 0; i < grvData.RowCount; i++)
            {
                string content = TextUtils.ToString(grvData.GetRowCellValue(i,colWorkContent));
                int userID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));

                DateTime? dateS = TextUtils.ToDate4(grvData.GetRowCellValue(i, colStartDate));
                DateTime? dateE = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEndDate));

                if (string.IsNullOrEmpty(content))
                {
                    MessageBox.Show("Nội dung công việc không được để trống.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.FocusedRowHandle = i;
                    return false;
                }

                if (!dateS.HasValue)
                {
                    MessageBox.Show("Ngày bắt đầu không được để trống.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.FocusedRowHandle = i;
                    return false;
                }

                if (!dateE.HasValue)
                {
                    MessageBox.Show("Ngày kết thúc không được để trống.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.FocusedRowHandle = i;
                    return false;
                }

                if (userID <= 0)
                {
                    MessageBox.Show("Người phụ trách không được để trống.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvData.FocusedRowHandle = i;
                    return false;
                }

                //if (userIDInput == userID)
                //{
                //    if (dsInput >= dateS && deInput <= dateE)
                //    {
                //        MessageBox.Show("Bạn đã có kế hoạch cho ngày " + dsInput.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        grvData.FocusedRowHandle = i;
                //        return false;
                //    }
                //}
            }

            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadWorkPlan();
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    int STT;
                    DataTable dt = (DataTable)grdData.DataSource;
                    if(dt.Rows.Count == 0)
                    {
                        STT = 1; 
                    }  
                    else
                    {
                        STT = TextUtils.ToInt(grvData.GetRowCellValue(0, "STT")) + 1;
                    }    
                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = STT;
                    //dtrow["UserID"] = 1;
                    //dtrow["UserID"] = grvData.GetRowCellValue(0, colUserID);
                    dtrow["UserID"] = Global.UserID;
                    dt.Rows.InsertAt(dtrow, 0);
                    grdData.DataSource = dt;
                }
            }
        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colSTT || e.Column == colSTT && e.RowHandle >= 0)
            {
                e.Handled = true;
                grvData.ShowEditor();
                SearchLookUpEdit editor = grvData.ActiveEditor as SearchLookUpEdit;
                editor.ShowPopup();
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
                return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa bản ghi này không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                WorkPlanBO.Instance.Delete(ID);
                WorkPlanDetailBO.Instance.DeleteByAttribute("WorkPlanID", ID);

                if (ID > 0)
                {
                    lstIDDelete.Add(ID);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadWorkPlan();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadWorkPlan();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadWorkPlan();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadWorkPlan();
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colStartDate || e.Column == colEndDate)
                {
                    string strDateS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                    string strDateE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colEndDate));

                    if (string.IsNullOrEmpty(strDateE) || string.IsNullOrEmpty(strDateS))
                    {
                        return;
                    }

                    DateTime? dsInput = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                    DateTime? deInput = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colEndDate));

                    
                    DateTime startDate = DateTime.Parse(strDateS);
                    DateTime endDate = DateTime.Parse(strDateE);

                    //int userIDInput = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colUserID));

                    //DataTable dt = dataSet.Tables[2];

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    DateTime? ds = TextUtils.ToDate4(dt.Rows[i]["StartDate"]);
                    //    DateTime? de = TextUtils.ToDate4(dt.Rows[i]["EndDate"]);
                    //    int userID = TextUtils.ToInt(dt.Rows[i]["UserID"]);

                    //    if (userIDInput == userID)
                    //    {
                    //        if (dsInput >= ds && dsInput <= de)
                    //        {
                    //            MessageBox.Show("Bạn đã có kế hoạch cho ngày " + dsInput.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            grvData.SetRowCellValue(e.RowHandle, colTotalDay, "");
                    //            return;
                    //        }

                    //        if (deInput >= ds && deInput <= de)
                    //        {
                    //            MessageBox.Show("Bạn đã có kế hoạch cho ngày " + deInput.Value.ToString("dd/MM/yyyy") + "\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            grvData.SetRowCellValue(e.RowHandle, colTotalDay, "");
                    //            return;
                    //        }
                    //    }
                    //}

                    TimeSpan totalDay = endDate - startDate;
                    grvData.SetRowCellValue(e.RowHandle, colTotalDay, totalDay.TotalDays + 1);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                STT = TextUtils.ToInt(grvData.GetRowCellValue(0, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            //dtrow["UserID"] = grvData.GetRowCellValue(0, colUserID);
            dtrow["UserID"] = Global.UserID;
            dt.Rows.InsertAt(dtrow,0);
            grdData.DataSource = dt;
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadWorkPlan();
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvData.FocusedRowHandle >= 0)
            {
                int userIdInput = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));

                if (grvData.FocusedColumn == colStartDate)
                {
                    DateTime? dsInput = TextUtils.ToDate4(e.Value);

                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        DateTime? dateStart = null;
                        DateTime? dateEnd = null;
                        int userID = 0;
                        if (i != grvData.FocusedRowHandle)
                        {
                            dateStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, colStartDate));
                            dateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEndDate));
                            userID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                        }

                        if (userIdInput == userID)
                        {
                            if (dsInput >= dateStart && dsInput <= dateEnd)
                            {
                                e.Valid = false;
                                e.ErrorText = "Bạn đã có kế hoạch cho ngày " + dsInput.Value.ToString("dd/MM/yyyy");
                                return;
                            }
                        }
                        
                    }
                    if (dsInput < dtpStartDate.Value || dsInput > dtpEndDate.Value)
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày bắt đầu không được nằm ngoài " + dtpStartDate.Value.ToString("dd/MM/yyyy") + " và " + dtpEndDate.Value.ToString("dd/MM/yyyy");
                        return;
                    }
                    
                }

                if (grvData.FocusedColumn == colEndDate)
                {
                    DateTime? deInput = TextUtils.ToDate4(e.Value);

                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        DateTime? dateStart = null;
                        DateTime? dateEnd = null;
                        int userID = 0;

                        if (i != grvData.FocusedRowHandle)
                        {
                            dateStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, colStartDate));
                            dateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEndDate));
                            userID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                        }

                        if (userIdInput == userID)
                        {
                            if (deInput >= dateStart && deInput <= dateEnd)
                            {
                                e.Valid = false;
                                e.ErrorText = "Bạn đã có kế hoạch cho ngày " + deInput.Value.ToString("dd/MM/yyyy");
                                return;
                            }
                        }
                    }

                    if (deInput < dtpStartDate.Value || deInput > dtpEndDate.Value)
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày kết thúc không được nằm ngoài " + dtpStartDate.Value.ToString("dd/MM/yyyy") + " và " + dtpEndDate.Value.ToString("dd/MM/yyyy");
                        return;
                    }
                }

                if (grvData.FocusedColumn == colUserID)
                {
                    if (TextUtils.ToInt(e.Value) <= 0)
                    {
                        e.Valid = false;
                        e.ErrorText = "Vui lòng chọn người phụ trách!";
                        return;
                    }
                }
            }
        }
    }
}
