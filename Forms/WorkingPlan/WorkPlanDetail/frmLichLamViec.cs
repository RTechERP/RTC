using BMS.Model;
using DevExpress.XtraGrid.Columns;
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
    public partial class frmLichLamViec : _Forms
    {
        UsersModel user = new UsersModel();

        List<GridColumn> listCol = new List<GridColumn>();
        public frmLichLamViec()
        {
            InitializeComponent();
        }

        private void frmKeHoachCongViecDetail_Load(object sender, EventArgs e)
        {

            DateTime dateFirstWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime dateEndWeek = dateFirstWeek.AddDays(+6);

            dtpFromDate.Value = dateFirstWeek;
            dtpEndDate.Value = dateEndWeek;
            loadNhanVien();
            loadcboPhongban();
            loadUserTeam();

            cboPhongban.SelectedIndex = 2;
        }
        void loadNhanVien()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            //DataTable dt = TextUtils.LoadDataFromSP("spGetTest1", "A", new string[] { "@DateStart", "@DateEnd" }, new object[] { dateTimeS, dateTimeE });
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetWorkPlanDetail1",
                                new string[] { "@DateStart", "@DateEnd", "@FilterText", "@DepartmentID", "@TeamID" },
                                new object[] { dateTimeS, dateTimeE, txtFilterText.Text.Trim(), TextUtils.ToInt(cboPhongban.SelectedValue), TextUtils.ToInt(cboTeam.SelectedValue) });

            DataTable dtColDate = dataSet.Tables[0];
            DataTable dtData = dataSet.Tables[1];
            
            if (listCol.Count > 0)
            {
                //grvData.Columns.Clear();
                foreach (GridColumn item in listCol)
                {
                    grvData.Columns.Remove(item);
                }
                listCol.Clear();
            }

            colHidden.Visible = false;

            for (int i = 0; i < dtColDate.Rows.Count; i++)
            {
                GridColumn col = grvData.Columns.Add();
                DateTime? dateCol = TextUtils.ToDate4(dtColDate.Rows[i]["AllDates"]);

                col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = dateCol.Value.ToString("dd/MM/yyyy");
                col.FieldName = dateCol.Value.ToString("yyyy-MM-dd");
                col.Name = "col" + dateCol.Value.ToString("ddMMyy");
                col.Visible = true;
                col.Width = 300;
                col.MinWidth = 300;
                col.Width = 300;
                col.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                listCol.Add(col);
            }

            grdData.DataSource = dtData;
        }

        void loadcboPhongban()
        {
            DataTable dt = TextUtils.Select($"Select ID, Name from dbo.Department");
            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["Name"] = "Tất cả";
            dt.Rows.InsertAt(row, 0);

            cboPhongban.DataSource = dt;
            cboPhongban.DisplayMember = "Name";
            cboPhongban.ValueMember = "ID";
        }


        void loadUserTeam()
        {
            DataTable dt = TextUtils.Select($"Select ID, Name from dbo.UserTeam");
            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["Name"] = "Tất cả";
            dt.Rows.InsertAt(row, 0);

            cboTeam.DataSource = dt;
            cboTeam.DisplayMember = "Name";
            cboTeam.ValueMember = "ID";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            loadNhanVien();
        }

        private void cboPhongban_SelectedValueChanged(object sender, EventArgs e)
        {
            //loadNhanVien();
        }

        private void cboPhongban_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNhanVien();
        }

        private void cboTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNhanVien();
        }
    }
}
