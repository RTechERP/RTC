using BMS;
using DevExpress.XtraGrid.Views.Card;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.PO
{
    public partial class frmViewPOKH11 : _Forms
    {
        public frmViewPOKH11()
        {
            InitializeComponent();
        }

        private void frmViewPOKH_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-1);
            loadcbColor();
            txtPageNumber.Text = "1";
            loadgroupSale();
            loadCustomer();
            loadUser();
            loadMainIndex();
            loadPOKH();
        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        void loadcbColor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add(i);
            }

            cbColor.Properties.DisplayMember = "ID";
            cbColor.Properties.ValueMember = "ID";
            cbColor.Properties.DataSource = dt;
        }
        /// <summary>
        /// load type
        /// </summary>
        public void loadMainIndex()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN(8,9,10,11,12,13)");
            cbStatus.Properties.DisplayMember = "MainIndex";
            cbStatus.Properties.ValueMember = "ID";
            cbStatus.Properties.DataSource = dt;
        }

        /// <summary>
        /// load POKH
        /// </summary>
        private void loadPOKH()
        {

            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetViewPOKH"
                                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@CustomerID", "@UserID", "@POType", "@Status", "@Group", "@StartDate", "@EndDate" }
                                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                        txtFilterText.Text.Trim(), TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbUser.EditValue), TextUtils.ToInt(cbStatus.EditValue),TextUtils.ToInt(cbColor.EditValue),TextUtils.ToInt( cbGroup.EditValue),dateTimeS,dateTimeE});


            if (ds.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(ds.Tables[0].Rows[0]["TotalPage"]);
            //Set up a master-detail relationship between the DataTables
            DataColumn keyColumn = ds.Tables[0].Columns["ID"];
            DataColumn foreignKeyColumn = ds.Tables[1].Columns["POKHDetailID"];
            ds.Relations.Add(keyColumn, foreignKeyColumn);//link
            //Bind the grid control to the data source
            grdMaster.DataSource = ds.Tables["Table"];
            grdMaster.ForceInitialize();
            grvMaster.ExpandAllGroups();
          
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void grvMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colColorStatus));
            if (e.Column == colStatus)
            {
                switch (status)
                {
                    case 0:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    case 1:
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                        break;
                    case 3:
                        e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                        break;
                    case 4:
                        e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                        break;
                    default:
                        break;
                }
            }
        }

        private void cbvColor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(cbvColor.GetRowCellValue(e.RowHandle, colColor));

            switch (status)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 3:
                    e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 4:
                    e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;

            }
        }

        private void cbColor_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
           
            int color = TextUtils.ToInt(cbColor.EditValue);
            switch (color)
            {
                case 0:
                    cbColor.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    cbColor.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case 2:
                    cbColor.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 3:
                    cbColor.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 4:
                    cbColor.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;
            }
        }

        private void cbStatus_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadPOKH();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadPOKH();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadPOKH();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadPOKH();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadPOKH();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void grvMaster_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {

        }

        private void grvMaster_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {

        }

        private void grvMaster_MasterRowGetRelationDisplayCaption(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {

        }

        private void grvMaster_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "gridview1";
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }
    }
}
