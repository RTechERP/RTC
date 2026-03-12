using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using static Forms.Classes.cGlobVar;
using DevExpress.Utils;
using System.Net;
using System.IO;
using DevExpress.XtraRichEdit.Import.Html;
using System.Diagnostics;

namespace Forms.Personal
{
    public partial class frmApprovedTP : _Forms
    {
        bool _isSenior = false;
        int StatusApprove, IDApproved;
        //string[] arrParamName = null;
        //object[] arrParamValue = null;
        DataTable dtData = new DataTable();
        public frmApprovedTP(bool isSenior)
        {
            InitializeComponent();
            _isSenior = isSenior;

            btnUnapproved.Visible = btnApproved.Visible = !_isSenior;
            btnUnApprovedSenior.Visible = btnSeniorApprove.Visible = isSenior;
        }

        private void frmApprovedTP_Load(object sender, EventArgs e)
        {
            DateTime firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpStart.Value = DateTime.Now.AddDays(-7);
            dtpEnd.Value = firstDate.AddMonths(+1).AddDays(-1);
            loadDepartment();
            IDApproved = !_isSenior ? Global.EmployeeID : 0;//người duyệt
            cbApprovedStatusTBP.SelectedIndex = 1;//chưa duyệt
            cboDeleteFlag.SelectedIndex = 1;//chưa xoá
            cboType.SelectedIndex = !_isSenior ? 0 : 3;
            LoadEmployee();
            loadUserTeam(); //VTN Update 14725
            //LoadType();


            cbApprovedStatusHR.SelectedIndex = 0;
            cbApprovedStatusBGD.SelectedIndex = 0;

            //if (Global.DepartmentID == 1 && Global.EmployeeID != 54)
            //{
            //IDApproved = 0;

            //cbApprovedStatusTBP.SelectedIndex = 2;
            //cbApprovedStatusHR.SelectedIndex = 2;
            //cbApprovedStatusBGD.SelectedIndex = 1;
            //}

            btnApprovedBGD.Visible = btnUnApprovedBGD.Visible = (Global.DepartmentID == 1 && Global.EmployeeID != 54) || Global.IsAdmin;

            loadData();
        }

        void LoadEmployee()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            //cboEmployee.Properties.DisplayMember = "FullName";
            //cboEmployee.Properties.ValueMember = "ID";
            //cboEmployee.Properties.DataSource = dt;


            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            int kpiemployeeID = TextUtils.ToInt(cboTeam.EditValue);
            //int currentYear = dtpEnd.Value.Year;
            //int currentQuarter = (dtpEnd.Value.Month - 1) / 3 + 1;
            //ndnhat update 03/10/2025
            int currentYear = DateTime.Now.Year;
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            int departmentID = kpiemployeeID < 0 ? Global.DepartmentID : 0;

            DataTable dt;
            if (kpiemployeeID != 0)
            {

                dt = TextUtils.LoadDataFromSP("spGetKPIEmployeeTeamLink_New", "A", new string[] { "@KPIEmployeeteamID", "@DepartmentID", "@YearValue", "@QuarterValue" }, new object[] { kpiemployeeID, departmentID, currentYear, currentQuarter });
            }
            else
            {
                dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            }

            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "EmployeeID";
            //end ndnhat update 03/10/2025
            cboEmployee.Properties.DataSource = dt;
        }

        void LoadType()
        {
            var data = dtData.AsEnumerable().OrderBy(x => x["TType"]).Select(x => x["TypeText"]).ToArray();
            cboType.Items.AddRange(data);
        }


        void loadData()
        {
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;

            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int deleteFlag = cboDeleteFlag.SelectedIndex - 1;

            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);
            int employee = TextUtils.ToInt(cboEmployee.EditValue);
            int ttype = cboType.SelectedIndex;
            int statusHR = cbApprovedStatusHR.SelectedIndex - 1;
            int statusBGD = cbApprovedStatusBGD.SelectedIndex - 1;
            int userTeamId = TextUtils.ToInt(cboTeam.EditValue); // VTN update 14725
            //arrParamName = new string[] { "@FilterText", "@DateStart", "@DateEnd", "@IDApprovedTP", "@Status", "@DeleteFlag" };
            //arrParamValue = new object[] { txtFilterText.Text, dateTimeS, dateTimeE, IDApproved, StatusApprove, deleteFlag };


            bool isBGD = Global.DepartmentID == 1 && Global.EmployeeID != 54;


            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                dtData = TextUtils.LoadDataFromSP("spGetApprovedByApprovedTP", "A",
                                        new string[] { "@FilterText", "@DateStart", "@DateEnd", "@IDApprovedTP", "@Status", "@DeleteFlag", "@EmployeeID", "@TType", "@StatusHR", "@StatusBGD", "@IsBGD", "@UserTeamID" },
                                        new object[] { txtFilterText.Text, dateTimeS, dateTimeE, IDApproved, StatusApprove, deleteFlag, employee, ttype, statusHR, statusBGD, isBGD, userTeamId }); // VTN update 14725

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    string reason = TextUtils.ToString(dtData.Rows[i]["Reason"]);

                    string pattern1 = "</.*?>";


                    Match match = Regex.Match(reason, pattern1);

                    if (match.Success)
                    {
                        reason = reason.Replace(match.Value, "\n");

                        string pattern2 = @"<.*?>";
                        Match match2 = Regex.Match(reason, pattern2);
                        if (match2.Success)
                        {
                            reason = reason.Replace(match2.Value, "");
                        }
                    }


                    dtData.Rows[i]["Reason"] = reason;


                }
                grdData.DataSource = dtData;
            }
        }
        void loadDepartment()
        {
            //DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            //cbDepartment.Properties.DataSource = dt;
            //cbDepartment.Properties.DisplayMember = "Name";
            //cbDepartment.Properties.ValueMember = "ID";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            approved(false);
        }
        void approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            int[] rowIndex = grvData.GetSelectedRows();
            bool isSeniorApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsSeniorApproved)); // VTN update 12725
            if (rowIndex.Length == 0)
            {
                if (MessageBox.Show(String.Format("Vui lòng chọn nhân viên!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
                {
                    return;
                }
            }

            if (!Global.IsAdmin)
            {

                for (int i = 0; i < rowIndex.Length; i++)
                {
                    bool Isapproved = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                    bool IsApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR));
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                    string fullname = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
                    int isCancel = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colIsCancelRegister));

                    bool deleteFlag = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colDeleteFlag));
                    string table = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colTypeText));

                    bool isApprovedBGD = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedBGD));
                    if (id <= 0)
                    {
                        continue;
                    }

                    if (deleteFlag)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} đã tự xoá khai báo [{table}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!isApproved && IsApprovedHR)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} đã được HR duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (isCancel > 0)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} đã đăng ký hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (isApproved & Isapproved)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} đã được TBP duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!isApproved & !Isapproved)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} chưa được TBP duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!isApproved & isApprovedBGD)
                    {
                        MessageBox.Show($"Bạn không thể {approved} vì nhân viên {fullname} đã được BGĐ duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }


            //if ( // VTN update 14725
            //    MessageBox.Show(string.Format(!isSeniorApproved && isApproved ?
            //    "Senior chưa duyệt! \nBạn có chắc muốn {0} danh sách nhân viên đã chọn không?" : "Bạn có chắc muốn {0} danh sách nhân viên đã chọn không?", approved, name),
            //    TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{

            //    DataTable dataWFHs = (DataTable)grdData.DataSource;
            //    dataWFHs = dataWFHs.Clone();
            //    foreach (int row in rowIndex)
            //    {
            //        int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
            //        int ttype = TextUtils.ToInt(grvData.GetRowCellValue(row, colTType));

            //        if (id <= 0 || ttype != 5) continue; //Nếu ko chọn WFH

            //        var dataRow = grvData.GetDataRow(row);
            //        //if (!dataWFHs.Contains(dataRow)) dataWFHs.Add(dataRow);
            //        dataWFHs.ImportRow(dataRow);
            //    }
            //    //string evaluateResults = "";
            //    DataTable dtWFHUpdated = dataWFHs.Clone();
            //    if (isApproved && dataWFHs.Rows.Count > 0)
            //    {
            //        frmApprovedTBP frm = new frmApprovedTBP();
            //        frm.grdData.DataSource = dataWFHs;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            //evaluateResults = frm.txtEvaluateResults.Text.Trim();

            //            dtWFHUpdated = (DataTable)frm.grdData.DataSource;
            //        }
            //        else
            //        {

            //            foreach (DataRow row in dataWFHs.Rows)
            //            {
            //                row["EvaluateResults"] = row["EvaluateResults", DataRowVersion.Original];
            //            }
            //            //DataRow dataRow = arrRow[0];
            //            //dataRow["EvaluateResults"] = dataRow["EvaluateResults", DataRowVersion.Original];
            //            dtWFHUpdated = dataWFHs;
            //        }
            //    }
            //    //List<int> listID = new List<int>();
            //    for (int i = 0; i < rowIndex.Length; i++)
            //    {
            //        //listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
            //        int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
            //        string FieldName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colColumnNameUpdate));
            //        string TableName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colTableName));
            //        int declineApprove = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDecilineApprove));
            //        if (TableName == "") continue;
            //        //if (isApproved)
            //        //{
            //        //    string sql = $"Update {TableName} set DecilineApprove = 1 where ID = {id}";
            //        //    TextUtils.ExcuteSQL(sql);
            //        //}

            //        string evaluateResults = "";
            //        var wfh = dtWFHUpdated.Select($"ID = {id} and TType = 5");
            //        if (wfh.Length > 0) evaluateResults = TextUtils.ToString(wfh[0].Field<string>("EvaluateResults"));

            //        TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
            //                new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate", "@ValueDecilineApprove", "@EvaluateResults" },
            //                new object[] { TableName, FieldName, isApproved ? 1 : 0, id, Global.LoginName, DateTime.Now, isApproved ? 1 : declineApprove, evaluateResults });

            //    }
            //    //TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
            //    //            new string[] { "@TableName", "@FieldName", "@Value", "@ID" },
            //    //            new object[] { "EmployeeOvertime", "IsApproved", isApproved ? 1 : 0, string.Join(",", listID) });
            //    loadData();
            //}


            //NXLuong Update 22/9/25
            int countTType = 0;
            foreach (int row in rowIndex)
            {
                int ttype = TextUtils.ToInt(grvData.GetRowCellValue(row, colTType));
                if (ttype == 3)
                {
                    countTType++;

                }
            }
            bool requireSeniorCheck = Global.DepartmentID == 2;
            // if(colTypeText.s=="D")
            // WARNING text
            // Nội dung cảnh báo
            string msg = requireSeniorCheck && !isSeniorApproved && isApproved && countTType > 0
                ? $"Senior chưa duyệt làm thêm !\nBạn có chắc muốn {approved} danh sách nhân viên đã chọn không?\nYes: Duyệt tất cả bản ghi đã chọn \nNo: Duyệt bản ghi làm thêm đã được senior duyệt \nCancel: Hủy lựa chọn"
                : $"Bạn có chắc muốn {approved} danh sách nhân viên đã chọn không?";
            var buttons = (requireSeniorCheck && countTType > 0) ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
            var dr = MessageBox.Show(msg, TextUtils.Caption, buttons, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel) return; // chỉ xảy ra khi countTType > 0
                                                   // Tập dòng cần xử lý: Yes = tất cả, No = chỉ dòng đã Senior duyệt
            IEnumerable<int> rowsToProcess = rowIndex;
            if (dr == DialogResult.No)
            {

                //rowsToProcess = rowIndex.Where(r =>
                //    TextUtils.ToInt(grvData.GetRowCellValue(r, colIsSeniorApproved)) == 1
                //);
                rowsToProcess = rowIndex.Where(r =>
                TextUtils.ToBoolean(grvData.GetRowCellValue(r, colIsSeniorApproved)) ||
                TextUtils.ToInt(grvData.GetRowCellValue(r, colTType)) != 3);
            }

            var rowsArr = rowsToProcess as int[] ?? rowsToProcess.ToArray();
            if (rowsArr.Length == 0) return;


            DataTable dataWFHs = (DataTable)grdData.DataSource;
            dataWFHs = dataWFHs.Clone();

            foreach (int row in rowsArr)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                int ttype = TextUtils.ToInt(grvData.GetRowCellValue(row, colTType));
                if (id <= 0 || ttype != 5) continue;
                dataWFHs.ImportRow(grvData.GetDataRow(row));
            }

            DataTable dtWFHUpdated = dataWFHs.Clone();
            if (isApproved && dataWFHs.Rows.Count > 0)
            {
                using (var frm = new frmApprovedTBP())
                {
                    frm.grdData.DataSource = dataWFHs;
                    if (frm.ShowDialog() == DialogResult.OK)
                        dtWFHUpdated = (DataTable)frm.grdData.DataSource;
                    else
                    {
                        foreach (DataRow row in dataWFHs.Rows)
                            row["EvaluateResults"] = row["EvaluateResults", DataRowVersion.Original];
                        dtWFHUpdated = dataWFHs;
                    }
                }
            }

            for (int i = 0; i < rowsArr.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowsArr[i], colID));
                string fieldName = TextUtils.ToString(grvData.GetRowCellValue(rowsArr[i], colColumnNameUpdate));
                string tableName = TextUtils.ToString(grvData.GetRowCellValue(rowsArr[i], colTableName));
                int declineApprove = TextUtils.ToInt(grvData.GetRowCellValue(rowsArr[i], colDecilineApprove));
                if (string.IsNullOrEmpty(tableName)) continue;

                string evaluateResults = "";
                var wfh = dtWFHUpdated.Select($"ID = {id} and TType = 5");
                if (wfh.Length > 0) evaluateResults = TextUtils.ToString(wfh[0].Field<string>("EvaluateResults"));

                TextUtils.ExcuteProcedure(
                    "spUpdateTableByFieldNameAndID",
                    new[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate", "@ValueDecilineApprove", "@EvaluateResults" },
                    new object[] { tableName, fieldName, isApproved ? 1 : 0, id, Global.LoginName, DateTime.Now, isApproved ? 1 : declineApprove, evaluateResults }
                );
            }

            loadData();

        }


        void ApprovedBGD(bool isApproved)
        {
            string isApprovedText = isApproved == true ? "duyệt" : "hủy duyệt";
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            int[] selectedRows = grvData.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                MessageBox.Show($"Vui lòng chọn nhân viên muốn {isApprovedText}!", "Thông báo");
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} {selectedRows.Length} đăng ký đã chọn không?\n(Những đăng ký chưa được HR duyệt sẽ tự động được bỏ qua!)", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    string tableName = TextUtils.ToString(grvData.GetRowCellValue(row, colTableName));

                    var myDict = new Dictionary<string, object>()
                    {
                        { EmployeeWFHModel_Enum.IsApprovedBGD.ToString(),isApproved},
                        { EmployeeWFHModel_Enum.ApprovedBGDID.ToString(),Global.EmployeeID},
                        { EmployeeWFHModel_Enum.DateApprovedBGD.ToString(),DateTime.Now},
                        { EmployeeWFHModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { EmployeeWFHModel_Enum.UpdatedBy.ToString(),Global.AppCodeName},
                    };

                    if (tableName == "EmployeeOnLeave")
                    {
                        EmployeeOnLeaveModel model = SQLHelper<EmployeeOnLeaveModel>.FindByID(id) ?? new EmployeeOnLeaveModel();
                        if (model.IsApprovedHR == false) continue;
                        if (model.IsApprovedBGD == true) continue;

                        SQLHelper<EmployeeOnLeaveModel>.UpdateFieldsByID(myDict, id);
                    }
                    else if (tableName == "EmployeeBussiness")
                    {
                        EmployeeBussinessModel model = SQLHelper<EmployeeBussinessModel>.FindByID(id) ?? new EmployeeBussinessModel();
                        if (model.IsApprovedHR == false) continue;
                        if (model.IsApprovedBGD == true) continue;

                        SQLHelper<EmployeeBussinessModel>.UpdateFieldsByID(myDict, id);
                    }
                    else if (tableName == "EmployeeOvertime")
                    {
                        EmployeeOvertimeModel model = SQLHelper<EmployeeOvertimeModel>.FindByID(id) ?? new EmployeeOvertimeModel();
                        if (model.IsApprovedHR == false) continue;
                        if (model.IsApprovedBGD == true) continue;

                        SQLHelper<EmployeeOvertimeModel>.UpdateFieldsByID(myDict, id);
                    }
                    else if (tableName == "EmployeeWFH")
                    {
                        EmployeeWFHModel wfh = SQLHelper<EmployeeWFHModel>.FindByID(id) ?? new EmployeeWFHModel();
                        if (wfh.IsApprovedHR == false) continue;
                        if (wfh.IsApprovedBGD == true) continue;

                        SQLHelper<EmployeeWFHModel>.UpdateFieldsByID(myDict, id);
                    }
                }

                loadData();
            }
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnDecilineApprove_Click(object sender, EventArgs e)
        {
            List<EmployeeDeclineApprove> list = new List<EmployeeDeclineApprove>();
            int[] rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn nhân viên!", "Thông báo");
                return;
            }

            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0)
                {
                    continue;
                }
                bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApprovedTP));
                int isCancel = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsCancelRegister));
                bool deleteFlag = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colDeleteFlag));

                string fullName = TextUtils.ToString(grvData.GetRowCellValue(row, colFullName));
                string table = TextUtils.ToString(grvData.GetRowCellValue(row, colTypeText));
                if (isApprovedTP)
                {
                    MessageBox.Show($"Nhân viên [{fullName}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo");
                    return;
                }

                if (isCancel > 0)
                {
                    MessageBox.Show($"Nhân viên [{fullName}] đã đăng ký huỷ!", "Thông báo");
                    return;
                }

                if (deleteFlag)
                {
                    MessageBox.Show($"Nhân viên [{fullName}] đã xoá đăng ký [{table}]!", "Thông báo");
                    return;
                }

                EmployeeDeclineApprove declineApprove = new EmployeeDeclineApprove();
                declineApprove.ID = id;
                declineApprove.TableName = TextUtils.ToString(grvData.GetRowCellValue(row, colTableName)).Trim();

                list.Add(declineApprove);
            }


            //int[] rowIndex = grvData.GetSelectedRows();
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (id <= 0)
            //{
            //    if (MessageBox.Show(String.Format("Vui lòng chọn nhân viên!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
            //    {
            //        return;
            //    }
            //}


            //bool deleteFlag = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colDeleteFlag));
            //string table = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeText));
            //string fullname = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (deleteFlag)
            //{
            //    MessageBox.Show($"Bạn không thể không duyệt vì nhân viên {fullname} đã tự xoá khai báo [{table}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //int isCancel = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIsCancelRegister));
            //if (isCancel > 0)
            //{
            //    //string fullname = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
            //    MessageBox.Show($"Bạn không thể không duyệt vì nhân viên {fullname} đã đăng ký hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}



            //bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            //if (IsApprovedTP)
            //{
            //    MessageBox.Show(String.Format("Bạn hãy huỷ duyệt cho nhân viên trước!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //int departmentId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDepartment));
            ////string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
            ////int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colNguoiDuyet));
            //string TableName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTableName));
            //if (departmentId == 0) return;


            frmReasonDecline frm = new frmReasonDecline();
            //frm.ID = id;
            //frm.tablename = TableName;
            frm.listDecline = list;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }

        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //e.Appearance.BackColor = Color.White;
            //e.Appearance.ForeColor = Color.Black;

            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }

            if (e.RowHandle >= 0)
            {
                bool deleteFlag = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colDeleteFlag));
                if (deleteFlag)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }

        private void cboDeleteFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colReason)
            //{
            //    string value = TextUtils.ToString(e.Value);

            //    string pattern1 = "<.>";


            //    Match match = Regex.Match(value, pattern1);

            //    if (match.Success)
            //    {
            //        value = value.Replace(match.Value, "\n");

            //        string pattern2 = "</.>";
            //        Match match2 = Regex.Match(value, pattern2);
            //        if (match2.Success)
            //        {
            //            value = value.Replace(match2.Value, "");
            //        }
            //    }

            //    e.DisplayText = value;


            //    //dt.Rows[e.ListSourceRowIndex]["Reason"] = value;
            //    //grvData.RefreshData();
            //}
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }
        private void btnApprovedBGD_Click(object sender, EventArgs e)
        {
            ApprovedBGD(true);
        }

        private void btnUnApprovedBGD_Click(object sender, EventArgs e)
        {
            ApprovedBGD(false);
        }

        private void btnApprovedCancel_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));

            if (rowIndex.Length == 0)
            {
                if (MessageBox.Show(String.Format("Vui lòng chọn nhân viên!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
                {
                    return;
                }
            }
            else if (rowIndex.Length == 1)
            {
                if (MessageBox.Show(String.Format("Bạn có chắc muốn không duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (MessageBox.Show(String.Format("Bạn có chắc muốn không duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            for (int i = 0; i < rowIndex.Length; i++)
            {
                bool deleteFlag = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colDeleteFlag));
                string table = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colTypeText));
                string fullname = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));

                if (id <= 0)
                {
                    continue;
                }

                if (deleteFlag)
                {
                    MessageBox.Show($"Bạn không thể Duyệt huỷ đăng ký vì nhân viên {fullname} đã tự xoá khai báo [{table}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int isCancel = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colIsCancelRegister));
                if (isCancel < 0)
                {

                    MessageBox.Show($"Bạn không thể duyệt đăng ký hủy vì nhân viên {fullname} chưa đăng ký hủy!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return;
                }
            }

            for (int i = 0; i < rowIndex.Length; i++)
            {

                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colIDDepartment));

                string TableName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colTableName));
                if (departmentId == 0) continue;
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;

                int declineApprove = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDecilineApprove));

                TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                            new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate", "@ValueDecilineApprove" },
                            new object[] { TableName, "IsCancelTP", 1, id, Global.LoginName, DateTime.Now, declineApprove });
            }
            loadData();
        }

        // LN Hải update thêm senior duyệt 11/06/2025
        private void btnSeniorApprove_Click(object sender, EventArgs e)
        {
            ApprovedSenior(true);
        }

        private void btnUnApprovedSenior_Click(object sender, EventArgs e)
        {
            ApprovedSenior(false);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void ApprovedSenior(bool isApproved)
        {
            string isApprovedText = isApproved ? "duyệt" : "hủy duyệt";
            int[] selectedRows = grvData.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                MessageBox.Show($"Vui lòng chọn nhân viên muốn {isApprovedText}!", "Thông báo");
                return;
            }

            var dt = TextUtils.LoadDataFromSP("spGetUserTeamLinkByLeaderID", "A", new string[] { "@LeaderID" }, new object[] { Global.EmployeeID });
            foreach (var row in selectedRows)
            {
                int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(row, colEmployeeID));
                string tableName = TextUtils.ToString(grvData.GetRowCellValue(row, colTableName));
                string fullName = TextUtils.ToString(grvData.GetRowCellValue(row, colFullName));

                if (tableName != "EmployeeOvertime") continue;

                var seniors = dt.AsEnumerable().Where(x => x.Field<int>("EmployeeID") == employeeID).ToList();
                if (seniors.Count <= 0)
                {
                    MessageBox.Show($"Bạn không phải là Senior của nhân viên [{fullName}]", "Thông báo");
                    return;
                }
            }

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} {selectedRows.Length} đăng ký đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes) return;

            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                string tableName = TextUtils.ToString(grvData.GetRowCellValue(row, colTableName));

                if (tableName == "EmployeeOvertime")
                {
                    EmployeeOvertimeModel model = SQLHelper<EmployeeOvertimeModel>.FindByID(id) ?? new EmployeeOvertimeModel();
                    if (model.IsApproved == true) continue;
                    if (model.IsApprovedBGD == true) continue;

                    model.IsSeniorApproved = isApproved;
                    model.ApprovedSeniorID = Global.EmployeeID;
                    model.DateApprovedSenitor = DateTime.Now;
                    SQLHelper<EmployeeOvertimeModel>.Update(model);
                }
            }
            loadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
        #region Update bổ sung dữ liệu 14725
        void loadUserTeam() //NTA B - update 08/09/25
        {

            //var listDept = TextUtils.LoadDataFromSP("spGetUserTeam", "A", new string[] { "@DepartmentID" }, new object[] { 0 });
            //cboTeam.Properties.ValueMember = "ID";
            //cboTeam.Properties.DisplayMember = "Name";
            //cboTeam.Properties.DataSource = listDept;
            //cboTeam.EditValue = Global.UserTeamID;

            int currentYear = DateTime.Now.Year;
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1; 
            DataTable dt = TextUtils.LoadDataFromSP("spGetALLKPIEmployeeTeam", "A",
                                                        new string[] { "@YearValue", "@QuarterValue", "@DepartmentID" },
                                                        new object[] { currentYear, currentQuarter, 0 });
            //var filteredRows = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin || TextUtils.ToInt(r["LeaderID"]) == Global.EmployeeID).CopyToDataTable();

            DataRow newRow = dt.NewRow();
            newRow["ID"] = 0;
            newRow["Name"] = "--Tất cả các Team--";
            dt.Rows.InsertAt(newRow, 0);

            cboTeam.Properties.DataSource = dt;
            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "Name";
            //cboTeam.EditValue = Global.UserTeamID;
        }
        #endregion



        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //DownloadFile();
        }


        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            //DownloadFile();
        }

        private void grvData_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column != colFileName) return;

            DownloadFile();

        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        void DownloadFile()
        {
            try
            {
                string tableName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTableName));
                string api = "http://113.190.234.64:8083/api";
                string folderName = "";
                if (tableName == "EmployeeBussiness") folderName = "CongTac";
                else if (tableName == "EmployeeOvertime") folderName = "LamThem";
                else return;


                //api = @"http://192.168.1.2:8083/api/lamthem/Năm 2025/Tháng 7/Ngày 30.07.2025/NV076/1031533_cancel_close_cross_delete_remove_icon.png";

                string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFilePath));
                string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
                if (string.IsNullOrWhiteSpace(fileName)) return;

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", folderName);

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                Uri uri = new Uri(api + filePath);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(uri, Path.Combine(pathDownload, fileName));
                Process.Start(pathDownload);
                Process.Start(Path.Combine(pathDownload, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tìm thấy file!\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }
    }
}
