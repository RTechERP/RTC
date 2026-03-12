using BMS.Model;
using DevExpress.Utils;
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
    public partial class frmProjectSummary : _Forms
    {
        DataSet oDataSet;
        public frmProjectSummary()
        {
            InitializeComponent();
        }

        private void frmProjectSummary_Load(object sender, EventArgs e)
        {
            oDataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeForProject", new string[] { }, new object[] { });

            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = new DateTime(dtpFromDate.Value.Year, 1, 1, 0, 0, 0).AddYears(-1);
            txtPageNumber.Text = "1";

            loadPM();
            loadCustomer();
            loadUserSale();
            loadUserTech();
            loadUserLeader();
            loadStatus();
            loadBusinessField();
            loadProjectType();
            loadProjectTypeLink();
            loadProjectItem();
            loadData();

        }
        void loadProjectTypeLink()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { id });
            tlProjectTypeMaster.DataSource = dt;
            tlProjectTypeMaster.ExpandAll();

        }
        void loadProjectDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectDetail", "A", new string[] { "@ID" }, new object[] { IDMaster });
            grdData.DataSource = dt;
        }
        void loadProjectItem()
        {
            //  DataTable dt = TextUtils.Select("SELECT * FROM ProjectType p WHERE not (p.parentID = 0 AND p.id IN(SELECT ParentID FROM ProjectType) or p.id = 4)");
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id <= 0) id--;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItem", "A", new string[] { "@ProjectID" }, new object[] { id });
            grdProjectItem.DataSource = dt;
            //tlProjectItem.DataSource = dt;
            //tlProjectItem.ExpandAll();
        }
        private void loadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

                string projectType = "";
                DataTable dt = TextUtils.Select("SELECT ID FROM dbo.ProjectType");
                int[] typeCheck = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                List<int> listID = new List<int>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = TextUtils.ToInt(dt.Rows[i]["ID"]);
                    listID.Add(id);
                }
                if (TextUtils.ToString(cboProjectType.EditValue) == "")
                {
                    projectType = string.Join(",", listID);
                }
                else
                {
                    projectType = TextUtils.ToString(cboProjectType.EditValue);
                }
                if (!string.IsNullOrEmpty(TextUtils.ToString(cboProjectType.Properties.GetCheckedItems())))
                {
                    string stringType = cboProjectType.Properties.GetCheckedItems().ToString();

                    string[] checkedItems = stringType.Split(',');

                    foreach (var item in checkedItems)
                    {
                        int index = listID.IndexOf(TextUtils.ToInt(item));
                        if (index >= 0 && index < typeCheck.Length)
                        {
                            typeCheck[index] = 1;
                        }
                    }
                }

                var employeePm = TextUtils.ToInt(cboPM.EditValue);
                var x = TextUtils.ToInt(cboLeader.EditValue);
                int _bussinessFieldID = TextUtils.ToInt(cboBusinessField.EditValue);

                DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetProject"
                    , new string[] { "@PageSize", "@PageNumber", "@DateStart", "@DateEnd", "@FilterText", "@CustomerID", "@UserID", "@ListProjectType", "@LeaderID", "@UserIDTech", "@EmployeeIDPM", "@1", "@2", "@3", "@4", "@5", "@6", "@7", "@8", "@9", "@UserIDPriotity", "@BusinessFieldID" }
                    , new object[] { TextUtils.ToInt(txtPageSize.Text), TextUtils.ToInt(txtPageNumber.Text), dateTimeS, dateTimeE, txtFilterText.Text.Trim()
                                ,TextUtils.ToInt(cbCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue), projectType, TextUtils.ToInt(cboLeader.EditValue), TextUtils.ToInt(cboUserTech.EditValue),employeePm ,typeCheck[0] ,typeCheck[1] ,typeCheck[2] ,typeCheck[3] ,typeCheck[4] ,typeCheck[5] ,typeCheck[6] ,typeCheck[7] ,typeCheck[8],Global.UserID, _bussinessFieldID});
                grdMaster.DataSource = oDataSet.Tables[0];

                if (oDataSet.Tables.Count == 0) return;
                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
                txtShowCount.Text = TextUtils.ToString(oDataSet.Tables[2].Rows[0]["TotalEntries"]) + " Entries";
            }
        }
        void loadProjectType()
        {
            DataTable dt = TextUtils.Select("SELECT ID,ProjectTypeName  FROM dbo.ProjectType WHERE ID <> 4");
            //  DataTable dt = TextUtils.Select("SELECT * FROM ProjectType p WHERE not (p.parentID = 0 AND p.id IN(SELECT ParentID FROM ProjectType) or p.id = 4)");

            //DataRow dtRow = dt.NewRow();
            //dtRow["ID"] = 0;
            //dtRow["ProjectTypeName"] = "Tất cả";  
            //dt.Rows.Add(dtRow);
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DataSource = dt;

             cboProjectType.SetEditValue(2); // Cơ khí
          //  cboProjectType.Enabled = false;

            //cboProjectType.Properties.Items.Add(0);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "")
                return;
            else
            {
                txtPageNumber.Text = "1";
                loadData();
            }
        }

        private void txtPageSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {

        }
        void loadPM()
        {
            DataTable dtPM = TextUtils.GetTable("spGetEmployeeForProject");
            cboPM.Properties.DataSource = dtPM;
            cboPM.Properties.DisplayMember = "FullName";
            cboPM.Properties.ValueMember = "EmployeeID";
        }


        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerCode, CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerCode";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// load ng phụ trách Sale lên cbUser
        /// </summary>
        void loadUserSale()
        {
            //DataTable dt = TextUtils.GetTable("spGetEmployeeForProject");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = oDataSet.Tables[1];
        }

        /// <summary>
        /// Load danh sách người phụ trách kỹ thuật lên cboUserTech
        /// </summary>
        void loadUserTech()
        {
            //DataTable dtUser = TextUtils.GetTable("spGetEmployeeForProject");
            cboUserTech.Properties.DisplayMember = "FullName";
            cboUserTech.Properties.ValueMember = "ID";
            cboUserTech.Properties.DataSource = oDataSet.Tables[1];
        }

        /// <summary>
        /// Load danh sách leader lên cboLeader
        /// </summary>
        void loadUserLeader()
        {
            //DataTable dtUser = TextUtils.GetTable("spGetEmployeeForProject");
            cboLeader.Properties.DisplayMember = "FullName";
            cboLeader.Properties.ValueMember = "ID";
            cboLeader.Properties.DataSource = oDataSet.Tables[1];
        }
        void loadStatus()
        {
            DataTable dt = TextUtils.Select("Select ID as ProjectStatusID, StatusName from ProjectStatus order by ID");
            cboProjectStatus.DataSource = dt;
            cboProjectStatus.DisplayMember = "StatusName";
            cboProjectStatus.ValueMember = "ProjectStatusID";
        }

        private void loadBusinessField()
        {
            List<BusinessFieldModel> list = SQLHelper<BusinessFieldModel>.FindAll().OrderByDescending(x => x.STT).ToList();

            cboBusinessField.Properties.ValueMember = "ID";
            cboBusinessField.Properties.DisplayMember = "Name";
            cboBusinessField.Properties.DataSource = list;
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadProjectDetail();
            loadProjectTypeLink();
            loadProjectItem();
        }

        private void grvProjectItem_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                //string dateEnd = TextUtils.ToString(grvProjectItem.GetRowCellValue(e.RowHandle, colActualEndDate)).Trim();

                //DateTime actualDE = TextUtils.ToDate3(grvProjectItem.GetRowCellValue(e.RowHandle, colActualEndDate));
                //DateTime planDE = TextUtils.ToDate3(grvProjectItem.GetRowCellValue(e.RowHandle, colPlanEndDate));

                //var date = actualDE - planDE;
                //int day = date.Days;

                //if (string.IsNullOrEmpty(dateEnd) || day >= 1)
                //{
                //    e.Appearance.BackColor = Color.LightSalmon;hy7u
                //    //e.Appearance.BackColor2 = Color.Red;

                //    //e.Appearance.ForeColor = Color.White;
                //    e.HighPriority = true;
                //}

                int itemLate = TextUtils.ToInt(grvProjectItem.GetRowCellValue(e.RowHandle, "ItemLateActual"));
                if (itemLate == 1)
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.HighPriority = true;
                }

                if (itemLate == 2)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }
    }
}